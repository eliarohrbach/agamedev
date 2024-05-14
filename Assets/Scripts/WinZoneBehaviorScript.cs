using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WinZoneBehavior : MonoBehaviour
{
    [SerializeField] Material activeMaterial;       // green
    [SerializeField] Material inactiveMaterial;     // red
    
    public bool endZoneOpen;
    public bool endLevel;
    
    private bool enemyAlive;
    private MeshRenderer _render;


    // Start is called before the first frame update
    void Start()
    {
        endZoneOpen = false;
        endLevel = false;
        enemyAlive = true;
        _render = GetComponent<MeshRenderer>(); // Get the MeshRenderer Component to change the color
    }

    // Update is called once per frame
    void Update()
    {
        EnemyDetection();

        // Opens the WinZone if there are no enemies alive
        if (!enemyAlive)
        {
            endZoneOpen = true;
        }
        else
        {
            endZoneOpen = false;
        }

        // Changes the color of the WinZone when it is open
        if (endZoneOpen)
        {
            _render.material = activeMaterial;
        }
        else
        {
            _render.material = inactiveMaterial;
        }

    }

    // Method to check if there are still enemies around
    void EnemyDetection()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies == null || enemies.Length == 0)
        {
            enemyAlive = false;
        }
        else
        {
            enemyAlive = true;
        }
    }

    // Trigger that activates when the level is successfully completed
    // The variable endLevel is used in the LevelContoller Script to avtivate the LevelComplete Menu
    public void OnTriggerEnter(Collider other)
    {
        if (endZoneOpen)
        {
            endLevel = true;
        }
    }
}
