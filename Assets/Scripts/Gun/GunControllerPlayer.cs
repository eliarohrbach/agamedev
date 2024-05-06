using System;
using System.Collections;
using UnityEngine;

namespace Gun
{
    [RequireComponent(typeof(AudioSource))]
    public class GunControllerPlayer : MonoBehaviour
    {
        public PistoleMagazin magazin;
        public GameObject _bulletCounter;
        public Transform bulletSpawnPoint;
        public GameObject bulletPrefab;
        public float cooldownSeconds = 1;
        private float _timeOfLastShot;
        public bool useUnscaledTime;
        public AudioClip gunShotAudioClip;
        public AudioClip gunCooldownReadyAudioClip;
        private AudioSource _audioSource;

        public Action OnFire = delegate { };

        private void Awake()
        {
            _timeOfLastShot = GetTime();
            _audioSource = GetComponent<AudioSource>();
            magazin.bulletCount = magazin.maxBullet;
        }

        public void Fire()
        {
            var currentTime = GetTime();
            if (currentTime > _timeOfLastShot + cooldownSeconds && magazin.bulletCount > 0)
            {
                Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                _timeOfLastShot = currentTime;
                _audioSource.PlayOneShot(gunShotAudioClip);
                StartCoroutine(PlayGunReadyAudioClip());
                OnFire.Invoke();

                _bulletCounter.GetComponent<BulletCounterTextScript>().changeCounter();
            }
        }

        private float GetTime()
        {
            return useUnscaledTime ? Time.unscaledTime : Time.time;
        }

        private IEnumerator PlayGunReadyAudioClip()
        {
            if (useUnscaledTime)
            {
                yield return new WaitForSecondsRealtime(cooldownSeconds);
            }
            else
            {
                yield return new WaitForSeconds(cooldownSeconds);
            }

            _audioSource.PlayOneShot(gunCooldownReadyAudioClip);
        }
    }
}