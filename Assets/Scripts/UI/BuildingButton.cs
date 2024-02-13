using AgeOfKing.Components;
using AgeOfKing.Systems.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AgeOfKing.UI
{
    public class BuildingButton : MonoBehaviour
    {
        [SerializeField] Image icon;
        [SerializeField] TextMeshProUGUI TMP_label;
        BuildingData _building;

        public void Initialize(BuildingData building)
        {
            TMP_label.text = building.BuildingLabel;
            icon.sprite = building.Icon;
            _building = building;
            icon.rectTransform.sizeDelta = (building.AspectSize);
        }


        public void OnButtonClicked()
        {
            UIManager.GetInstance.OnClicked_BuildingButton(_building);
            // UI sounds, and some feedbacks
        }


    }
}
