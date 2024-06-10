using TMPro;
using UnityEngine;

// written by Severin Landolt

/// <summary>
/// Class <c>BulletCounterTextScript</c> controlles the bullet counter in the UI
/// </summary>
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

    /// <summary>
    /// Method <c>changeCounter</c> updates the UI Bullet Counter
    /// </summary>
    public void changeCounter()
    {
        bulletCounter.text = pistoleMagazin.bulletCount + " / " + pistoleMagazin.maxBullet;
    }
}
