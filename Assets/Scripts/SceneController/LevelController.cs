using Player;
using UnityEngine;

// written by Severin Landolt

/// <summary>
/// Class <c>LevelController</c> controlles the Save- and WinZone
/// and for enabling and disabling the associated menus in a level
/// </summary>
public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject levelCompleteUI;    // UI menu displayed when the level is completed
    [SerializeField] GameObject levelStartUI;       // UI menu displayed when the scene is loaded
    [SerializeField] GameObject winZone;
    [SerializeField] GameObject timeController;
    [SerializeField] GameObject player;
    [SerializeField] GameObject ui;
    private GameObject[] saveZones;

    public bool triggerOn = false;


    private void Start()
    {
        saveZones = GameObject.FindGameObjectsWithTag("SaveZone");
    }

    void Update()
    {
        // Checks if the level was successfully completed and activates the LevelComplete menu
        triggerOn = winZone.GetComponent<WinZoneBehavior>().endLevel;

        if (triggerOn == true)
        {
            EndLevel();
        }
    }

    /// <summary>
    /// Method <c>EndLevel</c> activates the LevelComplete Menu and pause the game
    /// </summary>
    void EndLevel()
    {
        levelCompleteUI.SetActive(true);
        timeController.SetActive(false);
        ui.SetActive(false);
        player.GetComponent<PlayerCameraController>().enabled = false;
        player.GetComponent<PlayerMovementController>().enabled = false;
        player.GetComponent<PlayerGunController>().enabled = false;
        levelCompleteUI.GetComponent<Animator>().SetTrigger("Start");
    }

    /// <summary>
    /// Method <c>GameStart</c> deactivates the StartScreen Menu and the SaveZone and start the game
    /// </summary>
    public void GameStart()
    {
        levelStartUI.SetActive(false);
        timeController.SetActive(true);
        ui.SetActive(true);
        player.GetComponent<PlayerCameraController>().enabled = true;
        player.GetComponent<PlayerMovementController>().enabled = true;
        player.GetComponent<PlayerGunController>().enabled = true;
        foreach (var saveZone in saveZones)
        {
            saveZone.SetActive(false);   
        }
    }
}