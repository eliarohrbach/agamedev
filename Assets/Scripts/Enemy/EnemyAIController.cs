using System;
using Gun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    enum SearchState
    {
        NONE,
        RIGHT,
        RETURNING,
        LEFT,
        FINAL_RETURNING
    }

    public enum AiState
    {
        Waiting,
        Firing,
        Chasing,
        Searching,
        Patrolling
    }

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
        private GameObject _target;
        private GameObject[] _players;
        private SearchState _searchState = SearchState.NONE;
        private Quaternion _searchStartingRotation;
        public Transform[] patrolPath;
        private int _currentPatrolIndex;
        private bool _isPatrolling;
        private EnemyHealthController _healthController;

        private void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgentWithObstacle>();
            PossibleTargets = GameObject.FindGameObjectsWithTag("Player");
            
            NavMeshAgent.OnNavEnded += OnNavReached;
            _healthController = GetComponent<EnemyHealthController>();
        }

        private void Start()
        {
            StartingPosition = transform.position;
        }

        private void OnEnable()
        {
            _healthController.OnDeath += OnDeath;
        }

        void OnDisable()
        {
            _target = null;
            _searchState = SearchState.NONE;
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
            if (_target is null)
            {
                if (_searchState == SearchState.RIGHT)
                {
                    var targetRotation = GetRotationByAngle(_searchStartingRotation, 110f);
                    RotateTowards(targetRotation);
                    if (Quaternion.Angle(transform.rotation, targetRotation) < 1)
                    {
                        _searchState = SearchState.RETURNING;
                    }
                }
                else if (_searchState is SearchState.RETURNING or SearchState.FINAL_RETURNING)
                {
                    RotateTowards(_searchStartingRotation);
                    if (Quaternion.Angle(transform.rotation, _searchStartingRotation) < 1)
                    {
                        if (_searchState == SearchState.RETURNING)
                        {
                            _searchState = SearchState.LEFT;
                        }
                        else
                        {
                            _searchState = SearchState.NONE;
                            if (Vector3.Distance(StartingPosition, transform.position) > 3)
                            {
                                SetNavDestination(StartingPosition);
                            }
                        }
                    }
                }
                else if (_searchState == SearchState.LEFT)
                {
                    var targetRotation = GetRotationByAngle(_searchStartingRotation, -110f);
                    RotateTowards(targetRotation);
                    if (Quaternion.Angle(transform.rotation, targetRotation) < 1)
                    {
                        _searchState = SearchState.FINAL_RETURNING;
                    }
                }
                else if (patrolPath is not null && patrolPath.Length > 0 && !_isPatrolling)
                {
                    SetNavDestination(patrolPath[_currentPatrolIndex].position);
                    _currentPatrolIndex++;
                    if (_currentPatrolIndex >= patrolPath.Length)
                    {
                        _currentPatrolIndex = 0;
                    }

                    _isPatrolling = true;
                }


                foreach (var player in _players)
                {
                    if (CanSee(player))
                    {
                        _target = player;
                        NavMeshAgent.IsStopped = true;
                        _searchState = SearchState.NONE;
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
                        Fire();
                    }
                }
                else
                {
                    SetNavDestination(_target.transform.position);
                    _target = null;
                    Debug.Log("Player Lost!");
                }
            }
        }

        public void Fire()
        {
            gun.Fire();
        }

        public void SetNavDestination(Vector3 target)
        {
            NavMeshAgent.SetDestination(target);
            NavMeshAgent.IsStopped = false;
        }

        private void OnNavReached()
        {
            _searchState = SearchState.RIGHT;
            _searchStartingRotation = transform.rotation;
            _isPatrolling = false;
            Debug.Log("nav ended");
        }

        private Quaternion GetRotationByAngle(Quaternion startingRotation, float angle)
        {
            var rotation = Quaternion.AngleAxis(angle, Vector3.up);
            return startingRotation * rotation;
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

        private Quaternion GetTargetLookRotation(Vector3 target)
        {
            var direction = target - transform.position;
            direction.y = 0;
            return Quaternion.LookRotation(direction);
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