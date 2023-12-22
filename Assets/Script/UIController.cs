using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject deathScreen;

    private float restartThreshhold = 1.5f;
    private float restartTimer;
    private Scene activeScene;
    private bool playerDead;
    private PlayerController playerController;

    public bool isGamePaused { get; private set; } = true;

    // Start is called before the first frame update
    void Start()
    {
        deathScreen.SetActive(false);
        startScreen.SetActive(true);
        activeScene = SceneManager.GetActiveScene();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startScreen.activeSelf && Input.anyKey)
        {
            startScreen.SetActive(false);
            isGamePaused = false;
        }

        playerDead = playerController.isPlayerDead;

        if (Input.GetKeyDown(KeyCode.R)) 
        {
            if (playerDead) RestartLevel();
            restartTimer = Time.unscaledTime + restartThreshhold;
        }
        if (Input.GetKey(KeyCode.R) && Time.unscaledTime > restartTimer)
        {
            RestartLevel();
        }
        if (playerDead)
        {
            deathScreen.SetActive(true);
            isGamePaused = true;
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(activeScene.name);
    }
}
