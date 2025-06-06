using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI menuDescription;
    [SerializeField] private GameObject mainMenuButtonsPanel;
    [SerializeField] private Button backButton;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [SerializeField] private GameObject[] menus;

    private Stack<GameObject> _menusHistory = new();

    public void OnButtonsButtonClick()
    {
        var buttonsMenu = menus[0];
        var buttonsAmount = buttonsMenu.transform.childCount;

        if (buttonsAmount > 0)
        {
            for (var i = 0; i < buttonsAmount; i++)
            {
                buttonsMenu.transform.GetChild(i).GetComponent<Button>().interactable = true;
            }

            MoveFromMainMenu(buttonsMenu, "Buttons");
        }
        else
        {
            Debug.Log("Can't go inside the Buttons menu because it is empty");
        }
    }

    public void OnTogglesButtonClick()
    {
        var togglesMenu = menus[1];
        var togglesAmount = menus[1].transform.childCount;

        if (togglesAmount > 0)
        {
            for (var i = 0; i < togglesAmount; i++)
            {
                togglesMenu.transform.GetChild(i).GetComponent<Toggle>().isOn = false;
            }

            MoveFromMainMenu(togglesMenu, "Toggles");
        }
        else
        {
            Debug.Log("Can't go inside the Toggles menu because it is empty");
        }
    }
    
    public void OnDropsButtonClick()
    {
        var dropsMenu = menus[2];
        if (dropsMenu.transform.childCount > 0)
        {
            MoveFromMainMenu(dropsMenu, "Drops");
        }
        else
        {
            Debug.Log("Can't go inside the Drops menu because it is empty");
        }
    }
    
    public void OnInputButtonClick()
    {
        var inputMenu = menus[3];
        if (inputMenu.transform.childCount > 0)
        {
            MoveFromMainMenu(inputMenu, "Input");
        }
        else
        {
            Debug.Log("Can't go inside the Input menu because it is empty");
        }

    }
    
    public void OnScrollViewButtonClick()
    {
        var scrollViewMenu = menus[4];
        if (scrollViewMenu.transform.childCount > 0)
        {
            MoveFromMainMenu(scrollViewMenu, "Scroll View");
        }
        else
        {
            Debug.Log("Can't go inside the Scroll View menu because it is empty");
        }
    }

    private void MoveFromMainMenu(GameObject targetMenu, string menuTitle)
    {
        // activate necessary UI elements
        backButton.gameObject.SetActive(true);
        descriptionText.gameObject.SetActive(true);

        _menusHistory.Push(targetMenu);
        menuDescription.text = menuTitle;
        targetMenu.gameObject.SetActive(true);

        // deactivate main menu UI elements panel
        mainMenuButtonsPanel.gameObject.SetActive(false);
    }

    public void OnBackButtonClick()
    {
        if (_menusHistory.Count > 0)
        {
            _menusHistory.Peek().SetActive(false);
        }
        else
        {
            mainMenuButtonsPanel.SetActive(true);
        }

        menuDescription.text = "Main Menu";
        backButton.gameObject.SetActive(false);
    }
}