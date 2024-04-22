using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggerControllerScript : MonoBehaviour
{

    [SerializeField] GameObject tutorialController;
    
    public bool moveBarrel = false;



    public void OnTriggerExit(Collider other)
    {
        tutorialController.SetActive(true);
        moveBarrel = true;
    }
}
