using AgeOfKing.Data;
using AgeOfKing.UI;
using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace AgeOfKing.Systems
{
    public class ModelViewController : ASingleBehaviour<ModelViewController>
    {

        public delegate void  OnVillageDataChange(VillageData villageData);
        public OnVillageDataChange OnPlayerVillageDataChanged;

        [Header("GOLD")]
        [SerializeField] AVillageDataView goldDataView;
        [SerializeField] UnityEvent OnGoldIncome;
        [SerializeField] UnityEvent OnGoldExpense;

        [Header("MOVERIGHTS")]
        [SerializeField] AVillageDataView moveRightPocketView;
        [SerializeField] UnityEvent OnMoveRightsUsed;
        [SerializeField] UnityEvent OnMoveRightsBack;

        [Header("POPULATION")]
        [SerializeField] AVillageDataView populationDataView;
        [SerializeField] UnityEvent OnPopulationIncrease;
        [SerializeField] UnityEvent OnPopulationDecrease;

        [Header("POP. CAPACITY")]
        [SerializeField] AVillageDataView populationCapacityDataView;
        [SerializeField] UnityEvent OnPopulationCapacityIncrease;
        [SerializeField] UnityEvent OnPopulationCapacityDecrease;

        [Header("KING RIGHTS")]
        [SerializeField] AVillageDataView kingRightDataView;
        [SerializeField] UnityEvent OnKingRightsDecrease;


        #region POPULATIONS

        public void BindModelView_Population(AVillageDataModel villageDataModel)
        {
            villageDataModel.OnChange += PopulationVillageDataModel_OnChange;
        }

        private void PopulationVillageDataModel_OnChange(VillageData obj, AVillageDataModel.DataModelChangeWay changeWay)
        {
            populationDataView.VillageDataChanged(obj);
            OnPlayerVillageDataChanged?.Invoke(obj);

            if (changeWay == AVillageDataModel.DataModelChangeWay.INCREASE)
            {
                OnPopulationIncrease?.Invoke();
            }
            else
            {
                OnPopulationDecrease?.Invoke();
            }
        }


        public void BindModelView_PopulationCapacity(AVillageDataModel villageDataModel)
        {
            villageDataModel.OnChange += PopulationCapacityVillageDataModel_OnChange;
        }



        private void PopulationCapacityVillageDataModel_OnChange(VillageData obj, AVillageDataModel.DataModelChangeWay changeWay)
        {
            populationCapacityDataView.VillageDataChanged(obj);
            OnPlayerVillageDataChanged?.Invoke(obj);

            if (changeWay == AVillageDataModel.DataModelChangeWay.INCREASE)
            {
                OnPopulationCapacityIncrease?.Invoke();
            }
            else
            {
                OnPopulationCapacityDecrease?.Invoke();
            }
        }

        #endregion

        #region MOVERIGHTS

        public void BindModelView_MoveRights(AVillageDataModel villageDataModel)
        {
            villageDataModel.OnChange += MoveRightsVillageDataModel_OnChange;
        }

        public void BindModelView_KingMoveRights(AVillageDataModel villageDataModel)
        {
            villageDataModel.OnChange += KingMoveRightsVillageDataModel_OnChange;
        }


        private void MoveRightsVillageDataModel_OnChange(VillageData obj,AVillageDataModel.DataModelChangeWay changeWay)
        {
            moveRightPocketView.VillageDataChanged(obj);
            OnPlayerVillageDataChanged?.Invoke(obj);

            if(changeWay == AVillageDataModel.DataModelChangeWay.INCREASE)
            {
                OnMoveRightsUsed?.Invoke();
            }
            else
            {
                OnMoveRightsBack?.Invoke();
            }
        }

        private void KingMoveRightsVillageDataModel_OnChange(VillageData obj, AVillageDataModel.DataModelChangeWay changeWay)
        {
            kingRightDataView.VillageDataChanged(obj);

            if (changeWay == AVillageDataModel.DataModelChangeWay.DECREASE)
            {
                OnKingRightsDecrease?.Invoke();
            }
        }

        #endregion

        #region GOLD

        public void BindModelView_Gold(AVillageDataModel villageDataModel)
        {
            villageDataModel.OnChange += GoldVillageDataModel_OnChange;
        }


        private void GoldVillageDataModel_OnChange(VillageData obj, AVillageDataModel.DataModelChangeWay changeWay)
        {
            goldDataView.VillageDataChanged(obj);
            OnPlayerVillageDataChanged?.Invoke(obj);

            if (changeWay == AVillageDataModel.DataModelChangeWay.INCREASE)
            {
                OnGoldIncome?.Invoke();
            }
            else
            {
                OnGoldExpense?.Invoke();
            }
        }

        #endregion
    }

}
