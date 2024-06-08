using UnityEngine;

// written by Severin Landolt

/// <summary>
/// Class <c>MainMenuController</c> holds the Methods to navigate between
/// the different menus using buttons
/// </summary>
public class MainMenuController : MonoBehaviour
{

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject levelSelectionMenu;
    [SerializeField] GameObject settingsMenu;

    /// <summary>
    /// Method <c>OpenMainMenue</c> opens the Main Menu
    /// </summary>
    public void OpenMainMenu()
    {
        levelSelectionMenu.SetActive(false);
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    /// <summary>
    /// Method <c>OpenSettingsMenu</c> opens the Settings Menu
    /// </summary>
    public void OpenSettingsMenu()
    {
        levelSelectionMenu.SetActive(false);
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    /// <summary>
    /// Method <c>OpenLevelSelection</c> opens the Level Selection Menu
    /// </summary>
    public void OpenLevelSelection()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        levelSelectionMenu.SetActive(true);
    }
}
