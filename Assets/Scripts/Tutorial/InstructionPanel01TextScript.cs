using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InstructionPanel01TextScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI barrelCounterText;

    private Animator animator;
    private int oldBarrelCount;
    private int newBarrelCount;

    public int count;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        oldBarrelCount = 4;
        count = 0;

        animator.SetBool("Panel01In", true);
    }
    
    void Update()
    {
        GameObject[] barrels = GameObject.FindGameObjectsWithTag("Barrel");
        newBarrelCount = barrels.Length;

        if (newBarrelCount < oldBarrelCount)
        {
            count += 1;
            oldBarrelCount = newBarrelCount;
        }

        barrelCounterText.text = count + " / 4";

        if (count == 4)
        {
            StartCoroutine(WaitForSeconds(3));
            barrelCounterText.text = "completed";
        }
    }

    IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
