using System.Collections;
using UnityEngine;

namespace Gun
{
    [RequireComponent(typeof(AudioSource))]
    public class GunController : MonoBehaviour
    {
        public Transform bulletSpawnPoint;
        public GameObject bulletPrefab;
        public float cooldownSeconds = 1;
        private float _timeOfLastShot;
        public bool useUnscaledTime;
        public AudioClip gunShotAudioClip;
        public AudioClip gunCooldownReadyAudioClip;
        private AudioSource _audioSource;

        private void Awake()
        {
            _timeOfLastShot = GetTime();
            _audioSource = GetComponent<AudioSource>();
        }

        public void Fire()
        {
            var currentTime = GetTime();
            if (currentTime > _timeOfLastShot + cooldownSeconds)
            {
                Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                _timeOfLastShot = currentTime;
                _audioSource.PlayOneShot(gunShotAudioClip);
                StartCoroutine(PlayGunReadyAudioClip());
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