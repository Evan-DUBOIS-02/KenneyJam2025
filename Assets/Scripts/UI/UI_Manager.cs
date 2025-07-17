using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UI_Manager:MonoBehaviour
    {
        [SerializeField]
        private string _playSceneName;
        
        public void OnClickPlay()
        {
            SceneManager.LoadScene(_playSceneName);
        }
    }
}