using AgeOfKing.Data;

namespace AgeOfKing.Abstract.Components
{
    public interface IManufacturerBuilding
    {
        public ManufacturerBuildingData GetManufacturerData { get; }
        public void Produce(UnitData unitData);
    }

}