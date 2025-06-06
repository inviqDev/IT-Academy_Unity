using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ButtonsMenuPanel
{
    public class ButtonTwo : MonoBehaviour
    {
        [SerializeField] private Transform description;
        
        public void OnButtonTwoClick()
        {
            if (!transform.GetComponent<Button>().interactable) return;
            
            description.GetComponent<TextMeshProUGUI>().text = "Two Clicked";
        }
    }
}
