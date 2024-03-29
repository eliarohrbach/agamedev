﻿using System;
using Gun;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgentWithObstacle))]
    public class EnemyAIController : MonoBehaviour
    {
        public float rotationSpeed = 5;
        public float fov = 180;
        public float automaticDetectionRadius = 4;
        public GunController gun;
        private GameObject _target;
        private GameObject[] _players;
        private NavMeshAgentWithObstacle _navMeshAgent;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgentWithObstacle>();
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
                        _navMeshAgent.IsStopped = true;
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
                    _navMeshAgent.IsStopped = false;
                    _target = null;
                    Debug.Log("Player Lost!");
                }
            }
        }

        private bool CanSee(GameObject target)
        {
            var targetDirection = target.transform.position - transform.position;

            if (targetDirection.magnitude < automaticDetectionRadius)
            {
                return true;
            }

            if (Vector3.Angle(transform.forward, targetDirection) > fov / 2)
            {
                return false;
            }

            RaycastHit raycastHit;
            Debug.DrawRay(transform.position, targetDirection, Color.red);
            if (Physics.Raycast(transform.position, targetDirection, out raycastHit))
            {
                return raycastHit.transform.CompareTag("Player");
            }

            return false;
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