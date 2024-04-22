using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room02TriggerScript : MonoBehaviour
{
    [SerializeField] GameObject gate;


    public void OnTriggerExit(Collider other)
    {
        GetComponent<BoxCollider>().isTrigger = false;
        gate.GetComponent<Animator>().ResetTrigger("OpenGate");
        gate.GetComponent<Animator>().SetTrigger("CloseGate");
    }
}
