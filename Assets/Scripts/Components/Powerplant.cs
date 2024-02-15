using AgeOfKing.Abstract.Components;
using AgeOfKing.Datas;
using UnityEngine;

namespace AgeOfKing.Components
{
    public class Powerplant : ABuilding , IResource
    {
        [SerializeField] string resourceLabel;
        [SerializeField] int resourceAmount;

        public string GetName => resourceLabel;

        public void Collect()
        {

        }

        public int Get()
        {
            return resourceAmount;
        }

        public override void InitializeData(BuildingData data)
        {
            base.InitializeData(data);

            // Add population
        }
    }


    public struct BuildingStat 
    {
        
    }

}