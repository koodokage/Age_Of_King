using TMPro;
using UnityEngine;

namespace AgeOfKing.UI
{
    public class SimplePoulationPocketView : APopulationPocketView
    {
        [SerializeField] TextMeshProUGUI TMP_current;
        [SerializeField] TextMeshProUGUI TMP_capacity;

        public override void CapacityDecreased(int updated)
        {
            TMP_capacity.text = updated.ToString();
        }

        public override void CapacityIncreased(int updated)
        {
            TMP_capacity.text = updated.ToString();
        }

        public override void Decreased(int updated, int change)
        {
            TMP_current.text = updated.ToString();
        }

        public override void Increased(int updated, int change)
        {
            TMP_current.text = updated.ToString();
        }

    }


}
