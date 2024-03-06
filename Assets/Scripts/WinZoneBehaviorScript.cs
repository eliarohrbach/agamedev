using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WinZoneBehavior : MonoBehaviour
{
    [SerializeField] Material activeMaterial;
    [SerializeField] Material inactiveMaterial;
    
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
        _render = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyDetection();

        if (!enemyAlive)
        {
            endZoneOpen = true;
        }
        else
        {
            endZoneOpen = false;
        }

        if (endZoneOpen)
        {
            _render.material = activeMaterial;
        }
        else
        {
            _render.material = inactiveMaterial;
        }

    }

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

    public void OnTriggerEnter(Collider other)
    {
        if (endZoneOpen)
        {
            endLevel = true;
        }
    }
}
