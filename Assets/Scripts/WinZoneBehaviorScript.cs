using UnityEngine;

// written by Severin Landolt

/// <summary>
/// Class <c>WinZoneBehavior</c> activates the GameObject WinZone when
/// all enemies have been defeated
/// </summary>
public class WinZoneBehavior : MonoBehaviour
{
    [SerializeField] Material activeMaterial;       // green
    [SerializeField] Material inactiveMaterial;     // red
    
    public bool endZoneOpen;
    public bool endLevel;
    
    private bool enemyAlive;
    private MeshRenderer _render;


    void Start()
    {
        endZoneOpen = false;
        endLevel = false;
        enemyAlive = true;
        _render = GetComponent<MeshRenderer>(); // Get the MeshRenderer Component to change the color
    }

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

    /// <summary>
    /// Method <c>EnemyDetection</c> checks if there are still enemies around
    /// </summary>
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

    // Trigger activates when the level is successfully completed
    // The variable endLevel is used in the LevelContoller Script to avtivate the LevelComplete Menu
    public void OnTriggerEnter(Collider other)
    {
        if (endZoneOpen)
        {
            endLevel = true;
        }
    }
}
