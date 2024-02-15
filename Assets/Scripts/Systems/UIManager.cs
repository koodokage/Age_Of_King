using AgeOfKing.Abstract.Components;
using AgeOfKing.Abstract.UI;
using AgeOfKing.Components;
using AgeOfKing.Datas;
using AgeOfKing.UI;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace AgeOfKing.Systems.UI
{
    public class UIManager : SingleBehaviour<UIManager>
    {
        [SerializeField] ALaunchableUI[] launchables;
        [SerializeField] EntityInfoGroup infoGroup;


        private IEnumerator Start()
        {
            foreach (var IE in launchables)
            {
                IE.Launch();
                yield return null;
            }
        }



        public  void OnClicked_BuildingButton(Datas.BuildingData building)
        {
            BuildingTileChecker.GetInstance.SetBuildingData(building);
        }

        public void OnUnitSelected(AUnit unit)
        {
            infoGroup.Open(unit);
        }

        public void OnBuildingSelected(ABuilding building)
        {
            infoGroup.Open(building);
        }

        public void OnManufacturerSelected(AManufacturerBuilding manufacturerBuilding)
        {
            infoGroup.Open(manufacturerBuilding);
        }

        internal void OnClickEmpty()
        {
            infoGroup.Close();
        }

       
    }
}
