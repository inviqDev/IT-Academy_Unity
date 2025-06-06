using TMPro;
using UnityEngine;

public class DropsBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mainMenuAdditionalText;
    private string[] _optionTexts;

    private void Start()
    {
        _optionTexts = new[]
        {
            "Option A",
            "Option B",
            "Option C",
            "Option D",
        };
    }

    public void OnDropdownOptionClick()
    {
        var value = GetComponent<TMP_Dropdown>().value;
        mainMenuAdditionalText.text = _optionTexts[value];
    }
}
