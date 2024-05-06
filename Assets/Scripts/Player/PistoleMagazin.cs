using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PistoleMagazin", menuName = "ScriptableObjects/PistoleMagazin", order = 1)]
public class PistoleMagazin : ScriptableObject
{
    public int bulletCount;
    public int maxBullet;
}
