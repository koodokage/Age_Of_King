using AgeOfKing.Components;
using AgeOfKing.Datas;
using AgeOfKing.Systems.Input;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AgeOfKing.Systems
{
    public class TilePlacer : SingleBehaviour<TilePlacer>
    {

        BuildingData _buildingData;

        [Header("Dependencies")]
        [SerializeField] Tilemap highlightMap;
        [SerializeField] Tilemap groundMap;

        [Header("Highlight")]
        [SerializeField] Tile tile;
        [SerializeField] Color blockedTile;
        [SerializeField] Color placeableTile;

        bool _isAreaEnable = false;


        public void InitializeBuildingData(BuildingData data)
        {
            _buildingData = data;
            InputManager.OnMousePositionChanged += OnChange_MousePosition;
            InputManager.OnMouseLeftClicked += OnClicked_MouseLeft;
        }

        public void EndPlacement()
        {
            InputManager.OnMouseLeftClicked -= OnClicked_MouseLeft;
            InputManager.OnMousePositionChanged -= OnChange_MousePosition;
            _buildingData = null;
            _isAreaEnable = false;
            highlightMap.ClearAllTiles();
        }

        private void OnClicked_MouseLeft(Vector2 location)
        {
            if (_isAreaEnable)
            {
                Vector3Int pointerCell = GridInput.GetCurrentMapCell;
                ABuilding buildingInstance = _buildingData.CreateAndInitialize();

                for (int x = 0; x < _buildingData.XDimension; x++)
                {
                    for (int y = 0; y < _buildingData.YDimension; y++)
                    {
                        Vector3Int cellPosition = new Vector3Int(pointerCell.x + x, pointerCell.y + y, 0);

                        // add data to building obstacle
                        buildingInstance.UpdateObstacleArea(groundMap,cellPosition);
                    }
                }

            }

            EndPlacement();
        }

        private void OnChange_MousePosition(Vector2 location)
        {
            if (_buildingData == null)
            {
                EndPlacement();
                return;
            }

            highlightMap.ClearAllTiles();
            _isAreaEnable = true;

            Vector3Int pointerCell = GridInput.GetCurrentMapCell;
            for (int x = 0; x < _buildingData.XDimension; x++)
            {
                for (int y = 0; y < _buildingData.YDimension; y++)
                {
                    Vector3Int cellPosition = new Vector3Int(pointerCell.x + x, pointerCell.y + y, 0);

                    bool anyBlock = MapEntityData.GetInstance.IsPlaceBlocked(cellPosition);
                    if (anyBlock)
                    {
                        highlightMap.SetTile(cellPosition, tile);
                        highlightMap.SetTileFlags(cellPosition, TileFlags.None);
                        highlightMap.SetColor(cellPosition, blockedTile);
                        _isAreaEnable = false;
                        continue;
                    }


                    highlightMap.SetTile(cellPosition, tile);
                    highlightMap.SetTileFlags(cellPosition, TileFlags.None);
                    highlightMap.SetColor(cellPosition, placeableTile);

                }
            }
        }

        private void OnDisable()
        {
            //Release inputs
            if (_buildingData != null)
            {
                InputManager.OnMousePositionChanged -= OnChange_MousePosition;
                InputManager.OnMouseLeftClicked -= OnClicked_MouseLeft;
            }
        }
    }
}
