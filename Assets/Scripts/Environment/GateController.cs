using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] Animator anim;

    public void OpenGate()
    {
        anim.ResetTrigger("CloseGate");
        anim.SetTrigger("OpenGate");
    }

    public void CloseGate()
    {
        anim.ResetTrigger("OpenGate");
        anim.SetTrigger("CloseGate");
    }
}
