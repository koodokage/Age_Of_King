using AgeOfKing.Systems;
using UnityEngine;
namespace AgeOfKing.UI
{
    public class TurnPassButton : MonoBehaviour
    {
        [SerializeField] bool isP1;

        public void OnButtonClick()
        {
            if (isP1)
            {
                TurnManager.GetInstance.PassTurn(PlayerManager.P1);
                return;
            }

            TurnManager.GetInstance.PassTurn(PlayerManager.P2);
        }
    }
}
