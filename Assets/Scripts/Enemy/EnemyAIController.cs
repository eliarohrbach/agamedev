using Gun;
using UnityEngine;

namespace Enemy
{
    public class EnemyAIController : MonoBehaviour
    {
        public float detectionRadius = 50;
        public float rotationSpeed = 5;
        public GunController gun;
        private GameObject _target;

        void OnDisable()
        {
            _target = null;
        }

        void Update()
        {
            if (_target is null)
            {
                foreach (var overlappingCollider in Physics.OverlapSphere(transform.position, detectionRadius))
                {
                    if (overlappingCollider.gameObject.CompareTag("Player"))
                    {
                        Debug.Log("Player Detected!");
                        _target = overlappingCollider.gameObject;
                        break;
                    }
                }
            }
            else
            {
                var distanceToTarget = (_target.transform.position - transform.position).magnitude;
                if (distanceToTarget > detectionRadius)
                {
                    _target = null;
                    Debug.Log("Player Lost!");
                }
                else
                {
                    RotateTowards(_target.transform.position);
                    gun.Fire();
                }
            }
        }

        private void RotateTowards(Vector3 target)
        {
            var direction = target - transform.position;
            direction.y = 0;
            var rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
}