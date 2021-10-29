using Platformer.Gameplay;
using Platformer.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.UI
{
    public class LevelCanvas : MonoBehaviour
    {
        private static LevelCanvas _instance;
        public static LevelCanvas Instance => _instance;

        void Awake()
        {
            if (_instance == null) _instance = this;

            PlayerDeath.OnExecute += PlayerDiedCallback;
            PlayerEnteredVictoryZone.OnExecute += PlayerWonCallback;
            
            GameDatabase.Instance.ResetScore();
        }

        private void OnDestroy()
        {
            PlayerDeath.OnExecute -= PlayerDiedCallback;
            PlayerEnteredVictoryZone.OnExecute -= PlayerWonCallback;
        }

        private void PlayerDiedCallback(PlayerDeath playerDeath)
        {
            GoBackToMainScene();
        }

        private void PlayerWonCallback(PlayerEnteredVictoryZone playerEnteredVictoryZone)
        {
            GoBackToMainScene();
        }

        private void GoBackToMainScene()
        {
            SceneManager.LoadScene("Assets/Scenes/MainScene.unity", LoadSceneMode.Single);
        }
    }
}