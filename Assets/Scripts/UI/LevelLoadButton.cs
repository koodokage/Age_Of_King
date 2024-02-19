using AgeOfKing.Systems;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AgeOfKing.UI
{
    [RequireComponent(typeof(Button))]
    public  class LevelLoadButton : MonoBehaviour
    {
        [SerializeField] string targetLevel;

        private void Awake()
        {
            var button = GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnButtonClick);
        }

        public void OnButtonClick()
        {
            PlayerManager.OnGameOver();
            StartCoroutine(LoadYourAsyncScene());
        }

        IEnumerator LoadYourAsyncScene()
        {
            Scene current = SceneManager.GetActiveScene();
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetLevel,LoadSceneMode.Single);

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {

                yield return null;
            }

            SceneManager.UnloadSceneAsync(current, UnloadSceneOptions.None);

        }
    }
}
