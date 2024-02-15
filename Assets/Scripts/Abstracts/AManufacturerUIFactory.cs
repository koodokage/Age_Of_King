using AgeOfKing.Components;
using UnityEngine;
namespace AgeOfKing.Abstract.Components
{
    public abstract class AManufacturerUIFactory<T, K> : SingleBehaviour<AManufacturerUIFactory<T, K>> where T : AManufacturerBuilding where K : MonoBehaviour
    {
        public abstract K[] GetAllUnitProducerUI(T building);
    }

}