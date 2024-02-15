using AgeOfKing.Abstract.Datas;
using AgeOfKing.Datas;
using AgeOfKing.Systems;
using AgeOfKing.Systems.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AgeOfKing.UI
{

    public class BuildingProduceButton : AEntityProducerButton, IValueViewChangeListener
    {
        [SerializeField] Button BTN_produce;
        [SerializeField] Image icon;
        [SerializeField] TextMeshProUGUI TMP_label;
        BuildingData _building;

        private void OnEnable()
        {
            ModelViewController.GetInstance.Subscribe_OnChangeGoldPocket(this);
        }

        private void OnDisable()
        {
            ModelViewController.GetInstance.Unsubscribe_OnChangeGoldPocket(this);
        }


        public override void InitializeData(AEntityData entityData)
        {
            if (entityData is BuildingData)
            {
                _building = entityData as BuildingData;
                Initialize(_building);
            }
        }

        public void Initialize(BuildingData data)
        {
            _building = data;
            TMP_label.text = _building.GetLabel;
            icon.sprite = (Resources.Load("UIAtlas") as UnityEngine.U2D.SpriteAtlas).GetSprite(_building.GetIcon.name); ;
            icon.rectTransform.sizeDelta = (_building.GetAspectSize);
            BTN_produce.onClick.RemoveAllListeners();
            BTN_produce.onClick.AddListener(OnButtonClicked);
        }


        void OnButtonClicked()
        {
            // basit check
            if (PlayerManager.GetInstance.IsGoldEnough(_building.GetPrice))
            {
                UIManager.GetInstance.OnClicked_BuildingButton(_building);
            }

            // UI sounds, and some feedbacks
        }


        public  void OnValueChanged(int leftAmount)
        {
            if (leftAmount < _building.GetPrice)
            {
                BTN_produce.interactable = false;
                return;
            }

            BTN_produce.interactable = true;
        }
    }



}
