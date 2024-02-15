using AgeOfKing.Abstract.Components;
using AgeOfKing.Abstract.Datas;
using AgeOfKing.Datas;
using AgeOfKing.Systems;
using TMPro;
using UnityEngine;

using UnityEngine.UI;

namespace AgeOfKing.UI
{



    public class UnitProduceButton : AEntityProducerButton, IValueViewChangeListener , IPopulationViewChangeListener
    {
        [SerializeField] Button BTN_produce;
        [SerializeField] TextMeshProUGUI TMP_Label;
        [SerializeField] Image icon;
        [SerializeField] TextMeshProUGUI TMP_Price;

        UnitData _unitData;
        IManufacturerBuilding _manufacturerBuilding;

        private void Start()
        {
        }

        private void OnEnable()
        {
            ModelViewController.GetInstance.Subscribe_OnChangeGoldPocket(this);   
            ModelViewController.GetInstance.Subscribe_OnChangePopulationPocket(this);
        }

        private void OnDisable()
        {
            ModelViewController.GetInstance.Unsubscribe_OnChangeGoldPocket(this);
            ModelViewController.GetInstance.Unsubscribe_OnChangePopulationPocket(this);
        }

        public override void InitializeData(AEntityData entityData)
        {
            if(entityData is UnitData)
            {
                _unitData = entityData as UnitData;
                Initialize(_unitData);
            }
        }

        public void Initialize(UnitData data)
        {
            _unitData = data;
            TMP_Label.text = _unitData.GetLabel;
            TMP_Price.text = _unitData.GetPrice.ToString();
            icon.sprite = (Resources.Load("UIAtlas") as  UnityEngine.U2D.SpriteAtlas).GetSprite(_unitData.GetIcon.name);
            icon.rectTransform.sizeDelta = (data.GetAspectSize);
            BTN_produce.onClick.RemoveAllListeners();
            BTN_produce.onClick.AddListener(OnButtonClicked);
        }

        public void SetManufacturer(IManufacturerBuilding manufacturer)
        {
            _manufacturerBuilding = manufacturer;
        }

        void OnButtonClicked()
        {
            if (PlayerManager.GetInstance.IsGoldEnough(_unitData.GetPrice))
            {
                _manufacturerBuilding.Produce(_unitData);
            }
        }

        public void OnValueChanged(int leftAmount)
        {
            if (leftAmount < _unitData.GetPrice)
            {
                BTN_produce.interactable = false;
                return;
            }

            BTN_produce.interactable = true;
        }

        public void OnPopulationChanged(int leftAmount)
        {
            if (leftAmount <= 0 )
            {
                BTN_produce.interactable = false;
                return;
            }

            BTN_produce.interactable = true;
        }

      
    }
}
