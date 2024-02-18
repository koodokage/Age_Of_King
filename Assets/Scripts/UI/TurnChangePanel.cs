using AgeOfKing.Systems;
using System.Collections;
using TMPro;
using UnityEngine;
namespace AgeOfKing.UI
{
    public class TurnChangePanel : MonoBehaviour
    {
        [SerializeField] GameObject humansTurnOpenGroup;
        [SerializeField] GameObject humansTurnCloseGroup;

        [SerializeField] GameObject goblinsTurnOpenGroup;
        [SerializeField] GameObject goblinsTurnCloseGroup;

        [SerializeField] TextMeshProUGUI tmp_turnCount;
        [SerializeField] CanvasGroup canvasGroup;


        public void OnTurnChange(IPlayer player, int turnCount)
        {
            canvasGroup.blocksRaycasts = true;
            tmp_turnCount.text = turnCount.ToString();

            if (player.GetKingdomPreset.GetKigdomType == Data.KingdomPreset.Kingdom.HUMAN)
            {
                humansTurnOpenGroup.SetActive(true);
                goblinsTurnCloseGroup.SetActive(true);

                goblinsTurnOpenGroup.SetActive(false);
                humansTurnCloseGroup.SetActive(false);
            }
            else
            {
                goblinsTurnOpenGroup.SetActive(true);
                humansTurnCloseGroup.SetActive(true);

                humansTurnOpenGroup.SetActive(false);
                goblinsTurnCloseGroup.SetActive(false);
            }

            StartCoroutine(FadeInOut());
        }

        IEnumerator FadeInOut()
        {
            float alpha = 0;
            float velocity = 0;
            canvasGroup.alpha = alpha;

            while (alpha < 1)
            {
                alpha = Mathf.SmoothDamp(alpha,1,ref velocity,.1f);
                canvasGroup.alpha = alpha;
                
                if (alpha > .9f)
                    break;

                yield return null;
            }

            alpha = 1;
            canvasGroup.alpha = alpha;
            
            yield return new WaitForSeconds(1.5f);

            velocity = 0;
            while (alpha > 0 )
            {
                alpha = Mathf.SmoothDamp(alpha, 0, ref velocity, .05f);
                canvasGroup.alpha = alpha;

                if ( alpha < .1f)
                    break;

                yield return null;
            }

            alpha = 0;
            canvasGroup.alpha = alpha;

            canvasGroup.blocksRaycasts = false;

        }
    }
}
