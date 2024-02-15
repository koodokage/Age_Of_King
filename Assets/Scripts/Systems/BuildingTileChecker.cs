using AgeOfKing.Abstract.Components;
using AgeOfKing.Components;
using AgeOfKing.Datas;
using AgeOfKing.Systems.Input;
using UnityEngine;

namespace AgeOfKing.Systems
{
    public class BuildingTileChecker : SingleBehaviour<BuildingTileChecker>
    {
        BuildingData _buildingData;
        const int PlaygroundHorizontalLine = 0;

        bool _isAreaEnable = false;
        bool _isCheckerActive = false;

        private void OnEnable()
        {
            InputManager.OnMousePositionChanged += OnChange_MousePosition;
            InputManager.OnMouseLeftClicked += OnClicked_MouseLeft;
            InputManager.OnMouseRightClicked += OnClicked_MouseRight;
        }

        private void OnDisable()
        {
            InputManager.OnMousePositionChanged -= OnChange_MousePosition;
            InputManager.OnMouseLeftClicked -= OnClicked_MouseLeft;
            InputManager.OnMouseRightClicked -= OnClicked_MouseRight;

        }

        public void SetBuildingData(BuildingData data)
        {
            _buildingData = data;
            _isCheckerActive = true;
        }

        public void EndPlacement()
        {
            _isCheckerActive = false;
            _isAreaEnable = false;
            Maps.GetInstance.GetHighlightMap.ClearAllTiles();
        }

        private void OnClicked_MouseRight(Vector2 location)
        {
            if (_isCheckerActive == false)
                return;

            EndPlacement();
        }

        private void OnClicked_MouseLeft(Vector2 location)
        {
            if (_isCheckerActive == false)
                return;

            if (_isAreaEnable)
            {
                Vector3Int pointerCell = MapInput.GetCurrentMapCell;
                ABuilding buildingInstance = BuildingFactory.GetInstance.Produce(_buildingData);
                buildingInstance.Draw(pointerCell);
            }

            EndPlacement();
        }

        private void OnChange_MousePosition(Vector2 location)
        {
            if (_isCheckerActive == false)
                return;

            MapHighlighter hgSystem =  MapHighlighter.GetInstance;
            hgSystem.ClearAll();

            Vector3Int pointerCell = MapInput.GetCurrentMapCell;
            _isAreaEnable = true;
            bool isBlocked;

            for (int x = 0; x < _buildingData.GetXDimension; x++)
            {
                for (int y = 0; y < _buildingData.GetYDimension; y++)
                {
                    Vector3Int cellPosition = new Vector3Int(pointerCell.x + x, pointerCell.y + y, 0);
                    if(cellPosition.x >= PlaygroundHorizontalLine)
                    {
                        isBlocked = true;
                    }
                    else
                    {
                        isBlocked = MapEntityDataBase.GetInstance.IsTileBlocked(cellPosition);
                    }

                    if (isBlocked )
                        _isAreaEnable = false;

                    hgSystem.Highlight(cellPosition, isBlocked);
                }
            }
        }


    }
}
