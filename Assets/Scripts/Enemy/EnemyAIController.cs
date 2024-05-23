using System;
using Enemy.ai;
using Gun;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    /// <summary>
    /// Author: Alexander Wyss
    /// </summary>
    [RequireComponent(typeof(NavMeshAgentWithObstacle), typeof(EnemyHealthController))]
    public class EnemyAIController : MonoBehaviour
    {
        public GameObject[] PossibleTargets { get; private set; }

        public NavMeshAgentWithObstacle NavMeshAgent { get; private set; }
        public Vector3 StartingPosition { get; private set; }

        public float rotationSpeed = 5;
        public float fov = 180;
        public float automaticDetectionRadius = 4;
        public GunControllerEnemy gun;
        public Transform[] patrolPath;
        private int _currentPatrolIndex;
        private bool _isPatrolling;
        private EnemyHealthController _healthController;
        private IState state;

        private void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgentWithObstacle>();
            PossibleTargets = GameObject.FindGameObjectsWithTag("Player");
            _healthController = GetComponent<EnemyHealthController>();
        }

        private void Start()
        {
            StartingPosition = transform.position;
        }

        private void OnEnable()
        {
            _healthController.OnDeath += OnDeath;
            SetState(new WaitState(this));
        }

        void OnDisable()
        {
            _currentPatrolIndex = 0;
            _isPatrolling = false;
            _healthController.OnDeath -= OnDeath;
        }

        private void OnDeath()
        {
            enabled = false;
        }

        void Update()
        {
            var newState = state.Update();
            SetState(newState);
            /*
                  if (patrolPath is not null && patrolPath.Length > 0 && !_isPatrolling)
                 {
                     SetNavDestination(patrolPath[_currentPatrolIndex].position);
                     _currentPatrolIndex++;
                     if (_currentPatrolIndex >= patrolPath.Length)
                     {
                         _currentPatrolIndex = 0;
                     }

                     _isPatrolling = true;
                 }*/
        }

        private void SetState(IState state)
        {
            if (state != this.state)
            {
                this.state = state;
                this.state.Start();
            }
        }

        public void Fire()
        {
            gun.Fire();
        }

        public bool CanSee(GameObject target)
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

        public void RotateTowards(Quaternion rotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

        public GameObject CheckForTargetInSight()
        {
            foreach (var target in PossibleTargets)
            {
                if (CanSee(target))
                {
                    Debug.Log("Player Detected!");
                    return target;
                }
            }

            return null;
        }
    }
}