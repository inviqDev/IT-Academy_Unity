using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenuBottomElements
{
    public class UIMainMenuBackButtonBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenuBottomEleents;
        [SerializeField] private GameObject descriptionField;
        [SerializeField] private GameObject buttonsMenu;
        
        private Button _backButton;

        private void Start()
        {
            _backButton = GetComponent<Button>();
            _backButton.onClick.AddListener(OnBackButtonClick);
        }

        private void OnBackButtonClick()
        {
            mainMenuBottomEleents.SetActive(true);
            
            buttonsMenu.SetActive(false);
            descriptionField.SetActive(false);
            _backButton.gameObject.SetActive(false);
        }
    }
}
