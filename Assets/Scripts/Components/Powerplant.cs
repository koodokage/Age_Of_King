using AgeOfKing.Abstract.Components;
using AgeOfKing.Data;
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

        
    }


}