using AgeOfKing.Datas;

namespace AgeOfKing.Abstract.Components
{
    public interface IManufacturerBuilding
    {
        public ManufacturerBuildingData GetManufacturerData { get; }
        public void Produce(UnitData unitData);
    }

}