using System.Collections;
using UnityEngine;

// written by Severin Landolt

/// <summary>
/// Class <c>TutorialControllerScript</c> controlles the Tutorial instructions
/// </summary>
public class TutorialControllerScript : MonoBehaviour
{
    [SerializeField] GameObject Panel01;
    [SerializeField] GameObject Panel02;
    [SerializeField] GameObject gate;
    [SerializeField] GameObject enemies;

    private Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();

        animator.SetBool("Panel01In", true);

        gate.GetComponent<Animator>().ResetTrigger("CloseGate");
    }

    void Update()
    {
        if (GetComponentInChildren<InstructionPanel01TextScript>().count == 4)
        {
            StartCoroutine(WaitForSeconds(8));
            animator.SetBool("Panel01In", false);

            animator.SetBool("Panel02In", true);
            gate.GetComponent<Animator>().SetTrigger("OpenGate");
            enemies.SetActive(true);
        }

        if (GetComponentInChildren<InstructionPanel02TextScript>().count == 3)
        {
            StartCoroutine(WaitForSeconds(8));
            animator.SetBool("Panel02In", false);
        }
    }

    /// <summary>
    /// Coroutine <c>WaitForSeconds</c> waits for a defined time before the
    /// further code is executed
    /// </summary>
    IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
