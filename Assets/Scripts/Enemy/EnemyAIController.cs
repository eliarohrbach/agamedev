using System;
using Gun;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyAIController : MonoBehaviour
    {
        public float detectionRadius = 50;
        public float rotationSpeed = 5;
        public GunController gun;
        private GameObject _target;
        private GameObject[] _players;
        private NavMeshAgent _navMeshAgent;

        private void Start()
        {
            _players = GameObject.FindGameObjectsWithTag("Player");
            _navMeshAgent = GetComponent<NavMeshAgent>();
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
                        _navMeshAgent.isStopped = true;
                        Debug.Log("Player Detected!");
                        break;
                    }
                }
            }

            if (_target is not null)
            {
                if (CanSee(_target))
                {
                    var targetLookRotation = GetTargetLookRotation(_target.transform.position);
                    RotateTowards(targetLookRotation);
                    if (Quaternion.Angle(transform.rotation, targetLookRotation) < 1)
                    {
                        gun.Fire();
                    }
                }
                else
                {
                    _navMeshAgent.SetDestination(_target.transform.position);
                    _navMeshAgent.isStopped = false;
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

        private void RotateTowards(Quaternion rotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

        private Quaternion GetTargetLookRotation(Vector3 target)
        {
            var direction = target - transform.position;
            direction.y = 0;
            return Quaternion.LookRotation(direction);
        }
    }
}