using AgeOfKing.Components;
using AgeOfKing.Data;
using AgeOfKing.Systems.UI;
using System.Collections.Generic;
using UnityEngine;

namespace AgeOfKing.Systems
{
    public class Village 
    {
        public readonly IPlayer Owner;
        public readonly VillageData villageData;
        public readonly KingdomPreset kingdomPreset;

        protected AVillageDataModel _goldDataModel;
        protected AVillageDataModel _populationDataModel;
        protected AVillageDataModel _populationCapacityDataModel;
        protected AVillageDataModel _moveRightsDataModel;
        protected AVillageDataModel _kingMoveRightsDataModel;
        protected Dictionary<ValueGenre, AVillageDataModel> dataModels;

        public Village(IPlayer ownerPlayer,KingdomPreset preset)
        {
            Owner = ownerPlayer;

            kingdomPreset = preset;

            villageData = new VillageData(kingdomPreset.GetGold, 0, kingdomPreset.GetPopulationCapacity, kingdomPreset.GetMoveRights, kingdomPreset.GetKingMoveRights);

            _goldDataModel = new GoldDataModel(villageData, ValueGenre.GOLD);
            _populationDataModel = new PopulationDataModel(villageData, ValueGenre.POPULATION);
            _populationCapacityDataModel = new PopulationCapacityDataModel(villageData, ValueGenre.POPCAPACITY);
            _moveRightsDataModel = new MoveRightsDataModel(villageData, ValueGenre.MOVERIGHTS);
            _kingMoveRightsDataModel = new KingMoveRightsDataModel(villageData, ValueGenre.KINGMOVERIGHTS);

            dataModels = new Dictionary<ValueGenre, AVillageDataModel>();

            dataModels.Add(ValueGenre.GOLD, _goldDataModel);
            dataModels.Add(ValueGenre.POPULATION, _populationDataModel);
            dataModels.Add(ValueGenre.POPCAPACITY, _populationCapacityDataModel);
            dataModels.Add(ValueGenre.MOVERIGHTS, _moveRightsDataModel);
            dataModels.Add(ValueGenre.KINGMOVERIGHTS, _kingMoveRightsDataModel);

            TurnManager.GetInstance.RegisterPlayer(Owner, OnTurnPassToVillage);

       
        }

        public void BindUI_PlayerVillage()
        {
            BindModeViewController();
            UIManager.GetInstance.OnPlayerManagerInitialized(kingdomPreset.GetKingdomBuildings);
        }

        public void UnbindUI_PlayerVillage()
        {
            _goldDataModel.ClearBindings();
            _populationDataModel.ClearBindings();
            _populationCapacityDataModel.ClearBindings();
            _moveRightsDataModel.ClearBindings();
        }


        public void BindModeViewController()
        {

            ModelViewController.GetInstance.BindModelView_Gold(_goldDataModel);

            ModelViewController.GetInstance.BindModelView_Population(_populationDataModel);

            ModelViewController.GetInstance.BindModelView_PopulationCapacity(_populationCapacityDataModel);

            ModelViewController.GetInstance.BindModelView_MoveRights(_moveRightsDataModel);

            ModelViewController.GetInstance.BindModelView_KingMoveRights(_kingMoveRightsDataModel);

            _goldDataModel.RefreshView();
            _populationDataModel.RefreshView();
            _populationCapacityDataModel.RefreshView();
            _moveRightsDataModel.RefreshView();
            _kingMoveRightsDataModel.RefreshView();

        }

        public void IncreaseVillageData(ValueGenre genreKey, int value)
        {
            if (dataModels.TryGetValue(genreKey, out AVillageDataModel dataModel))
            {
                dataModel.Increase(value);
            }
        }

        public void DecreaseVillageData(ValueGenre genreKey, int value)
        {
            if (dataModels.TryGetValue(genreKey, out AVillageDataModel dataModel))
            {
                dataModel.Decrease(value);
            }
        }

        protected void OnTurnPassToVillage()
        {
            _moveRightsDataModel.ResetMoveRights(kingdomPreset);
        }

        public bool IsGoldEnough(int requestedGold)
        {
            return _goldDataModel.CheckEnough(requestedGold);
        }

        public bool IsPopulationEnough(int requestedPopulation)
        {
            return villageData.Population >= requestedPopulation;
        }

        public bool IsMoveRightsEnough(int requestedMoveRights)
        {
            return _moveRightsDataModel.CheckEnough(requestedMoveRights);
        }

        public bool IsKingMoveRightsEnough(int requestedMoveRights)
        {
            return _kingMoveRightsDataModel.CheckEnough(requestedMoveRights);
        }

        public bool TryPurhcaseWith_Gold(int requestedGold)
        {
            bool state = _goldDataModel.CheckEnough(requestedGold);

            if (state)
            {
                PurchaseWith_Gold(requestedGold);
            }

            return state;
        }

        public bool TryPurhcaseWith_GoldAndPopulation(int requestedGold, int requestedPopulation)
        {
            bool stateGold = _goldDataModel.CheckEnough(requestedGold);
            bool statePop = _populationDataModel.CheckEnough(requestedPopulation);

            if (statePop && stateGold)
            {
                PurchaseWith_Gold(requestedGold);
                PurchaseWith_Population(requestedPopulation);
                return true;
            }

            return false;
        }

        public bool TryUseMoveRights(int requestedMoveRights)
        {
            bool state = _moveRightsDataModel.CheckEnough(requestedMoveRights);

            if (state)
            {
                UseMoveRights(requestedMoveRights);
            }

            return state;
        }

        public void UseMoveRights(int changeAmount = 1)
        {
            _moveRightsDataModel.Decrease(changeAmount);

            if (villageData.MoveRights <= 0)
            {
                TurnManager.GetInstance.PassTurn(Owner);
            }
        }

        public void UseKingMoveRights(int changeAmount = 1)
        {
            _kingMoveRightsDataModel.Decrease(changeAmount);

            if (villageData.KingMoveRights <= 0)
            {

            }
        }

        public void AddMoveRights(int changeAmount)
        {
            _moveRightsDataModel.Increase(changeAmount);
        }


        public void PurchaseWith_Population(int changeAmount)
        {
            _populationDataModel.Decrease(changeAmount);
        }

        public void AddPopulation(int changeAmount)
        {
            _populationDataModel.Increase(changeAmount);
        }


        void PurchaseWith_Gold(int changeAmount)
        {
            _goldDataModel.Decrease(changeAmount);
        }

        public void AddGold(int changeAmount)
        {
            _goldDataModel.Increase(changeAmount);
        }

    }

}