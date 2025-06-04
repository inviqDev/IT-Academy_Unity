using UnityEngine;
using UnityEngine.UI;

namespace UI.ButtonsMenu
{
    public class UIButtonsMenuButtonDisableBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject[] buttons;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(DisableButtons);
        }

        private void DisableButtons()
        {
            foreach (var button in buttons) 
            {
                button.gameObject.GetComponent<Button>().interactable = false;
            }
        }
    }
}