using UnityEngine;

// written by Severin Landolt

/// <summary>
/// Class <c>Room02TriggerScript</c> closes the gate, when the player
/// enters the second room in the tutorial level
/// </summary>
public class Room02TriggerScript : MonoBehaviour
{
    [SerializeField] GameObject gate;

    // Trigger activates when the player enters the second room in
    // the tutorial level and closes the gate
    public void OnTriggerExit(Collider other)
    {
        GetComponent<BoxCollider>().isTrigger = false;
        gate.GetComponent<Animator>().ResetTrigger("OpenGate");
        gate.GetComponent<Animator>().SetTrigger("CloseGate");
    }
}
