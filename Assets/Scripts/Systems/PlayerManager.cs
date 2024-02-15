using AgeOfKing.Data;
using UnityEngine;

namespace AgeOfKing.Systems
{
    public class PlayerManager : SingleBehaviour<PlayerManager>
    {
        [SerializeField] AModelData goldModelData;
        [SerializeField] APopulationModelData populationModelData;
        [SerializeField] AModelData moveRightsModeData;

        private void Start()
        {
            //Initialize game start values (add on SO)
            goldModelData.SetValue(goldModelData.GetValue);
            moveRightsModeData.SetValue(moveRightsModeData.GetValue);
            populationModelData.SetValue(populationModelData.GetValue);
            populationModelData.SetCapacity(populationModelData.GetCapacity);
        }

        public bool IsGoldEnough(int requested)
        {
            return goldModelData.CheckEnough(requested);
        }

        public bool TryPurhcaseWithGold(int requested)
        {
            bool state = goldModelData.CheckEnough(requested);

            if (state)
            {
                PurchaseWithGold(requested);
            }

            return state;
        }

        public bool IsPopulationEnough(int requested)
        {
            return populationModelData.CheckEnough(requested);
        }

        public void ChangePopulation(int changeAmount)
        {
            if (changeAmount > 0)
            {
                populationModelData.Increase(changeAmount);
            }
            else if (changeAmount < 0)
            {
                populationModelData.Decrease(changeAmount);
            }
        }

        public void ChangePopulationCapacity(int changeAmount)
        {
            if (changeAmount > 0)
            {
                populationModelData.IncreaseCapacity(changeAmount);
            }
            else if (changeAmount < 0)
            {
                populationModelData.DecreaseCapacity(changeAmount);
            }
        }

        void PurchaseWithGold(int changeAmount)
        {
            goldModelData.Decrease(changeAmount);
        }

        public void AddGold(int changeAmount)
        {
            goldModelData.Increase(changeAmount);
        }

    }

}