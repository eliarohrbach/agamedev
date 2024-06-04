using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// written by Elia

public class RecoilScript : MonoBehaviour
{
    public GameObject Gun;
    private Gun.GunControllerPlayer gunControllerPlayer;
    private bool isRecoiling = false; // Track the recoil state

    void Start()
    {
        // Get the GunControllerPlayer component
        gunControllerPlayer = Gun.GetComponent<Gun.GunControllerPlayer>();
        if (gunControllerPlayer == null)
        {
            Debug.LogError("No GunControllerPlayer script found on the Gun object.");
            enabled = false; // Disable this script if GunControllerPlayer is not found
            return;
        }

        // Ensure the magazin component is also set
        if (gunControllerPlayer.magazin == null)
        {
            Debug.LogError("No PistoleMagazin component found in GunControllerPlayer.");
            enabled = false; // Disable this script if PistoleMagazin is not found
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isRecoiling)
        {
            if (gunControllerPlayer.magazin.bulletCount > 0)
            {
                // Check if the gun is ready to shoot again
                float currentTime = gunControllerPlayer.useUnscaledTime ? Time.unscaledTime : Time.time;
                if (currentTime > gunControllerPlayer.cooldownSeconds + gunControllerPlayer._timeOfLastShot)
                {
                    StartCoroutine(StartRecoil());
                }
            }
            else
            {
                Debug.Log("No bullets left to fire.");
            }
        }
    }

    IEnumerator StartRecoil()
    {
        isRecoiling = true; // Set the recoil state to true
        Gun.GetComponent<Animator>().Play("Recoil");

        // Use the cooldownSeconds from the gun controller to wait
        yield return new WaitForSeconds(gunControllerPlayer.cooldownSeconds);

        // Ensure the animation stops playing by setting it to a default state
        Gun.GetComponent<Animator>().Play("New State");

        isRecoiling = false; // Reset the recoil state
    }
}
