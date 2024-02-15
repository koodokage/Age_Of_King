using AgeOfKing.Datas;
using AgeOfKing.Systems.UI;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AgeOfKing.Abstract.Components
{
    public abstract class ABuilding : AMapEntity, ISelectable, IHittable
    {
        BuildingData _buildinData;
        public BuildingData GetData { get => _buildinData; }
        int _currentHealth;
        public int CurrentHealth { get => _currentHealth; }

        protected Vector3Int _placedLocation;

        public virtual void InitializeData(BuildingData data)
        {
            _buildinData = data;
            _baseData = data;
        }

        public override void Draw(Vector3Int pointerCell)
        {
            Tilemap targetMap = Maps.GetInstance.GetBuildingMap;

            BuildingData buildingData = GetData;
            _placedLocation = pointerCell;

            for (int x = 0; x < buildingData.GetXDimension; x++)
            {
                for (int y = 0; y < buildingData.GetYDimension; y++)
                {
                    Vector3Int cellPosition = new Vector3Int(pointerCell.x + x, pointerCell.y + y, 0);

                    // add data to building obstacle
                    MapEntityDataBase.GetInstance.AddBuildingData(cellPosition, this);

                    //side sprite brush
                    targetMap.SetTile(cellPosition, buildingData.GetGroundTile);
                    targetMap.RefreshTile(cellPosition);

                    bool isMoveableRow = buildingData.IsRowMoveable(y);

                    if (isMoveableRow == false)
                    {
                        MapEntityDataBase.GetInstance.AddBuildingExtrudeData(cellPosition);
                    }

                }
            }

            //main sprite brush
            pointerCell += buildingData.GetPlacementOffset;
            targetMap.SetTile(pointerCell, buildingData.GetTile);
            targetMap.RefreshTile(pointerCell);
        }

        public virtual void OnSelected()
        {
            UIManager.GetInstance.OnBuildingSelected(this);
        }

        public bool Hit(int damage)
        {
            _currentHealth -= damage;

            if (_currentHealth < 0)
            {
                return true;
            }
            return false;
        }
    }


}
