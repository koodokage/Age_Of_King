using AgeOfKing.Abstract.Components;
using AgeOfKing.Data;
using UnityEngine;

namespace AgeOfKing.Components
{
    public class Barrack : AManufacturerBuilding
    {
        public override void Produce(UnitData unitData)
        {
            if(TryGetSpawnLocation(out Vector3Int cellLocation))
            {
                AUnit unit = UnitFactory.GetInstance.Produce(unitData,owner);
                unit.Draw(cellLocation);
                unit.MoveTo(cellLocation);
                MapEntityDataBase.AddUnitData(cellLocation, unit);
            }
            else
            {
                //is fully warn player for needed space move produced units
                Debug.LogWarning("PRODUCE ARE IS FULL !");
            }
        }



    }


}