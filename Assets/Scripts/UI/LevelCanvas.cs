using Platformer.Gameplay;
using Platformer.Model;
using TMPro;
using UnityEngine;

namespace Platformer.UI
{
    public class LevelCanvas : MonoBehaviour
    {
        #region Fields and Properties

        [SerializeField] private PauseMenu pauseMenu;
        [SerializeField] private LevelEndedPopup levelEndedPopup;
        [SerializeField] private TMP_Text lblTokens;
        [SerializeField] private TMP_Text lblEnemiesKilled;
        [SerializeField] private TMP_Text lblUsername;
        [SerializeField] private float unitDimension;

        #endregion Fields and Properties


        private static LevelCanvas _instance;
        public static LevelCanvas Instance => _instance;

        void Awake()
        {
            if (_instance == null) _instance = this;

            PlayerDeath.OnExecute += PlayerDiedCallback;
            PlayerEnteredVictoryZone.OnExecute += PlayerWonCallback;
            
            GameDatabase.Instance.ResetScore();

            if (Screen.width >= Screen.height * 16 / 9)
            {
                unitDimension = Mathf.Round((float)Screen.height / 1080 * 100.0f) / 100.0f;
            }
            else
            {
                unitDimension = 1.5f * Mathf.Round((float)Screen.width / 1920 * 100.0f) / 100.0f;
            }

            this.gameObject.transform.Find("InGameUI").Find("BtnPause").GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.transform.Find("InGameUI").Find("BtnPause").GetComponent<RectTransform>().rect.width * unitDimension * 1.5f, this.gameObject.transform.Find("InGameUI").Find("BtnPause").GetComponent<RectTransform>().rect.height * unitDimension * 1.5f);
            this.gameObject.transform.Find("InGameUI").Find("BtnPause").GetComponent<RectTransform>().anchoredPosition = new Vector2(-this.gameObject.transform.Find("InGameUI").Find("BtnPause").GetComponent<RectTransform>().rect.height / 3 * 2, this.gameObject.transform.Find("InGameUI").Find("BtnPause").GetComponent<RectTransform>().rect.height / 3 * 2);

            this.gameObject.transform.Find("InGameUI").Find("Username").GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.transform.Find("InGameUI").Find("Username").GetComponent<RectTransform>().rect.width * unitDimension * 1.5f, this.gameObject.transform.Find("InGameUI").Find("Username").GetComponent<RectTransform>().rect.height * unitDimension * 1.5f);
            this.gameObject.transform.Find("InGameUI").Find("Username").Find("Text (TMP)").GetComponent<TextMeshProUGUI>().fontSize *= unitDimension * 1.5f;
            this.gameObject.transform.Find("InGameUI").Find("Username").GetComponent<RectTransform>().anchoredPosition = new Vector2(this.gameObject.transform.Find("InGameUI").Find("Username").GetComponent<RectTransform>().rect.height / 3 * 2 + this.gameObject.transform.Find("InGameUI").Find("Username").GetComponent<RectTransform>().rect.width / 2, -this.gameObject.transform.Find("InGameUI").Find("Username").GetComponent<RectTransform>().rect.height);

            this.gameObject.transform.Find("InGameUI").Find("Enemies").GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.transform.Find("InGameUI").Find("Enemies").GetComponent<RectTransform>().rect.width * unitDimension * 1.5f, this.gameObject.transform.Find("InGameUI").Find("Enemies").GetComponent<RectTransform>().rect.height * unitDimension * 1.5f);
            this.gameObject.transform.Find("InGameUI").Find("Enemies").Find("Image").GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.transform.Find("InGameUI").Find("Enemies").Find("Image").GetComponent<RectTransform>().rect.width * unitDimension * 1.5f, this.gameObject.transform.Find("InGameUI").Find("Enemies").Find("Image").GetComponent<RectTransform>().rect.height * unitDimension * 1.5f);
            this.gameObject.transform.Find("InGameUI").Find("Enemies").Find("Image").GetComponent<RectTransform>().anchoredPosition = new Vector2(this.gameObject.transform.Find("InGameUI").Find("Enemies").Find("Image").GetComponent<RectTransform>().rect.width , 0);
            this.gameObject.transform.Find("InGameUI").Find("Enemies").Find("Text (TMP)").GetComponent<TextMeshProUGUI>().fontSize *= unitDimension * 1.5f;
            this.gameObject.transform.Find("InGameUI").Find("Enemies").Find("Text (TMP)").GetComponent<RectTransform>().anchoredPosition = new Vector2(-this.gameObject.transform.Find("InGameUI").Find("Enemies").Find("Image").GetComponent<RectTransform>().rect.width/2, 0);
            this.gameObject.transform.Find("InGameUI").Find("Enemies").GetComponent<RectTransform>().anchoredPosition = new Vector2(-this.gameObject.transform.Find("InGameUI").Find("Enemies").GetComponent<RectTransform>().rect.height / 3 * 2 - this.gameObject.transform.Find("InGameUI").Find("Enemies").GetComponent<RectTransform>().rect.width * 1.7f , -this.gameObject.transform.Find("InGameUI").Find("Enemies").GetComponent<RectTransform>().rect.height);

            this.gameObject.transform.Find("InGameUI").Find("Tokens").GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.transform.Find("InGameUI").Find("Tokens").GetComponent<RectTransform>().rect.width * unitDimension * 1.5f, this.gameObject.transform.Find("InGameUI").Find("Tokens").GetComponent<RectTransform>().rect.height * unitDimension * 1.5f);
            this.gameObject.transform.Find("InGameUI").Find("Tokens").Find("Image").GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.transform.Find("InGameUI").Find("Tokens").Find("Image").GetComponent<RectTransform>().rect.width * unitDimension * 1.5f, this.gameObject.transform.Find("InGameUI").Find("Tokens").Find("Image").GetComponent<RectTransform>().rect.height * unitDimension * 1.5f);
            this.gameObject.transform.Find("InGameUI").Find("Tokens").Find("Image").GetComponent<RectTransform>().anchoredPosition = new Vector2(this.gameObject.transform.Find("InGameUI").Find("Tokens").Find("Image").GetComponent<RectTransform>().rect.width, 0);
            this.gameObject.transform.Find("InGameUI").Find("Tokens").Find("Text (TMP)").GetComponent<TextMeshProUGUI>().fontSize *= unitDimension * 1.5f;
            this.gameObject.transform.Find("InGameUI").Find("Tokens").Find("Text (TMP)").GetComponent<RectTransform>().anchoredPosition = new Vector2(-this.gameObject.transform.Find("InGameUI").Find("Tokens").Find("Image").GetComponent<RectTransform>().rect.width / 2, 0);
            this.gameObject.transform.Find("InGameUI").Find("Tokens").GetComponent<RectTransform>().anchoredPosition = new Vector2(-this.gameObject.transform.Find("InGameUI").Find("Tokens").GetComponent<RectTransform>().rect.height / 3 * 2 - this.gameObject.transform.Find("InGameUI").Find("Tokens").GetComponent<RectTransform>().rect.width * 0.5f, -this.gameObject.transform.Find("InGameUI").Find("Tokens").GetComponent<RectTransform>().rect.height);

            /*this.gameObject.transform.Find("LevelEndedPopup").Find("LblTitle").GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.transform.Find("GameName").GetComponent<RectTransform>().rect.width * unitDimension, this.gameObject.transform.Find("GameName").GetComponent<RectTransform>().rect.height * unitDimension);
            this.gameObject.transform.Find("LevelEndedPopup").Find("LblTitle").GetComponent<TextMeshProUGUI>().fontSize *= unitDimension;
            this.gameObject.transform.Find("LevelEndedPopup").Find("LblTitle").GetComponent<RectTransform>().anchoredPosition = new Vector2(this.gameObject.transform.Find("GameName").GetComponent<RectTransform>().anchoredPosition.x, this.gameObject.transform.Find("GameName").GetComponent<RectTransform>().rect.height / 2);
            */
        }

        private void OnDestroy()
        {
            PlayerDeath.OnExecute -= PlayerDiedCallback;
            PlayerEnteredVictoryZone.OnExecute -= PlayerWonCallback;
        }

        private void Update()
        {
            lblTokens.text = GameDatabase.Instance.CurrentUser.Tokens.ToString();
            lblEnemiesKilled.text = GameDatabase.Instance.CurrentUser.EnemiesKilled.ToString();
            lblUsername.text = GameDatabase.Instance.CurrentUser.Username.ToString(); 
        }

        #region Event Handlers
        
        private void PlayerDiedCallback(PlayerDeath playerDeath)
        {
            levelEndedPopup.Show(false);
        }

        private void PlayerWonCallback(PlayerEnteredVictoryZone playerEnteredVictoryZone)
        {
            levelEndedPopup.Show(true); 
        }

        public void BtnPauseClicked()
        {
            pauseMenu.Show();
        }

        #endregion Event Handlers
    }
}