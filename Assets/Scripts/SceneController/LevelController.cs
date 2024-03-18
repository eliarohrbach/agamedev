using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject levelCompleteUI;
    [SerializeField] GameObject levelStartUI;
    [SerializeField] GameObject winZone;
    [SerializeField] GameObject timeController;
    [SerializeField] GameObject player;
    public bool triggerOn = false;


    void Update()
    {
        triggerOn = winZone.GetComponent<WinZoneBehavior>().endLevel;

        if (triggerOn == true)
        {
            EndLevel();
        }
    }

    void EndLevel()
    {
        levelCompleteUI.SetActive(true);
        timeController.SetActive(false);
        player.GetComponent<PlayerCameraController>().enabled = false;
        player.GetComponent<PlayerGunController>().enabled = false;
        Time.timeScale = 0;
    }

    public void GameStart()
    {
        levelStartUI.SetActive(false);
        timeController.SetActive(true);
        player.GetComponent<PlayerCameraController>().enabled = true;
        player.GetComponent<PlayerMovementController>().enabled = true;
        player.GetComponent<PlayerGunController>().enabled = true;
    }
}
