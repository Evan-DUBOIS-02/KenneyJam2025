using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace UI
{
    public class UI_Manager : MonoBehaviour
    {
        [SerializeField]
        private string _playSceneName;
        [SerializeField]
        private string _playSceneMainMenu;
        [SerializeField]
        private string _playSceneCredit;

        public void OnClickPlay()
        {
            StartCoroutine(DelayedSceneLoad(1));
        }

        public void BackToMainMenu()
        {
            StartCoroutine(DelayedSceneLoad(2));
        }

        public void CreditScene()
        {
            StartCoroutine(DelayedSceneLoad(3));
        }

        private IEnumerator DelayedSceneLoad(int indice)
        {
            yield return new WaitForSeconds(0.5f);

            if (indice == 1)
            {
                SceneManager.LoadScene(_playSceneName);
            }
            else if (indice == 2)
            {
                SceneManager.LoadScene(_playSceneMainMenu);
            }
            else
            {
                SceneManager.LoadScene(_playSceneCredit);
            }
        
            
        }
        

    }
}