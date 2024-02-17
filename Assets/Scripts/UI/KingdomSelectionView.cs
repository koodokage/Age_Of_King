using AgeOfKing.Data;
using AgeOfKing.Systems;
using UnityEngine;

namespace AgeOfKing.UI
{
    public class KingdomSelectionView : MonoBehaviour
    {
        [SerializeField] KingdomPreset kingdomPreset_P1;
        [SerializeField] KingdomPreset kingdomPreset_P2;

        public void OnClicked_CreateGame()
        {
            PlayerManager.CreatePlayer(kingdomPreset_P1, kingdomPreset_P2);
        }

    }
}
