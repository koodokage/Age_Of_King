using AgeOfKing.UI;
using System.Collections;
using UnityEngine;

namespace AgeOfKing.Systems.UI
{
    public class UIManager : SingleBehaviour<UIManager>
    {
        [SerializeField] AProducibleUI[] Producables;

        private IEnumerator Start()
        {
            foreach (var IE in Producables)
            {
                IE.Initialize();
                yield return null;
            }
        }

        public void OnClicked_BuildingButton(Components.BuildingData building)
        {
            TilePlacer.GetInstance.InitializeBuildingData(building);
        }
    }
}
