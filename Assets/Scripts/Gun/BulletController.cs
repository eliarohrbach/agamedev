using UnityEngine;

namespace Gun
{
    
    /// <summary>
    /// Author: Alexander Wyss
    /// Controls the bullet speed. If a collider is hit, ApplyDamage is invoked on the hit object if it implements IDamageable.
    /// If the bullet still exists after the defined time, it is automatically destroyed. So Bullets shot straight in the air won't exist forever.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class BulletController : MonoBehaviour
    {
        public float speed = 20;
        public float despawnTimeSeconds = 100;
        private float _startTime;

        private void Start()
        {
            _startTime = Time.time;
        }

        private void Update()
        {
            transform.position += transform.forward * (Time.deltaTime * speed);
            if (Time.time > _startTime + despawnTimeSeconds)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var shootableGameObject = other.GetComponent<IDamageable>();
            shootableGameObject?.ApplyDamage();

            Destroy(gameObject);
        }
    }
}