using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenuBottomElements
{
    public class UIMainMenuButtonsButtonBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenuBackButton;
        [SerializeField] private GameObject description;
        [SerializeField] private GameObject buttonsMenu;
        
        private GameObject _parent;
        private Button _button;

        private void Start()
        {
            _parent = transform.parent.gameObject;
            _button = GetComponent<Button>();
            
            _button.onClick.AddListener(OnButtonsButtonClick);
        }

        private void OnButtonsButtonClick()
        {
            _parent.SetActive(false);
            
            mainMenuBackButton.SetActive(true);
            description.SetActive(true);
            buttonsMenu.SetActive(true);
        }
    }
}
