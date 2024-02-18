using AgeOfKing.Abstract.Components;
using AgeOfKing.Data;
using AgeOfKing.Systems;
using AgeOfKing.Systems.UI;
using UnityEngine;

namespace AgeOfKing.Components
{
    public abstract class AManufacturerBuilding : ABuilding, IManufacturerBuilding
    {
        [SerializeField] protected ManufacturerBuildingData manufacturerData;
        public ManufacturerBuildingData GetManufacturerData { get => manufacturerData; }

        public abstract void Produce(UnitData unitData);

        public override void OnSelected()
        {
            if (owner == TurnManager.GetInstance.GetTurnPlayer)
            {
                UIManager.GetInstance.OnManufacturerSelected(this);
                StartFlickerAnimation();
            }
        }

        protected bool TryGetSpawnLocation(out Vector3Int spawnLocation)
        {
            int[] spawnLocationIndexs = manufacturerData.GetSpawnableRowIndexs;
            int xdim = GetData.GetXDimension;
            spawnLocation = _placedLocation;
            for (int i = 0; i < spawnLocationIndexs.Length; i++)
            {
                int index = spawnLocationIndexs[i];
                spawnLocation.y += index;

                for (int x = 0; x < xdim; x++)
                {
                    // Check is valid
                    if (MapEntityDataBase.IsTileSpawnable(spawnLocation))
                    {
                        return true;
                    }
                    spawnLocation.x += 1;
                }

                spawnLocation = _placedLocation;
            }

            return false;
        }





    }
}