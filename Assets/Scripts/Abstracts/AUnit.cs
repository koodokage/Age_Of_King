using AgeOfKing.Datas;
using UnityEngine;

namespace AgeOfKing.Abstract.Components
{
    public abstract class AUnit : AMapEntity
    {
        protected UnitData _unitData;
        public UnitData GetData { get => _unitData; }

        private Vector3Int _currentCellLocation;
        public Vector3Int GetCurrentCellLocation { get => _currentCellLocation;}

        public virtual void InitializeData(UnitData data)
        {
            _unitData = data;
            _baseData = data;
        }

        public override void Draw(Vector3Int cellLocation)
        {
            // example : setup some tile occupation with dimension
            Maps.GetInstance.GetUnitMap.SetTile(cellLocation, _unitData.GetTile);
        }

        public virtual void MoveTo(Vector3Int cellLocation)
        {
            OnTileExit();
            _currentCellLocation = cellLocation;
            MapEntityDataBase.GetInstance.AddUnitData(_currentCellLocation,this);
            Maps.GetInstance.GetUnitMap.SetTile(_currentCellLocation, _unitData.GetTile);

        }

        protected void OnTileExit()
        {
            Maps.GetInstance.GetUnitMap.SetTile(_currentCellLocation, null);
            MapEntityDataBase.GetInstance.RemoveUnitData(_currentCellLocation);
        }

    }

}
