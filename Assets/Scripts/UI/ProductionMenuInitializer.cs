using AgeOfKing.Components;
using UnityEngine;

namespace AgeOfKing.UI
{
    public class ProductionMenuInitializer : AProducibleUI
    {
        [SerializeField] BuildingData[] buildings;
        [SerializeField] GameObject buttonPrefab;
        [SerializeField] RectTransform prefabParent;
        [SerializeField] AProducibleUI infiniteScroll;

        public override void Initialize()
        {
            for (int i = 0; i < buildings.Length; i++)
            {
                var instance = Instantiate(buttonPrefab, prefabParent);
                instance.GetComponent<BuildingButton>().Initialize(buildings[i]);
            }

            infiniteScroll.Initialize();
        }

    }

}