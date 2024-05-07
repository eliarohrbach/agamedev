using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletCounterTextScript : MonoBehaviour
{
    [SerializeField] PistoleMagazin pistoleMagazin;
    private TextMeshProUGUI bulletCounter;


    private void Awake()
    {
        bulletCounter = GetComponentInChildren<TextMeshProUGUI>();
        bulletCounter.text = pistoleMagazin.bulletCount + " / " + pistoleMagazin.maxBullet;
    }
    public void changeCounter()
    {
        bulletCounter.text = pistoleMagazin.bulletCount + " / " + pistoleMagazin.maxBullet;
    }
}
