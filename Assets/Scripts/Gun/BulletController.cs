using UnityEngine;

namespace Gun
{
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

        void Update()
        {
            transform.position += transform.forward * (Time.deltaTime * speed);
            if (Time.time > _startTime + despawnTimeSeconds)
            {
                Destroy(gameObject);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            var shootableGameObject = other.GetComponent<IDamageable>();
            shootableGameObject?.ApplyDamage();

            Destroy(gameObject);
        }
    }
}