using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UI_Manager:MonoBehaviour
    {
        [SerializeField]
        private string _playSceneName;
        [SerializeField]
        private string _playSceneMainMenu;
        [SerializeField]
        private string _playSceneCredit;

        public void OnClickPlay()
        {
            SceneManager.LoadScene(_playSceneName);
        }

        public void BackToMainMenu()
        {
            SceneManager.LoadScene(_playSceneMainMenu);
        }

        public void CreditScene()
        {
            SceneManager.LoadScene(_playSceneCredit);
        }
    }
}