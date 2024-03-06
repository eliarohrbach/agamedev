using System;
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
        private GameObject[] _players;

        private void Start()
        {
            _players = GameObject.FindGameObjectsWithTag("Player");
        }

        void OnDisable()
        {
            _target = null;
        }

        void Update()
        {
            if (_target is null)
            {
                foreach (var player in _players)
                {
                    if (CanSee(player))
                    {
                        _target = player;
                        Debug.Log("Player Detected!");
                        break;
                    }
                }
            }
            else
            {
                if (CanSee(_target))
                {
                    RotateTowards(_target.transform.position);
                    gun.Fire();
                }
                else
                {
                    _target = null;
                    Debug.Log("Player Lost!");
                }
            }
        }

        private bool CanSee(GameObject target)
        {
            if (DistanceToTarget(target) < detectionRadius)
            {
                RaycastHit raycastHit;
                Debug.DrawRay(transform.position, target.transform.position - transform.position, Color.red);
                if (Physics.Raycast(transform.position, target.transform.position - transform.position, out raycastHit,
                        detectionRadius))
                {
                    return raycastHit.transform.CompareTag("Player");
                }
            }

            return false;
        }

        private float DistanceToTarget(GameObject target)
        {
            return (target.transform.position - transform.position).magnitude;
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