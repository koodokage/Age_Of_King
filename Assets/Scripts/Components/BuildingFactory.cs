using AgeOfKing.Abstract.Components;
using AgeOfKing.Datas;
using AgeOfKing.Systems;
using UnityEngine;

namespace AgeOfKing.Components
{
    public class BuildingFactory : AEntityFactory<BuildingData,ABuilding>
    {
        public override ABuilding Produce(BuildingData entityData)
        {
            ABuilding buildingInstance = GameObject.Instantiate(entityData.GetBuildingPrefab);
            buildingInstance.InitializeData(entityData);
            PlayerManager.GetInstance.TryPurhcaseWithGold(entityData.GetPrice);
            return buildingInstance;
        }
    }
}
