using System.Collections;
using TMPro;
using UnityEngine;

// written by Severin Landolt

/// <summary>
/// Class <c>InstructionPanel02TextScript</c> controlles the Instruction Panel 02
/// in the Tutorial level
/// </summary>
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

    /// <summary>
    /// Coroutine <c>WaitForSeconds</c> waits for a defined time before the
    /// further code is executed
    /// </summary>
    IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
