using Gun;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Collider))]
    public class EnemyAIController : MonoBehaviour
    {
        public float detectionRadius = 50;
        public float rotationSpeed = 5;
        public GameObject gun;
        private GunController _gunController;
        private GameObject _target;

        void Start()
        {
            _gunController = gun.GetComponent<GunController>();
        }

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
                    Debug.Log("Player lost!");
                }
                else
                {
                    RotateTowards(_target.transform.position);
                    _gunController.Fire();
                }
            }
        }

        private void RotateTowards(Vector3 target)
        {
            Vector3 dir = target - transform.position;
            dir.y = 0;
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
        }
    }
}