using AgeOfKing.Abstract.Components;
using AgeOfKing.Components;
using AgeOfKing.Data;
using AgeOfKing.UI;
using UnityEngine;

namespace AgeOfKing.Systems.UI
{
    public class UIManager : ASingleBehaviour<UIManager>
    {
        [SerializeField] ProductionMenuInitializer productionMenu;
        [SerializeField] EntityInfoGroup infoGroup;
        [SerializeField] TurnIconView turnIconView_Player1;
        [SerializeField] TurnIconView turnIconView_Player2;
        [SerializeField] GameObject kingdomSelectionPanel;

        private void Start()
        {
            kingdomSelectionPanel.SetActive(true);

            TurnManager.GetInstance.OnTurnPassToP1 += OnTurnPlayer1;
            TurnManager.GetInstance.OnTurnPassToP2 += OnTurnPlayer2;
            TurnManager.GetInstance.OnTurnChange += OnTurnChanged;

        }

        private void OnDisable()
        {
            TurnManager.GetInstance.OnTurnPassToP1 -= OnTurnPlayer1;
            TurnManager.GetInstance.OnTurnPassToP2 -= OnTurnPlayer2;
            TurnManager.GetInstance.OnTurnChange -= OnTurnChanged;

        }

        /// <summary>
        /// manage ui side operations
        /// </summary>
        /// <param name="building"></param>
        public  void OnClicked_BuildingButton(Data.BuildingData building,IPlayer player)
        {
            player.BuildingTileChecker.InitializeBuildingData(building);
            infoGroup.InitializeAndOpen(building);
        }

        public void OnUnitSelected(AUnit unit)
        {
            infoGroup.InitializeAndOpen(unit);
        }

        public void OnBuildingSelected(BuildingData building)
        {
            infoGroup.InitializeAndOpen(building);
        }

        public void OnManufacturerSelected(AManufacturerBuilding manufacturerBuilding)
        {
            infoGroup.GenerateAndOpen(manufacturerBuilding);
        }

        public void OnClickEmpty()
        {
            infoGroup.Close();
        }

        public void OnTurnChanged(IPlayer currentSide,int currentTurnIndex )
        {
            infoGroup.Close();
        }

        public void OnTurnPlayer1()
        {
            turnIconView_Player1.SetView(true);
            turnIconView_Player2.SetView(false);
        }

        public void OnTurnPlayer2()
        {
            turnIconView_Player1.SetView(false);
            turnIconView_Player2.SetView(true);
        }

        public void OnPlayerManagerInitialized(BuildingData[] buildings)
        {
            productionMenu.Launch(buildings);
            kingdomSelectionPanel.SetActive(false);
        }

    }


}
