using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TogglesBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI additionalTextField;
    [SerializeField] private Text toggleLabel;

    public void OnToggleClick()
    {
        additionalTextField.text = toggleLabel.text;
    }
}
