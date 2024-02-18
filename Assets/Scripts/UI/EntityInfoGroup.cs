using AgeOfKing.Abstract.Components;
using AgeOfKing.Abstract.Data;
using AgeOfKing.Components;
using AgeOfKing.Data;
using AgeOfKing.Systems;
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

        char plus = '+';
        char mine = ' ';

        private void Start()
        {
            Close();
        }

        private void InitializeInfo(AEntityData data)
        {
            TMP_label.text = data.GetLabel;
            icon.sprite = (Resources.Load("UIAtlas") as UnityEngine.U2D.SpriteAtlas).GetSprite(data.GetIcon.name); ;
            icon.rectTransform.sizeDelta = (data.GetAspectSize);

            StringBuilder builder = new StringBuilder();
            builder.AppendLine(data.GetDescription);
            char onUse;
            foreach (EntityStat stat in data.GetEntityStats)
            {
                onUse = stat.GetValue > 0 ? plus : mine;
                builder.AppendLine($"{stat.GetGenre}: {onUse}{stat.GetValue} ({stat.GetUsage})");
            }

            TMP_description.text = builder.ToString();
        }

        public void GenerateAndOpen(AManufacturerBuilding building)
        {
            InitializeInfo(building.GetData);

            ReleaseProduceables();

            //extra :  Can be pulled in manufacturer building data for separeted unit
            UnitData[] unitDatas = building.GetOwnerPlayer.GetKingdomPreset.GetKigdomUnits;
            int unitDataCount = unitDatas.Length;
            int scrollerRequestedCount = infiniteScroll.GetRequestedAmount(unitDataCount,UnitProduceButtonFactory.GetInstance.GetPrefabHeight);
            int loopIndexCounter = 0;
            UnitProduceButton[] buttons = new UnitProduceButton[scrollerRequestedCount];

            while (scrollerRequestedCount != 0)
            {
                var unitButton = UnitProduceButtonFactory.GetInstance.GetProducerUI(unitDatas[loopIndexCounter], scrollContent, TurnManager.GetInstance.GetTurnPlayer);

                scrollerRequestedCount--;

                buttons[scrollerRequestedCount] = unitButton.GetComponent<UnitProduceButton>();

                loopIndexCounter++;
                if (loopIndexCounter >= unitDataCount)
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



        public void InitializeAndOpen(BuildingData buildingData)
        {
            InitializeInfo(buildingData);
            ReleaseProduceables();
            ownerPanel.SetActive(true);
        }


        public void InitializeAndOpen(AUnit unit)
        {
            InitializeInfo(unit.GetData);

            StringBuilder builder = new StringBuilder();

            builder.AppendLine(TMP_description.text);
            char onUse;

            if (unit.TryGetComponent(out IHittable hittable))
            {
                builder.AppendLine($"HEALTH: {hittable.CurrentHealth} / {unit.GetBaseData.GetEntityHealth}");
            }

            foreach (var stat in unit.GetData.GetStats)
            {

                if (stat.GetGenre == Data.CharacterStatGenre.MOVEMENT)
                {
                    builder.AppendLine($"{stat.GetGenre}: {unit.CurrentMovePoint} / {stat.GetValue}");
                }


                onUse = stat.GetValue > 0 ? plus : mine;
                builder.AppendLine($"{stat.GetGenre}: {onUse}{stat.GetValue}");
            }

            TMP_description.text = builder.ToString();


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
