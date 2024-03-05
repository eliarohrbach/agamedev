using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject levelCompleteUI;
    public GameObject winZone;
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
    }
}
