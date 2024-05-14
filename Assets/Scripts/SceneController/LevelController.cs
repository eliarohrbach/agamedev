using System;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.HighDefinition.ScalableSettingLevelParameter;
using UnityEditor;

// Script for controlling the Save- and WinZone
// and for enabling and disabling the associated menus in a Level
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

    // Method to activate the LevelComplete Menu and pause the game
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

    // Method to deactivate the StartScreen Menu and the SaveZone and start the game
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