using AgeOfKing.Data;
using TMPro;
using UnityEngine;

namespace AgeOfKing.UI
{
    public class PopulationDataView : AVillageDataView
    {
        [SerializeField] TextMeshProUGUI TMP_current;

        public override void VillageDataChanged(VillageData updated)
        {
            TMP_current.text = updated.CurrentPopulation.ToString();
        }

    }

}
