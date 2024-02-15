using TMPro;
using UnityEngine;

namespace AgeOfKing.UI
{
    public class SimpleGoldPocketView : AValuePocketView
    {
        [SerializeField] TextMeshProUGUI TMP_gold;

        public override void Decreased(int updated, int change)
        {
            TMP_gold.text = updated.ToString();
        }

        public override void Increased(int updated, int change)
        {
            TMP_gold.text = updated.ToString();
        }
    }


}
