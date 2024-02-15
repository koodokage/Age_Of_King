using AgeOfKing.Abstract.Components;
using AgeOfKing.Datas;
using AgeOfKing.Systems;

namespace AgeOfKing.Components
{
    public class UnitFactory : AEntityFactory<UnitData,AUnit>
    {
        public override AUnit Produce(UnitData entityData)
        {
            PlayerManager.GetInstance.TryPurhcaseWithGold(entityData.GetPrice);
            AUnit unitInstance = Instantiate(entityData.GetPrefab);
            unitInstance.InitializeData(entityData);
            return unitInstance;
        }
    }

}
