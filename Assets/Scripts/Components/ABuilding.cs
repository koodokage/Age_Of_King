using AgeOfKing.Datas;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AgeOfKing.Components
{
    public abstract class ABuilding : MonoBehaviour
    {
        BuildingData _data;
        public Vector3Int BottomLeftCorner;

        public BuildingData GetData { get => _data; }

        public virtual void Initialize(BuildingData data)
        {
            _data = data;
        }

        public virtual void UpdateObstacleArea(Tilemap targetMap, Vector3Int pointerCell)
        {
            BottomLeftCorner= new Vector3Int(pointerCell.x,  pointerCell.y, 0);

            for (int x = 0; x < _data.XDimension; x++)
            {
                for (int y = 0; y < _data.YDimension; y++)
                {
                    Vector3Int cellPosition = new Vector3Int(pointerCell.x + x, pointerCell.y + y, 0);

                    // add data to building obstacle
                    MapEntityData.GetInstance.AddBuildingData(cellPosition,this);

                    //side sprite brush
                    targetMap.SetTile(cellPosition, _data.GetGroundTile);
                    targetMap.RefreshTile(cellPosition);

                    bool moveableRow = _data.IsRowMoveable(y);

                    if (moveableRow == true)
                        continue;

                    // add data to movement obstacle

                    MapEntityData.GetInstance.AddObstacleData(cellPosition);

                }
            }

            //main sprite brush
            pointerCell += _data.GetPlacementOffset;
            targetMap.SetTile(pointerCell, _data.GetTile);
            targetMap.RefreshTile(pointerCell);
        }
    }
}
