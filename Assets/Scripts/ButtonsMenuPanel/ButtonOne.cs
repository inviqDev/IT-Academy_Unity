using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ButtonsMenuPanel
{
    public class ButtonOne : MonoBehaviour
    {
        [SerializeField] private Transform description;

        public void OnButtonOneClick()
        {
            if (!transform.GetComponent<Button>().interactable) return;
            
            description.GetComponent<TextMeshProUGUI>().text = "One Clicked";
        }
    }
}