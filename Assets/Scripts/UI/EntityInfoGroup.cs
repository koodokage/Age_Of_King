using AgeOfKing.Abstract.Components;
using AgeOfKing.Components;
using AgeOfKing.Datas;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AgeOfKing.UI
{
    public class EntityInfoGroup : MonoBehaviour
    {
        [Header("INFO SECTION")]
        [SerializeField] GameObject ownerPanel;
        [SerializeField] Transform scrollContent;
        [SerializeField] InfiniteScroll infiniteScroll;
        [SerializeField] TextMeshProUGUI TMP_label;
        [SerializeField] Image icon;
        [SerializeField] TextMeshProUGUI TMP_description;

        private void Start()
        {
            Close();
        }

        public void Open(AManufacturerBuilding building)
        {
            TMP_label.text = building.GetData.GetLabel;
            TMP_description.text = building.GetData.GetDescription;
            icon.sprite = (Resources.Load("UIAtlas") as UnityEngine.U2D.SpriteAtlas).GetSprite(building.GetData.GetIcon.name); ;
            icon.rectTransform.sizeDelta = (building.GetData.GetAspectSize);

            ReleaseProduceables();

            UnitData[] unitDatas = building.GetManufacturerData.GetUnits;
            int unitDataCount = unitDatas.Length;
            int scrollerRequestedCount = infiniteScroll.GetRequestedAmount(unitDataCount);
            int loopIndexCounter = 0;
            UnitProduceButton[] buttons = new UnitProduceButton[scrollerRequestedCount];

            while (scrollerRequestedCount != 0)
            {
                var unitButton = UnitProduceButtonFactory.GetInstance.GetProducerUI(unitDatas[loopIndexCounter],scrollContent);

                scrollerRequestedCount--;

                buttons[scrollerRequestedCount] = unitButton.GetComponent<UnitProduceButton>();

                loopIndexCounter++;
                if(loopIndexCounter >= unitDataCount)
                {
                    loopIndexCounter = 0;
                }

            }

            foreach (var unitProduceButton in buttons)
            {
                unitProduceButton.SetManufacturer(building);
                unitProduceButton.transform.SetParent(scrollContent);
            }
          

            infiniteScroll.Launch();
            infiniteScroll.gameObject.SetActive(true);

            ownerPanel.SetActive(true);
        }


        public void Open(ABuilding building)
        {
            TMP_label.text = building.GetData.GetLabel;
            TMP_description.text = building.GetData.GetDescription;
            icon.sprite = (Resources.Load("UIAtlas") as UnityEngine.U2D.SpriteAtlas).GetSprite(building.GetData.GetIcon.name);
            icon.rectTransform.sizeDelta = (building.GetData.GetAspectSize);

            ReleaseProduceables();

            ownerPanel.SetActive(true);
        }


        public void Open(AUnit unit)
        {

            TMP_label.text = unit.GetData.GetLabel;
            icon.sprite = (Resources.Load("UIAtlas") as UnityEngine.U2D.SpriteAtlas).GetSprite(unit.GetData.GetIcon.name);
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(unit.GetData.GetDescription);
            builder.AppendLine($"ATTACK: {unit.GetData.GetAttack}");
            builder.AppendLine($"MOVEMENT: {unit.GetData.GetMovement}");
            builder.AppendLine($"HEALTH: {unit.GetData.GetHealth}");
            TMP_description.text = builder.ToString();
            icon.rectTransform.sizeDelta = (unit.GetData.GetAspectSize);

            ReleaseProduceables();

            ownerPanel.SetActive(true);
        }

        public void Close()
        {
            ownerPanel.SetActive(false);
            ReleaseProduceables();

        }

        private void ReleaseProduceables()
        {
            infiniteScroll.gameObject.SetActive(false);

            int scrollContentChildCount = scrollContent.childCount;
            UnitProduceButton unitButton;

            while (scrollContentChildCount != 0)
            {
                var child = scrollContent.GetChild(0);
                if (child.TryGetComponent(out unitButton))
                {
                    UnitProduceButtonFactory.GetInstance.Release(unitButton);
                }
                scrollContentChildCount--;
            }

        }
    }
}
