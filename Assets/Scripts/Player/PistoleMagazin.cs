using UnityEngine;

// written by Severin Landolt

// ScriptableObject that stores the current available number of bullets
// and the maximum possible number of bullets

// If necessary, the ScriptableObject allows for easily carrying over the
// current available number of bullets to the next level
// This is currently not used

[CreateAssetMenu(fileName = "PistoleMagazin", menuName = "ScriptableObjects/PistoleMagazin", order = 1)]
public class PistoleMagazin : ScriptableObject
{
    public int bulletCount;
    public int maxBullet;
}
