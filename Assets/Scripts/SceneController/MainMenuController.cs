using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Methods to navigate between the different menus using buttons
public class MainMenuController : MonoBehaviour
{

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject levelSelectionMenu;
    [SerializeField] GameObject settingsMenu;

    // Opens the Main Menu
    public void OpenMainMenu()
    {
        levelSelectionMenu.SetActive(false);
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    // Opens the Settings Menu
    public void OpenSettingsMenu()
    {
        levelSelectionMenu.SetActive(false);
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    // Opens the Level Selection Menu
    public void OpenLevelSelection()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        levelSelectionMenu.SetActive(true);
    }
}
