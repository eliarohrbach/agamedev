using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilAnimationController : MonoBehaviour
{
    public GameObject gun;
    private Animator gunAnimator;
    private float gunCooldown;

    // Start is called before the first frame update
    void Start()
    {
        gunCooldown = GetComponentInParent<BulletSpawnerPlayer>().GunCooldown;
        gunAnimator = gun.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        gunAnimator.speed = Time.timeScale;

        if (Input.GetMouseButtonDown(0) && gunAnimator.GetCurrentAnimatorStateInfo(0).IsName("default"))
        {
            StartCoroutine(PlayRecoil());
        }
    }

    IEnumerator PlayRecoil()
    {
        gunAnimator.Play("Recoil");
        yield return new WaitForSeconds(gunCooldown);
        gunAnimator.Play("default");
    }
}
