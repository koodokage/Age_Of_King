using AgeOfKing.Data;
using TMPro;
using UnityEngine;

namespace AgeOfKing.UI
{
    public class PopulationCapacityDataView : AVillageDataView
    {
        [SerializeField] TextMeshProUGUI TMP_capacity;

        public override void VillageDataChanged(VillageData updated)
        {
            TMP_capacity.text = updated.PopulationCapacity.ToString();
        }

    }

}
