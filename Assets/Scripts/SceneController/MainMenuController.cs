using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject levelSelectionMenu;
    [SerializeField] GameObject settingsMenu;

    public void OpenMainMenu()
    {
        levelSelectionMenu.SetActive(false);
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    
    public void OpenSettingsMenu()
    {
        levelSelectionMenu.SetActive(false);
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }


    public void OpenLevelSelection()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        levelSelectionMenu.SetActive(true);
    }
}
