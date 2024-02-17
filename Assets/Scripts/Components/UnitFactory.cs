using AgeOfKing.Abstract.Components;
using AgeOfKing.Data;
using AgeOfKing.Systems;

namespace AgeOfKing.Components
{
    public class UnitFactory : AEntityFactory<UnitData,AUnit>
    {
        public override AUnit Produce(UnitData entityData,IPlayer player)
        {
            player.GetVillage.TryPurhcaseWith_Gold(entityData.GetPrice);
            AUnit unitInstance = Instantiate(entityData.GetPrefab);
            unitInstance.InitializeData(entityData, player);
            return unitInstance;
        }
    }

}
