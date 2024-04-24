using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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



    IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
