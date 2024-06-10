using UnityEngine;

// written by Severin Landolt

/// <summary>
/// Class <c>GateController</c> controlles the Gate in the Tutorial level
/// </summary>
public class GateController : MonoBehaviour
{
    [SerializeField] Animator anim;

    /// <summary>
    /// Method <c>OpenGate</c> opens the Gate
    /// </summary>
    public void OpenGate()
    {
        anim.ResetTrigger("CloseGate");
        anim.SetTrigger("OpenGate");
    }

    /// <summary>
    /// Method <c>CloseGate</c> closes the Gate
    /// </summary>
    public void CloseGate()
    {
        anim.ResetTrigger("OpenGate");
        anim.SetTrigger("CloseGate");
    }
}
