using AgeOfKing.Data;
using TMPro;
using UnityEngine;

namespace AgeOfKing.UI
{
    public class GoldDataView : AVillageDataView
    {
        [SerializeField] TextMeshProUGUI TMP_gold;

        public override void VillageDataChanged(VillageData updated)
        {
            TMP_gold.text = updated.Gold.ToString();
        }
    }


}
