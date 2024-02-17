using AgeOfKing.Abstract.Components;
using AgeOfKing.Data;
using AgeOfKing.Systems;
using TMPro;
using UnityEngine;

using UnityEngine.UI;

namespace AgeOfKing.UI
{
    public class UnitProduceButton : AEntityProducerButton<UnitData>
    {
        [SerializeField] Button BTN_produce;
        [SerializeField] TextMeshProUGUI TMP_Label;
        [SerializeField] Image icon;
        [SerializeField] TextMeshProUGUI TMP_Price;

        UnitData _unitData;
        IManufacturerBuilding _manufacturerBuilding;



        public override void InitializeData(UnitData entityData, IPlayer player)
        {
                _unitData = entityData;
                InitializeListener(player);
                Initialize(_unitData);
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
            if (ownerPlayer.GetVillage.IsGoldEnough(_unitData.GetPrice))
            {
                _manufacturerBuilding.Produce(_unitData);
            }
        }



        public override void VillageDataChanged(VillageData updated)
        {
            if (updated.MoveRights <= 0)
            {
                BTN_produce.interactable = false;
                return;
            }

            if (updated.Gold < _unitData.GetPrice || updated.Population <= 0)
            {
                BTN_produce.interactable = false;
                return;
            }

            BTN_produce.interactable = true;
        }
    }
}
