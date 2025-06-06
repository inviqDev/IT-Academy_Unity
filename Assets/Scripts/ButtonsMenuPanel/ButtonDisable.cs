using UnityEngine;
using UnityEngine.UI;

namespace ButtonsMenuPanel
{
    public class ButtonDisable : MonoBehaviour
    {
        public void OnButtonDisableClick()
        {
            if (!transform.GetComponent<Button>().interactable) return;
            
            for (var i = 0; i < transform.parent.childCount; i++)
            {
                transform.parent.GetChild(i).GetComponent<Button>().interactable = false;
            }
        }
    }
}
