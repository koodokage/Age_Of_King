using AgeOfKing.Data;
using TMPro;
using UnityEngine;

namespace AgeOfKing.UI
{
    public class SimpleKingMoveRightPocketView : AVillageDataView
    {
        [SerializeField] TextMeshProUGUI TMP_moveRight;

        public override void VillageDataChanged(VillageData updated)
        {
            TMP_moveRight.text = updated.KingMoveRights.ToString();
        }

    }

}
