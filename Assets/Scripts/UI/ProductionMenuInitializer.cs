using AgeOfKing.Abstract.UI;
using AgeOfKing.Components;
using AgeOfKing.Datas;
using UnityEngine;

namespace AgeOfKing.UI
{
    public class ProductionMenuInitializer : ALaunchableUI
    {
        [SerializeField] BuildingData[] buildings;
        [SerializeField] GameObject buttonPrefab;
        [SerializeField] RectTransform prefabParent;
        [SerializeField] InfiniteScroll infiniteScroll;

        public override void Launch()
        {
           int scrollerRequestedCount =  infiniteScroll.GetRequestedAmount(buildings.Length);
            int loopIndexCounter = 0;

            while (scrollerRequestedCount != 0)
            {
               BuildingProduceButtonFactory.GetInstance.GetProducerUI(buildings[loopIndexCounter], prefabParent);

                scrollerRequestedCount--;
                loopIndexCounter++;
                if (loopIndexCounter >= buildings.Length)
                {
                    loopIndexCounter = 0;
                }

            }


            infiniteScroll.Launch();
        }

    }

}