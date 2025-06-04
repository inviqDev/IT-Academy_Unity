using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ButtonsMenu
{
    public class UIButtonsMenuButtonTwoBehaviour : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _descriptionField;
        private string _buttonOneDescriptionText = "Two clicked";

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() => _descriptionField.text = _buttonOneDescriptionText);
        }
    }
}