using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// written by Elia

public class RecoilScript : MonoBehaviour
{
    public GameObject Gun;
    private Gun.GunController _gunController;
    private bool isRecoiling = false; // Track the recoil state

    private void Awake()
    {
        // Get the GunControllerPlayer component
        _gunController = Gun.GetComponent<Gun.GunController>();
        if (_gunController == null)
        {
            Debug.LogError("No GunControllerPlayer script found on the Gun object.");
            enabled = false; // Disable this script if GunControllerPlayer is not found
            return;
        }

        // Ensure the magazin component is also set
        if (_gunController.magazin == null)
        {
            Debug.LogError("No PistoleMagazin component found in GunControllerPlayer.");
            enabled = false; // Disable this script if PistoleMagazin is not found
        }
    }

    private void OnEnable()
    {
        _gunController.OnFire += TriggerRecoil;
    }

    private void OnDisable()
    {
        _gunController.OnFire -= TriggerRecoil;
    }

    private void TriggerRecoil()
    {
        if (!isRecoiling)
        {
            StartCoroutine(StartRecoil());
        }
    }

    IEnumerator StartRecoil()
    {
        isRecoiling = true; // Set the recoil state to true
        Gun.GetComponent<Animator>().Play("Recoil");

        // Use the cooldownSeconds from the gun controller to wait
        yield return new WaitForSecondsRealtime(_gunController.cooldownSeconds);

        // Ensure the animation stops playing by setting it to a default state
        Gun.GetComponent<Animator>().Play("New State");

        isRecoiling = false; // Reset the recoil state
    }
}
