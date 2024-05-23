using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent), typeof(NavMeshObstacle))]
    public class NavMeshAgentWithObstacle : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private NavMeshObstacle _obstacle;

        private float _lastMoveTime;
        private Vector3 _lastPosition;
        private Vector3? _destination;
        public event Action OnNavEnded = delegate { };

        public bool IsStopped
        {
            get => !_agent.enabled || _agent.isStopped;
            set
            {
                if (_agent.enabled)
                {
                    _agent.isStopped = value;
                }
            }
        }

        public float Velocity => _agent.enabled ? _agent.velocity.magnitude : 0;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _obstacle = GetComponent<NavMeshObstacle>();

            _obstacle.enabled = false;
            _obstacle.carveOnlyStationary = false;
            _obstacle.carving = true;

            _lastPosition = transform.position;
            _destination = null;
        }

        private void Update()
        {
            if (Vector3.Distance(_lastPosition, transform.position) > _obstacle.carvingMoveThreshold)
            {
                _lastMoveTime = Time.time;
                _lastPosition = transform.position;
            }

            if (_lastMoveTime + _obstacle.carvingTimeToStationary < Time.time)
            {
                _agent.enabled = false;
                _obstacle.enabled = true;
                
                if (_destination is not null)
                {
                    OnNavEnded.Invoke();
                    _destination = null;
                }

            }
        }

        public void SetDestination(Vector3 position)
        {
            Debug.Log("set dest");
            _obstacle.enabled = false;
            IsStopped = false;

            _lastMoveTime = Time.time;
            _lastPosition = transform.position;

            StartCoroutine(MoveAgent(position));
        }

        private IEnumerator MoveAgent(Vector3 position)
        {
            yield return null;
            _agent.enabled = true;
            _agent.SetDestination(position);
            _destination = position;
        }
    }
}