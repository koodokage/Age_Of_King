using AgeOfKing.Abstract.Components;
using AgeOfKing.Data;
using AgeOfKing.Systems;
using UnityEngine;

namespace AgeOfKing.Components
{
    public class BuildingFactory : AEntityFactory<BuildingData,ABuilding>
    {
        public override ABuilding Produce(BuildingData entityData,IPlayer player)
        {
            player.GetVillage.TryPurhcaseWith_Gold(entityData.GetPrice);
            ABuilding buildingInstance = GameObject.Instantiate(entityData.GetBuildingPrefab);
            buildingInstance.InitializeData(entityData, player);
            return buildingInstance;
        }
    }
}
