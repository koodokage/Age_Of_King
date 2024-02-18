using AgeOfKing.Data;
using AgeOfKing.Systems;
using UnityEngine;

namespace AgeOfKing.Abstract.Components
{
    /// <summary>
    /// Moveable entity on map
    /// </summary>
    public abstract class AUnit : AMapEntity<UnitData>, ITurnListener
    {
        protected UnitData _unitData;
        public UnitData GetData { get => _unitData; }

        protected int _currentMovePoint;
        public int CurrentMovePoint { get => _currentMovePoint; }


        public override void InitializeData(UnitData data, IPlayer player)
        {
            base.InitializeData(data,player);
            _unitData = data;
            _baseData = data;
            owner = player;

            foreach (EntityStat stat in GetData.GetEntityStats)
            {
                if (stat.GetUsage == StatUsage.ONCE)
                    player.GetVillage.IncreaseVillageData(stat.GetGenre, stat.GetValue);
            }

        }

        private void OnEnable()
        {
            TurnManager.GetInstance.OnTurnChange += OnTurnChange;
        }

        private void OnDisable()
        {
            TurnManager.GetInstance.OnTurnChange += OnTurnChange;
        }

        public override void Draw(Vector3Int cellLocation)
        {
            // example : setup some tile occupation with dimension
            Map.GetInstance.GetUnitMap.SetTile(cellLocation, _unitData.GetTile);
            MapEntityDataBase.AddUnitData(cellLocation, this);
        }

        public override void Erase()
        {
            MapEntityDataBase.RemoveUnitData(currentCellLocation);

            foreach (EntityStat stat in GetData.GetEntityStats)
            {
                if (stat.GetUsage == StatUsage.ONCE)
                    owner.GetVillage.DecreaseVillageData(stat.GetGenre, stat.GetValue);
            }

            base.Erase();
        }

        public virtual void MoveTo(Vector3Int cellLocation,int movementCost = 0)
        {
            OnTileExit();
            _currentMovePoint -= movementCost;
            currentCellLocation = cellLocation;
            MapEntityDataBase.AddUnitData(currentCellLocation,this);
            Map.GetInstance.GetUnitMap.SetTile(currentCellLocation, _unitData.GetTile);
        }

        protected void OnTileExit()
        {
            Map.GetInstance.GetUnitMap.SetTile(currentCellLocation, null);
            MapEntityDataBase.RemoveUnitData(currentCellLocation);
        }

        public abstract void OnTurnChange(IPlayer side, int turnIndex);
    }

}
