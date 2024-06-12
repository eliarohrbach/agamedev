using System.Collections;
using UnityEngine;

/// <summary>
/// Author: Elia Rohrbach
/// Implements the gate mechanic in the first level 
/// </summary>
public class EnemyManager : MonoBehaviour
{
    public int totalEnemies = 3; // Set this to the number of enemies in your scene
    private int enemiesKilled = 0;
    public GameObject gate; // Assign the gate GameObject in the Inspector

    public void EnemyKilled()
    {
        enemiesKilled++;
        if (enemiesKilled >= totalEnemies)
        {
            OpenGate();
        }
    }

    private void OpenGate()
    {
        // Implement the logic to open the gate here
        // For example, you can start a coroutine to move the gate downwards
        StartCoroutine(MoveGateDown());
    }

    private IEnumerator MoveGateDown()
    {
        float duration = 1.0f; // The time it takes for the gate to move down
        Vector3 initialPosition = gate.transform.position;
        Vector3 targetPosition = initialPosition + new Vector3(0, -10, 0); // Move down by 5 units

        float elapsed = 0;
        while (elapsed < duration)
        {
            gate.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        gate.transform.position = targetPosition;
    }
}
