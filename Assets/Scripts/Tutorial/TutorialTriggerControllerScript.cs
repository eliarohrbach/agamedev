using UnityEngine;

// written by Severin Landolt

/// <summary>
/// Class <c>TutorialTriggerControllerScript</c> starts the Tutorial
/// </summary>
public class TutorialTriggerControllerScript : MonoBehaviour
{

    [SerializeField] GameObject tutorialController;
    
    public bool moveBarrel = false;

    // Trigger starts the Tutorial and movement of the barrels
    public void OnTriggerExit(Collider other)
    {
        tutorialController.SetActive(true);
        moveBarrel = true;
    }
}
