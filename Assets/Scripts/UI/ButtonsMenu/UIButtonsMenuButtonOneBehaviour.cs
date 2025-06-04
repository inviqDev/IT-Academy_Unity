using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ButtonsMenu
{
    public class UIButtonsMenuButtonOneBehaviour : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _descriptionField;
        private string _buttonOneDescriptionText = "One clicked";

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() => _descriptionField.text = _buttonOneDescriptionText);
        }
    }
}