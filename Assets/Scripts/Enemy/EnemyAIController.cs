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
        private Vector3 _startingPosition;
        private SearchState _searchState = SearchState.NONE;
        private Quaternion _searchStartingRotation;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgentWithObstacle>();
            _navMeshAgent.OnNavEnded += LookAround;
            _players = GameObject.FindGameObjectsWithTag("Player");
        }

        private void Start()
        {
            _startingPosition = transform.position;
        }

        void OnDisable()
        {
            _target = null;
            _searchState = SearchState.NONE;
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
                            if (Vector3.Distance(_startingPosition, transform.position) > 3)
                            {
                                SetNavDestination(_startingPosition);
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
                    SetNavDestination(_target.transform.position);
                    _target = null;
                    Debug.Log("Player Lost!");
                }
            }
        }

        private void SetNavDestination(Vector3 target)
        {
            _navMeshAgent.SetDestination(target);
            _navMeshAgent.IsStopped = false;
        }

        private void LookAround()
        {
            _searchState = SearchState.RIGHT;
            _searchStartingRotation = transform.rotation;
            Debug.Log("nav ended");
        }

        private Quaternion GetRotationByAngle(Quaternion startingRotation, float angle)
        {
            var rotation = Quaternion.AngleAxis(angle, Vector3.up);
            return startingRotation * rotation;
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