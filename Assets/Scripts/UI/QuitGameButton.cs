using UnityEngine;
using UnityEngine.UI;

namespace AgeOfKing.UI
{
    [RequireComponent(typeof(Button))]
    public class QuitGameButton : MonoBehaviour
    {

        private void Awake()
        {
            var button = GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            Application.Quit();
        }
    }
}
