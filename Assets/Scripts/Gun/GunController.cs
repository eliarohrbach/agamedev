using System;
using System.Collections;
using SceneController;
using UnityEngine;

namespace Gun
{
    /// <summary>
    /// Author: Alexander Wyss
    /// MuzzleFlash Author: Elia Rohrbach
    /// Magazin Author: Severin Landolt
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class GunController : MonoBehaviour
    {
        /// <summary>
        /// Holds the max and current bullets.
        /// </summary>
        public PistoleMagazin magazin;
        /// <summary>
        /// The bullet count UI element.
        /// </summary>
        public GameObject _bulletCounter;
        /// <summary>
        /// Wheter or not to use the magazin. False for enemies.
        /// </summary>
        public bool useMagazine;
        
        /// <summary>
        /// Where the bullet is spawned.
        /// </summary>
        public Transform bulletSpawnPoint;
        /// <summary>
        /// The reload time.
        /// </summary>
        public float cooldownSeconds = 1;
        private float _timeOfLastShot;
        /// <summary>
        /// What time scale should be used. Unscaled for Players, Scaled for Enemies.
        /// </summary>
        public bool useUnscaledTime;
        public AudioClip gunShotAudioClip;
        public AudioClip gunCooldownReadyAudioClip;
        private AudioSource _audioSource;

        /// <summary>
        /// Invoked when a bullet is successfully fired.
        /// </summary>
        public Action OnFire = delegate { };

        public ParticleSystem muzzleFlash;

        /// <summary>
        /// Initialize bullet count an time of last shot.
        /// </summary>
        private void Awake()
        {
            _timeOfLastShot = GetTime();
            _audioSource = GetComponent<AudioSource>();
            if (useMagazine)
            {
                magazin.bulletCount = magazin.maxBullet;
            }
        }
        
        /// <summary>
        /// Checks whether a bullet can be fired. (Cooldown and magazin)
        /// If it can be fired instantiates the bullet, plays the audio and visual effects,
        /// invokes OnFire and updates the bullet count. 
        /// </summary>
        public void Fire()
        {
            var currentTime = GetTime();
            if (currentTime > _timeOfLastShot + cooldownSeconds && (!useMagazine || magazin.bulletCount > 0))
            {
                ObjectPool.SharedInstance.InstantiateBullet(bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                _timeOfLastShot = currentTime;
                _audioSource.PlayOneShot(gunShotAudioClip);
                StartCoroutine(PlayGunReadyAudioClip());
                OnFire.Invoke();

                if (muzzleFlash != null)
                {
                    muzzleFlash.Play();
                }

                if (useMagazine)
                {
                    magazin.bulletCount--;
                    _bulletCounter.GetComponent<BulletCounterTextScript>().changeCounter();
                }
            }
        }

        /// <summary>
        /// Convenience method to get the correct time.
        /// </summary>
        /// <returns></returns>
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