using UnityEngine;

namespace Gun
{
    public class GunController : MonoBehaviour
    {
        public Transform bulletSpawnPoint;
        public GameObject bulletPrefab;
        public float cooldownSeconds = 1;
        private float _timeOfLastShot;
        public bool useUnscaledTime;

        void Start()
        {
            _timeOfLastShot = GetTime();
        }

        public void Fire()
        {
            var currentTime = GetTime();
            if (currentTime > _timeOfLastShot + cooldownSeconds)
            {
                Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                _timeOfLastShot = currentTime;
            }
        }

        private float GetTime()
        {
            return useUnscaledTime ? Time.unscaledTime : Time.time;
        }
    }
}