using AgeOfKing.Abstract.Data;
using AgeOfKing.Components;
using AgeOfKing.Data;
using AgeOfKing.Systems.Input;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AgeOfKing.Systems
{
    public interface IBuildingTileChecker
    {
        public IPlayer Owner { get; }
        public event Action<BuildingData,IPlayer> OnBuildCommand;
        public event Action<Dictionary<Vector3Int, bool>> OnCheckingTiles;
        public event Action OnPlacementEnd;
        public void InitializeBuildingData(BuildingData data);
    }

    public  class PlayerBuildingTileChecker: IBuildingTileChecker
    {


        public event Action<BuildingData, IPlayer> OnBuildCommand;
        public event Action OnPlacementEnd;
        public event Action<Dictionary<Vector3Int, bool>> OnCheckingTiles;

        SIDE _playgroundSide;
        BuildingData _buildingData;
        bool _isAreaEnable = false;
        bool _isActive = false;


        Dictionary<Vector3Int, bool> tileStates;

        public IPlayer Owner { get; private set; }

        public PlayerBuildingTileChecker(IInputManager inputManager,IMapInput mapInput,IPlayer player, SIDE side)
        {
            tileStates = new Dictionary<Vector3Int, bool>(50);
            mapInput.OnCellHover += OnChange_MousePosition;
            inputManager.OnPointerSelectionClicked += OnClicked_MouseLeft;
            inputManager.OnPointerCommandClicked += OnClicked_MouseRight;
            Owner = player;
            _playgroundSide = side;
        }


        private  void OnClicked_MouseRight(Vector2 location)
        {
            if (_isActive == false)
                return;

            EndPlacement();
        }

        private  void OnClicked_MouseLeft(Vector2 location)
        {
            if (_isActive == false)
                return;

            if (_isAreaEnable)
            {
               OnBuildCommand?.Invoke(_buildingData, Owner);
            }

            EndPlacement();
        }

        private void OnChange_MousePosition(Vector3Int pointerCell)
        { 
            if (_isActive == false)
                return;

            CheckBuildingPlace(pointerCell);
            OnCheckingTiles?.Invoke(tileStates);
        }

        public  void InitializeBuildingData(BuildingData data)
        {
            _buildingData = data;
            _isActive = true;
        }

        public  void EndPlacement()
        {
            _isActive = false;
            _isAreaEnable = false;
            OnPlacementEnd?.Invoke();
        }

        void CheckBuildingPlace(Vector3Int pointerCell)
        {
            // check with job system ?

            tileStates.Clear();

            _isAreaEnable = true;
            bool isBlocked;
            for (int x = 0; x < _buildingData.GetXDimension; x++)
            {
                for (int y = 0; y < _buildingData.GetYDimension; y++)
                {
                    Vector3Int cellPosition = new Vector3Int(pointerCell.x + x, pointerCell.y + y, 0);
                    if(_playgroundSide == SIDE.LEFT)
                    {
                        if (cellPosition.x >= 0)
                        {
                            isBlocked = true;
                        }
                        else
                        {
                            isBlocked = MapEntityDataBase.IsTileBlocked(cellPosition);
                        }
                    }
                    else
                    {
                        if (cellPosition.x <= 0)
                        {
                            isBlocked = true;
                        }
                        else
                        {
                            isBlocked = MapEntityDataBase.IsTileBlocked(cellPosition);
                        }
                    }
                   

                    tileStates.Add(cellPosition, isBlocked);

                    if (isBlocked)
                        _isAreaEnable = false;
                }
            }
        }

    }
}
