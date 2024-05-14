using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletCounterTextScript : MonoBehaviour
{
    [SerializeField] PistoleMagazin pistoleMagazin;
    private TextMeshProUGUI bulletCounter;

    // Gets the current Bullet Counter when the GameObject awakens
    private void Awake()
    {
        bulletCounter = GetComponentInChildren<TextMeshProUGUI>();
        bulletCounter.text = pistoleMagazin.bulletCount + " / " + pistoleMagazin.maxBullet;
    }

    // Method to update the UI Bullet Counter
    public void changeCounter()
    {
        bulletCounter.text = pistoleMagazin.bulletCount + " / " + pistoleMagazin.maxBullet;
    }
}
