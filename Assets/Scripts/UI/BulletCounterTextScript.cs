using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletCounterTextScript : MonoBehaviour
{
    [SerializeField] PistoleMagazin pistoleMagazin;
    [SerializeField] TextMeshProUGUI bulletCounter;


    private void Awake()
    {
        bulletCounter.text = pistoleMagazin.bulletCount + " / " + pistoleMagazin.maxBullet;
    }
    public void changeCounter()
    {
        pistoleMagazin.bulletCount--;
        bulletCounter.text = pistoleMagazin.bulletCount + " / " + pistoleMagazin.maxBullet;
    }
}
