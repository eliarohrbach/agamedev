using Player;
using UnityEngine;
using UnityEngine.InputSystem;

// written by Severin Landolt

/// <summary>
/// Class <c>LevelController</c> controlles the Save- and WinZone
/// and for enabling and disabling the associated menus in a level
///
/// Pause behaviour written by Alexander Wyss
/// </summary>
public class LevelController : MonoBehaviour
{
    [SerializeField] InputActionAsset actions;
    private InputActionMap menuInputActionMap;
    private InputActionMap inputActionMap;
    private InputAction pauseAction;
    private float previousTimeScale = 1;
    private bool isPaused = false;
    [SerializeField] GameObject levelCompleteUI; // UI menu displayed when the level is completed
    [SerializeField] GameObject levelStartUI; // UI menu displayed when the scene is loaded
    [SerializeField] GameObject levelPauseUI; // UI menu displayed when the scene is paused
    [SerializeField] GameObject winZone;
    [SerializeField] GameObject timeController;
    [SerializeField] GameObject player;
    [SerializeField] GameObject ui;
    private GameObject[] saveZones;

    public bool triggerOn = false;


    /// <summary>
    /// init actions maps for pause
    /// Author: Alexander Wyss
    /// </summary>
    private void Awake()
    {
        menuInputActionMap = actions.FindActionMap("menu");
        inputActionMap = actions.FindActionMap("input");
        pauseAction = menuInputActionMap.FindAction("pause");
    }

    /// <summary>
    /// on escape we trigger pause
    /// Author: Alexander Wyss
    /// </summary>
    private void OnEnable()
    {
        pauseAction.performed += TogglePause;
    }
    /// <summary>
    /// deregister pause action listener.
    /// ensure time scale is reset, could otherwise cause problems in the next scene
    /// Author: Alexander Wyss
    /// </summary>
    private void OnDisable()
    {
        pauseAction.performed -= TogglePause;
        if (isPaused)
        {
            Time.timeScale = 1;
        }
    }
    /// <summary>
    /// Author: Alexander Wyss
    /// </summary>
    void TogglePause(InputAction.CallbackContext context)
    {
        if (isPaused)
        {
            UnPause();
        }
        else
        {
            Pause();
        }
    }

    /// <summary>
    /// Pauses the game.
    /// - Shows the Pause UI
    /// - Disables movement inputs
    /// - Disables the time controller
    /// - Sets timescale to zero
    /// - releases the cursor
    /// Author: Alexander Wyss
    /// </summary>
    void Pause()
    {
        isPaused = true;
        levelPauseUI.SetActive(true);
        inputActionMap.Disable();
        timeController.SetActive(false);
        previousTimeScale = Time.timeScale;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// Unpauses the game. Reverses actions of Pause()
    /// Author: Alexander Wyss
    /// </summary>
    public void UnPause()
    {
        isPaused = false;
        levelPauseUI.SetActive(false);
        inputActionMap.Enable();
        timeController.SetActive(true);
        Time.timeScale = previousTimeScale;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        saveZones = GameObject.FindGameObjectsWithTag("SaveZone");
        menuInputActionMap.Disable(); // pause menu can only be accessed after the game starts
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
        if (isPaused)
        {
            UnPause();
        }
        levelCompleteUI.SetActive(true);
        timeController.SetActive(false);
        ui.SetActive(false);
        player.GetComponent<PlayerCameraController>().enabled = false;
        player.GetComponent<PlayerMovementController>().enabled = false;
        player.GetComponent<PlayerGunController>().enabled = false;
        levelCompleteUI.GetComponent<Animator>().SetTrigger("Start");
        menuInputActionMap.Disable(); // pause menu can no longer be accesed
    }

    /// <summary>
    /// Method <c>GameStart</c> deactivates the StartScreen Menu and the SaveZone and start the game
    /// </summary>
    public void GameStart()
    {
        if (isPaused)
        {
            UnPause();
        }
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

        menuInputActionMap.Enable(); // pause menu can now be accessed
    }
}