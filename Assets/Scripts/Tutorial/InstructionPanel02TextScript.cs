using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InstructionPanel02TextScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI enemieCounterText;
    [SerializeField] GameObject enemy;

    private Animator animator;
    private int oldEnemyCount;
    private int newEnemyCount;

    public int count;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        oldEnemyCount = 3;
        count = 0;

        animator.SetBool("Panel02In", true);
    }

    void Update()
    {
        if (enemy.activeSelf)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            newEnemyCount = enemies.Length;

            if (newEnemyCount < oldEnemyCount)
            {
                count += 1;
                oldEnemyCount = newEnemyCount;
            }

            enemieCounterText.text = count + " / 3";

            if (count == 3)
            {
                StartCoroutine(WaitForSeconds(5));
                enemieCounterText.text = "completed";
            }
        }
    }

    IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
