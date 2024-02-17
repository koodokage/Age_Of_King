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
            _unitData = data;
            _baseData = data;
            owner = player;

            TurnManager.GetInstance.OnTurnChange += OnTurnChange;

            foreach (EntityStat stat in GetData.GetEntityStats)
            {
                if (stat.GetUsage == StatUsage.ONCE)
                    player.GetVillage.IncreaseVillageData(stat.GetGenre, stat.GetValue);
            }

        }

        public override void Draw(Vector3Int cellLocation)
        {
            // example : setup some tile occupation with dimension
            Map.GetInstance.GetUnitMap.SetTile(cellLocation, _unitData.GetTile);
        }

        public virtual void MoveTo(Vector3Int cellLocation,int movementCost = 0)
        {
            OnTileExit();
            _currentMovePoint -= movementCost;
            _currentCellLocation = cellLocation;
            MapEntityDataBase.AddUnitData(_currentCellLocation,this);
            Map.GetInstance.GetUnitMap.SetTile(_currentCellLocation, _unitData.GetTile);
        }

        protected void OnTileExit()
        {
            Map.GetInstance.GetUnitMap.SetTile(_currentCellLocation, null);
            MapEntityDataBase.RemoveUnitData(_currentCellLocation);
        }

        public virtual void OnTurnChange(IPlayer side, int turnIndex)
        {
            if (owner == side)
            {
                GetData.TryGetValueByGenre(CharacterStatGenre.MOVEMENT, out _currentMovePoint);
            }

        }
    }

}
