using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Elia Rohrbach
// Summary: Play Recoild animation when Trigger is activated 

public class RecoilScript : MonoBehaviour
{
    public GameObject Gun;
    private Gun.GunController _gunController;
    private bool isRecoiling = false;

    private void Awake()
    {
        _gunController = Gun.GetComponent<Gun.GunController>();
        if (_gunController == null)
        {
            Debug.LogError("No GunControllerPlayer script found on the Gun object.");
            enabled = false;
            return;
        }


        if (_gunController.magazin == null)
        {
            Debug.LogError("No PistoleMagazin component found in GunControllerPlayer.");
            enabled = false;
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
        isRecoiling = true;
        Gun.GetComponent<Animator>().Play("Recoil");

        yield return new WaitForSecondsRealtime(_gunController.cooldownSeconds);

        Gun.GetComponent<Animator>().Play("New State");

        isRecoiling = false;
    }
}
