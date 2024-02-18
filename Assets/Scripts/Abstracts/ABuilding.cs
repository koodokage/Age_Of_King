using AgeOfKing.Data;
using AgeOfKing.Systems;
using AgeOfKing.Systems.UI;
using AgeOfKing.Utils;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AgeOfKing.Abstract.Components
{
    public abstract class ABuilding : AMapEntity<BuildingData>, ISelectable, IHittable, ITurnListener
    {
        BuildingData _buildinData;
        public BuildingData GetData { get => _buildinData; }

        protected Vector3Int _placedLocation;

        public override void InitializeData(BuildingData data,IPlayer player)
        {
            base.InitializeData(data, player);

            _buildinData = data;
            _baseData = data;
            owner = player;


            foreach (EntityStat stat in GetData.GetEntityStats)
            {
                if(stat.GetUsage == StatUsage.ONCE)
                    owner.GetVillage.IncreaseVillageData(stat.GetGenre, stat.GetValue);
            }

            owner.GetVillage.UseMoveRights();

        }

        private void OnEnable()
        {
            TurnManager.GetInstance.OnTurnChange += OnTurnChange;
        }

        private void OnDisable()
        {
            TurnManager.GetInstance.OnTurnChange -= OnTurnChange;
        }


        public override void Draw(Vector3Int pointerCell)
        {
            Tilemap targetMap = Map.GetInstance.GetBuildingMap;

            BuildingData buildingData = GetData;
            _placedLocation = pointerCell;

            for (int x = 0; x < buildingData.GetXDimension; x++)
            {
                for (int y = 0; y < buildingData.GetYDimension; y++)
                {
                    Vector3Int cellPosition = new Vector3Int(pointerCell.x + x, pointerCell.y + y, 0);

                    // add data to building obstacle
                    MapEntityDataBase.AddBuildingData(cellPosition, this);

                    //side sprite brush
                    targetMap.SetTile(cellPosition, buildingData.GetGroundTile);
                    targetMap.RefreshTile(cellPosition);

                    bool isMoveableRow = buildingData.IsRowMoveable(y,x);

                    if (isMoveableRow == false)
                    {
                        MapEntityDataBase.AddBuildingBlockedData(cellPosition,this);
                    }

                }
            }

            //main sprite brush
            pointerCell += buildingData.GetPlacementOffset;
            targetMap.SetTile(pointerCell, buildingData.GetTile);
            targetMap.RefreshTile(pointerCell);
        }

        public override void Erase()
        {
            Tilemap targetMap = Map.GetInstance.GetBuildingMap;
            BuildingData buildingData = GetData;

            for (int x = 0; x < buildingData.GetXDimension; x++)
            {
                for (int y = 0; y < buildingData.GetYDimension; y++)
                {
                    Vector3Int cellPosition = new Vector3Int(_placedLocation.x + x, _placedLocation.y + y, 0);

                    // add data to building obstacle
                    MapEntityDataBase.RemoveBuildingData(cellPosition);

                    //side sprite brush
                    targetMap.SetTile(cellPosition, null);
                    targetMap.RefreshTile(cellPosition);

                    bool isMoveableRow = buildingData.IsRowMoveable(y, x);

                    if (isMoveableRow == false)
                    {
                        MapEntityDataBase.RemoveBuildingBlockedData(cellPosition);
                    }

                }
            }

            Vector3Int pointerCell = _placedLocation + GetData.GetPlacementOffset;
            MapEntityDataBase.RemoveBuildingData(pointerCell);
            MapEntityDataBase.RemoveBuildingBlockedData(pointerCell);
            targetMap.SetTile(pointerCell,null);
            targetMap.RefreshTile(pointerCell);

            foreach (EntityStat stat in GetData.GetEntityStats)
            {
                if (stat.GetUsage == StatUsage.ONCE)
                    owner.GetVillage.DecreaseVillageData(stat.GetGenre, stat.GetValue);
            }

            base.Erase();
        }

        public virtual void OnSelected()
        {
            if(owner == TurnManager.GetInstance.GetTurnPlayer)
            {
                UIManager.GetInstance.OnBuildingSelected(GetData);
                StartFlickerAnimation();
            }
        }

        protected void StartFlickerAnimation()
        {
            Vector3Int pointerCell = _placedLocation + GetData.GetPlacementOffset;
            StartCoroutine(FlickerAnimation(Map.GetInstance.GetBuildingMap, Color.gray, pointerCell));
        }

        public bool Hit(int damage)
        {
            bool isDestroyed = false;

            currentHealth -= damage;

            isDestroyed = currentHealth <= 0 ? true : false;

            Action onEnd = null;
            if (currentHealth <= 0)
            {
                onEnd = Erase;
            }

            Vector3Int pointerCell = _placedLocation + GetData.GetPlacementOffset;
            StartCoroutine(FlickerAnimation(Map.GetInstance.GetBuildingMap,Color.red, pointerCell, onEnd));

            return isDestroyed;
        }



        public virtual void OnTurnChange(IPlayer side, int turnIndex)
        {
            if (side == owner)
            {
                foreach (EntityStat stat in GetData.GetEntityStats)
                {
                    if (stat.GetUsage == StatUsage.PERTURN)
                        owner.GetVillage.IncreaseVillageData(stat.GetGenre, stat.GetValue);
                }

                return;
            }
        }
    }


}
