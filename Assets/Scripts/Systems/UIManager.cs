using AgeOfKing.Abstract.Components;
using AgeOfKing.Components;
using AgeOfKing.Data;
using AgeOfKing.UI;
using System;
using UnityEngine;

namespace AgeOfKing.Systems.UI
{
    [Serializable]
    public struct CursorTexture
    {
        public Texture2D regular;
        public Texture2D attack;
        public Texture2D move;
    }

    public enum CursorMode
    {
        Regular,
        Move,
        Attack
    }


    public class UIManager : ASingleBehaviour<UIManager>
    {
        [SerializeField] ProductionMenuInitializer productionMenu;
        [SerializeField] EntityInfoGroup infoGroup;
        [SerializeField] TurnIconView turnIconView_Player1;
        [SerializeField] TurnIconView turnIconView_Player2;
        [SerializeField] GameObject kingdomSelectionPanel;
        [SerializeField] GameOverScreen gameOverScreen;
        [SerializeField] ToolTip toolTip;
        [SerializeField] TurnChangePanel turnChangePanel;
        [SerializeField] CursorTexture cursorTextures;

        private void Start()
        {
            kingdomSelectionPanel.SetActive(true);

            TurnManager.GetInstance.OnTurnPassToP1 += OnTurnPlayer1;
            TurnManager.GetInstance.OnTurnPassToP2 += OnTurnPlayer2;
            TurnManager.GetInstance.OnTurnChange += OnTurnChanged;

            GameManager.GetInstance.OnPlayerWin.AddListener(OnPlayerWin);

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
        public void OnClicked_BuildingButton(Data.BuildingData building, IPlayer player)
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

        public void OnTurnChanged(IPlayer currentSide, int currentTurnIndex)
        {
            infoGroup.Close();
            turnChangePanel.OnTurnChange(currentSide,currentTurnIndex);
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

        public void OnPlayerManagerInitialized(IPlayer player)
        {
            productionMenu.Launch(player.GetKingdomPreset.GetKingdomBuildings);
            kingdomSelectionPanel.SetActive(false);
        }

        void OnPlayerWin(IPlayer player)
        {
            gameOverScreen.Open(player);
        }


        public void ChangeCursorTexture(CursorMode mode)
        {
            Vector2 hotspot;
            Texture2D texture;

            switch (mode)
            {
                case CursorMode.Regular:
                    texture = cursorTextures.regular;
                    break;
                case CursorMode.Move:
                    texture = cursorTextures.move;
                    break;
                case CursorMode.Attack:
                    texture = cursorTextures.attack;
                    break;
                default:
                    texture = cursorTextures.regular;
                    break;
            }

            hotspot = new Vector2(texture.width / 2, texture.height / 2);
            Cursor.SetCursor(texture, hotspot, UnityEngine.CursorMode.Auto);
            toolTip.Close();
        }

        public void ChangeCursorTexture(CursorMode mode, Vector2 cursorLocation, IHittable hittable)
        {
            Vector2 hotspot;
            Texture2D texture;

            switch (mode)
            {
                case CursorMode.Regular:
                    texture = cursorTextures.regular;
                    break;
                case CursorMode.Move:
                    texture = cursorTextures.move;
                    break;
                case CursorMode.Attack:
                    texture = cursorTextures.attack;
                    break;
                default:
                    texture = cursorTextures.regular;
                    break;
            }

            hotspot = new Vector2(texture.width / 2, texture.height / 2);
            Cursor.SetCursor(texture, hotspot, UnityEngine.CursorMode.Auto);
            toolTip.GetTooltip(cursorLocation, hittable);
        }

    }


}
