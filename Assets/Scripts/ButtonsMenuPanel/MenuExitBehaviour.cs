using TMPro;
using UnityEngine;

namespace ButtonsMenuPanel
{
    public class MenuExitBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private TextMeshProUGUI description;
        
        private void OnDisable()
        {
            description.GetComponent<TextMeshProUGUI>().text = string.Empty;
            mainMenu.SetActive(true);
            transform.gameObject.SetActive(false);
        }
    }
}
