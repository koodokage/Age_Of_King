using AgeOfKing.Components;
using AgeOfKing.Data;
using AgeOfKing.Systems;
using AgeOfKing.Systems.UI;
using UnityEngine;

namespace AgeOfKing.UI
{
    public class ProductionMenuInitializer : MonoBehaviour
    {
        [SerializeField] GameObject buttonPrefab;
        [SerializeField] RectTransform scrollContent;
        [SerializeField] InfiniteScroll infiniteScroll;

        public  void Launch(BuildingData[] buildings)
        {
            // Clean up first !
            int scrollContentChildCount = scrollContent.childCount;
            BuildingProduceButton buildingButton;

            while (scrollContentChildCount != 0)
            {
                var child = scrollContent.GetChild(0);
                if (child.TryGetComponent(out buildingButton))
                {
                    BuildingProduceButtonFactory.GetInstance.Release(buildingButton);
                }
                scrollContentChildCount--;

            }

            int scrollerRequestedCount =  infiniteScroll.GetRequestedAmount(buildings.Length, BuildingProduceButtonFactory.GetInstance.GetPrefabHeight);
            int loopIndexCounter = 0;

            while (scrollerRequestedCount != 0)
            {
               BuildingProduceButtonFactory.GetInstance.GetProducerUI(buildings[loopIndexCounter], scrollContent, TurnManager.GetInstance.GetTurnPlayer);

                scrollerRequestedCount--;
                loopIndexCounter++;
                if (loopIndexCounter >= buildings.Length)
                {
                    loopIndexCounter = 0;
                }

            }


            infiniteScroll.Launch();
        }

        public void OnPointerEnterProductionMenu()
        {
            TurnManager.GetInstance.GetTurnPlayer.CommandController.Release();
            UIManager.GetInstance.OnClickEmpty();
            UIManager.GetInstance.ChangeCursorTexture(Systems.UI.CursorMode.Regular);
        }

    }

}