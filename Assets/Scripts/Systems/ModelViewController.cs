using AgeOfKing.Data;
using AgeOfKing.UI;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace AgeOfKing.Systems
{
    public class ModelViewController : SingleBehaviour<ModelViewController>
    {

        [Header("GOLD")]
        [SerializeField] AModelData goldPocketModel;
        [SerializeField] AValuePocketView goldPocketView;
        [SerializeField] UnityEvent OnGoldCome;
        [SerializeField] UnityEvent OnGoldGone;

        [Header("MOVERIGHTS")]
        [SerializeField] AModelData moveRightPocketModel;
        [SerializeField] AValuePocketView moveRightPocketView;
        [SerializeField] UnityEvent OnMoveRightUsed;

        [Header("POPULATION")]
        [SerializeField] APopulationModelData populationPocketModel;
        [SerializeField] APopulationPocketView populationPocketView;
        [SerializeField] UnityEvent OnPopulationCapacityIncrease;
        [SerializeField] UnityEvent OnPopulationCapacityDecrease;

        protected override void Awake()
        {
            base.Awake();
            InitializePopulationPocketView();
            InitializeGoldPocketView();
            InitializeMoveRightPocketView();
        }


        public void InitializePopulationPocketView()
        {
            Assert.IsNotNull(populationPocketModel);
            Assert.IsNotNull(populationPocketView);

            populationPocketModel.OnIncrease += PopulationPocketModel_OnIncrease;
            populationPocketModel.OnDecrease += PopulationPocketModel_OnDecrease;
            populationPocketModel.OnCapacityIncrease += PopulationPocketModel_OnCapacityIncrease;
            populationPocketModel.OnCapacityDecrease += PopulationPocketModel_OnCapacityDecrease;
  
        }

        public void InitializeGoldPocketView()
        {
            Assert.IsNotNull(goldPocketModel);
            Assert.IsNotNull(goldPocketView);

            goldPocketModel.OnIncrease += GoldPocketModel_OnIncrease;
            goldPocketModel.OnDecrease += GoldPocketModel_OnDecrease;
        }

        public void InitializeMoveRightPocketView()
        {
            Assert.IsNotNull(moveRightPocketModel);
            Assert.IsNotNull(moveRightPocketView);

            moveRightPocketModel.OnIncrease += MoveRightPocketModel_OnIncrease;
            moveRightPocketModel.OnDecrease += MoveRightPocketModel_OnDecrease;
        }

        private void MoveRightPocketModel_OnDecrease(int arg1, int arg2)
        {
            moveRightPocketView.Decreased(arg1, arg2);
            OnMoveRightUsed?.Invoke();
        }

        private void MoveRightPocketModel_OnIncrease(int arg1, int arg2)
        {
            moveRightPocketView.Increased(arg1, arg2);
        }

        public void Subscribe_OnChangeGoldPocket(IValueViewChangeListener listener) 
        {
            goldPocketModel.OnChange += listener.OnValueChanged;
        }

        public void Unsubscribe_OnChangeGoldPocket(IValueViewChangeListener listener)
        {
            goldPocketModel.OnChange -= listener.OnValueChanged;
        }

        public void Subscribe_OnChangePopulationPocket(IValueViewChangeListener listener)
        {
            populationPocketModel.OnChange += listener.OnValueChanged;
        }

        public void Unsubscribe_OnChangePopulationPocket(IValueViewChangeListener listener)
        {
            populationPocketModel.OnChange -= listener.OnValueChanged;
        }

        

        private void PopulationPocketModel_OnCapacityDecrease(int obj)
        {
            populationPocketView.CapacityDecreased(obj);
            OnPopulationCapacityIncrease?.Invoke();

        }

        private void PopulationPocketModel_OnCapacityIncrease(int obj)
        {
            populationPocketView.CapacityIncreased(obj);
            OnPopulationCapacityIncrease?.Invoke();

        }

        private void PopulationPocketModel_OnDecrease(int arg1, int arg2)
        {
            populationPocketView.Decreased(arg1, arg2);
            OnPopulationCapacityDecrease?.Invoke();
        }

        private void PopulationPocketModel_OnIncrease(int arg1, int arg2)
        {
            populationPocketView.Increased(arg1, arg2);
            OnPopulationCapacityIncrease?.Invoke();
        }

        private void GoldPocketModel_OnDecrease(int arg1, int arg2)
        {
            goldPocketView.Decreased(arg1, arg2);
            OnGoldGone?.Invoke();
        }

        private void GoldPocketModel_OnIncrease(int arg1, int arg2)
        {
            goldPocketView.Increased(arg1,arg2);
            OnGoldCome?.Invoke();
        }

    }

}
