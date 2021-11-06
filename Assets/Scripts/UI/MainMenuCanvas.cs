using Platformer.Model;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Platformer.UI
{
    public class MainMenuCanvas : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputUsername;
        [SerializeField] private Button btnPlay;
        [SerializeField] private float unitDimension; 

        private static MainMenuCanvas _instance;
        public static MainMenuCanvas Instance => _instance;

        void Awake()
        {
            if (_instance == null) _instance = this;

            if (Screen.width >= Screen.height * 16 / 9)
            {
                this.gameObject.transform.Find("BackgroundImage").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.width * 9 / 16);
                unitDimension = Mathf.Round((float)Screen.height / 1080 * 100.0f) / 100.0f;
            }
            else
            {
                this.gameObject.transform.Find("BackgroundImage").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.height * 16 / 9, Screen.height);
                unitDimension = 2 * Mathf.Round((float)Screen.width / 1920 * 100.0f) / 100.0f;
            }

            this.gameObject.transform.Find("GameName").GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.transform.Find("GameName").GetComponent<RectTransform>().rect.width * unitDimension, this.gameObject.transform.Find("GameName").GetComponent<RectTransform>().rect.height * unitDimension);
            this.gameObject.transform.Find("GameName").GetComponent<TextMeshProUGUI>().fontSize *= unitDimension;
            this.gameObject.transform.Find("GameName").GetComponent<RectTransform>().anchoredPosition = new Vector2(this.gameObject.transform.Find("GameName").GetComponent<RectTransform>().anchoredPosition.x, this.gameObject.transform.Find("GameName").GetComponent<RectTransform>().rect.height / 2);

            this.gameObject.transform.Find("NameInputField").GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.transform.Find("NameInputField").GetComponent<RectTransform>().rect.width * unitDimension, this.gameObject.transform.Find("NameInputField").GetComponent<RectTransform>().rect.height * unitDimension);
            this.gameObject.transform.Find("NameInputField").GetComponent<TMP_InputField>().pointSize *= unitDimension;
            this.gameObject.transform.Find("NameInputField").GetComponent<RectTransform>().anchoredPosition = new Vector2(this.gameObject.transform.Find("NameInputField").GetComponent<RectTransform>().anchoredPosition.x, -this.gameObject.transform.Find("NameInputField").GetComponent<RectTransform>().rect.height);

            this.gameObject.transform.Find("BtnPlay").GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.transform.Find("BtnPlay").GetComponent<RectTransform>().rect.width * unitDimension, this.gameObject.transform.Find("BtnPlay").GetComponent<RectTransform>().rect.height * unitDimension);
            this.gameObject.transform.Find("BtnPlay").Find("Text (TMP)").GetComponent<TextMeshProUGUI>().fontSize *= unitDimension;
            this.gameObject.transform.Find("BtnPlay").GetComponent<RectTransform>().anchoredPosition = new Vector2(this.gameObject.transform.Find("BtnPlay").GetComponent<RectTransform>().anchoredPosition.x, -(this.gameObject.transform.Find("BtnPlay").GetComponent<RectTransform>().rect.height + this.gameObject.transform.Find("NameInputField").GetComponent<RectTransform>().rect.height));
            this.gameObject.transform.Find("BtnPlay").Find("Text (TMP)").GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.transform.Find("BtnPlay").GetComponent<RectTransform>().rect.width, this.gameObject.transform.Find("BtnPlay").GetComponent<RectTransform>().rect.height);
            /*Component[] components = this.gameObject.transform.Find("NameInputField").GetComponents(typeof(Component));
            for (int i = 0; i < components.Length; i++)
            {
                Debug.Log(components[i]);
            }*/

            inputUsername.onValueChanged.AddListener(OnUsernameInputChanged);
            inputUsername.text = GameDatabase.Instance.CurrentUser.Username;
        }

        private void Update()
        {
            if (inputUsername.text == "")
                btnPlay.interactable = false;
            else
                btnPlay.interactable = true; 
        }

        private void OnDestroy()
        {
            inputUsername.onValueChanged.RemoveListener(OnUsernameInputChanged);
        }

        #region Event Handlers

        private void OnUsernameInputChanged(string newName)
        {
            GameDatabase.Instance.SetUsername(newName);
        }

        public void BtnPlayClicked()
        {
            SceneManager.LoadScene("Assets/Scenes/LevelScene.unity", LoadSceneMode.Single);
        }
        
        #endregion Event Handlers
    }
}