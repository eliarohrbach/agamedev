using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    /// <summary>
    /// Author: Alexander Wyss
    /// NavMeshAgentWithObstacle combines a normal NavMeshAgent with a NavMeshObstacle.
    /// Our enemies are a lot of the time sitting ducks. This improves the path finding of other enemies, during this time.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent), typeof(NavMeshObstacle))]
    public class NavMeshAgentWithObstacle : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private NavMeshObstacle _obstacle;

        private float _lastMoveTime;
        private Vector3 _lastPosition;
        private Vector3? _destination;
        
        /// <summary>
        /// Invoked when the current destination is reached.
        /// </summary>
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

        /// <summary>
        /// Initiate the NavMeshAgent and NavMeshObstacle.
        /// Only one can be active at any time.
        /// </summary>
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

        
        /// <summary>
        /// Checks whether the enemy is still moving or not.
        /// If it stopped moving the obstacle is enabled and the agent disabled.
        /// If a destination is currently defined. OnNavEnded is invoked. 
        /// </summary>
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

        /// <summary>
        /// Sets the navigation destination.
        /// Disables the obstacle, and enables the agent with MoveAgent. This is done in a coroutine so the obstacle and agent aren't both active in the same frame. 
        /// </summary>
        /// <param name="position"></param>
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