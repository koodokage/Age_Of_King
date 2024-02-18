using TMPro;
using UnityEngine;

namespace AgeOfKing.UI
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI TMP_winnerLabel;

        public void Open(Systems.IPlayer  player)
        {
            TMP_winnerLabel.text = player.Name;
            gameObject.SetActive(true);
        }

    }

}