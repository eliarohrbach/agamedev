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
        private AudioSource _audioSource;

        void Start()
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
            }
        }

        private float GetTime()
        {
            return useUnscaledTime ? Time.unscaledTime : Time.time;
        }
    }
}