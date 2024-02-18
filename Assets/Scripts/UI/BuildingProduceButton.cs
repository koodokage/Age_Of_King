using AgeOfKing.Abstract.Data;
using AgeOfKing.Data;
using AgeOfKing.Systems;
using AgeOfKing.Systems.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AgeOfKing.UI
{

    public class BuildingProduceButton : AEntityProducerButton<BuildingData>
    {
        [SerializeField] Button BTN_produce;
        [SerializeField] Image icon;
        [SerializeField] TextMeshProUGUI TMP_label;
        [SerializeField] TextMeshProUGUI TMP_value;
        BuildingData _building;

        public override void InitializeData(BuildingData entityData,IPlayer player)
        {
            _building = entityData;
            InitializeListener(player);
            Initialize(_building);
        }

        public void Initialize(BuildingData data)
        {
            _building = data;
            TMP_label.text = _building.GetLabel;
            TMP_value.text = _building.GetPrice.ToString();
            icon.sprite = (Resources.Load("UIAtlas") as UnityEngine.U2D.SpriteAtlas).GetSprite(_building.GetIcon.name);
            icon.rectTransform.sizeDelta = (_building.GetAspectSize);
            BTN_produce.onClick.RemoveAllListeners();
            BTN_produce.onClick.AddListener(OnButtonClicked);
        }


        void OnButtonClicked()
        {
            // validation
            if (ownerPlayer.GetVillage.IsGoldEnough(_building.GetPrice))
            {
                UIManager.GetInstance.OnClicked_BuildingButton(_building,ownerPlayer);
            }

        }



        public override void VillageDataChanged(VillageData updated)
        {
            if (updated.MoveRights <= 0)
            {
                BTN_produce.interactable = false;
                return;
            }

            if (updated.Gold < _building.GetPrice)
            {
                TMP_value.color = Color.red;
                BTN_produce.interactable = false;
                return;
            }

            TMP_value.color = Color.white;

            BTN_produce.interactable = true;
        }

    }



}
