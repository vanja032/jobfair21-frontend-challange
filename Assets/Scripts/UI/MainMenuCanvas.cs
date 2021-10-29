using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.UI
{
    public class MainMenuCanvas : MonoBehaviour
    {

        private static MainMenuCanvas _instance;
        public static MainMenuCanvas Instance => _instance;

        void Awake()
        {
            if (_instance == null) _instance = this;
            
        }

        public void BtnPlayClicked()
        {
            SceneManager.LoadScene("Assets/Scenes/LevelScene.unity", LoadSceneMode.Single);
        }
    }
}