using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject levelSelectionMenu;

    public void OpenMainMenu()
    {
        levelSelectionMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OpenLevelSelection()
    {
        mainMenu.SetActive(false);
        levelSelectionMenu.SetActive(true);
    }
}
