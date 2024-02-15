using TMPro;
using UnityEngine;

namespace AgeOfKing.UI
{
    public class SimpleMoveRightPocketView : AValuePocketView
    {
        [SerializeField] TextMeshProUGUI TMP_moveRight;

        public override void Decreased(int updated, int change)
        {
            TMP_moveRight.text = updated.ToString();
        }

        public override void Increased(int updated, int change)
        {
            TMP_moveRight.text = updated.ToString();
        }
    }


}
