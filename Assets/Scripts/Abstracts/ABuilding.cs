using AgeOfKing.Data;
using AgeOfKing.Systems;
using AgeOfKing.Systems.UI;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AgeOfKing.Abstract.Components
{
    public abstract class ABuilding : AMapEntity<BuildingData>, ISelectable, IHittable, ITurnListener
    {
        BuildingData _buildinData;
        public BuildingData GetData { get => _buildinData; }
        int _currentHealth;
        public int CurrentHealth { get => _currentHealth; }

        protected Vector3Int _placedLocation;

        public override void InitializeData(BuildingData data,IPlayer player)
        {
            _buildinData = data;
            _baseData = data;
            owner = player;

            TurnManager.GetInstance.OnTurnChange += OnTurnChange;

            foreach (EntityStat stat in GetData.GetEntityStats)
            {
                if(stat.GetUsage == StatUsage.ONCE)
                    owner.GetVillage.IncreaseVillageData(stat.GetGenre, stat.GetValue);
            }

            owner.GetVillage.UseMoveRights();

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
                        Debug.Log($"CORE BLOCK {x},{y}");
                        MapEntityDataBase.AddBuildingBlockedData(cellPosition,this);
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
            if(owner == TurnManager.GetInstance.GetTurnPlayer)
                 UIManager.GetInstance.OnBuildingSelected(GetData);
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
