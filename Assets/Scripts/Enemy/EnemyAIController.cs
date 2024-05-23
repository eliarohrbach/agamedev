using Enemy.ai;
using Gun;
using UnityEngine;

namespace Enemy
{
    /// <summary>
    /// Author: Alexander Wyss
    /// </summary>
    [RequireComponent(typeof(NavMeshAgentWithObstacle), typeof(EnemyHealthController))]
    public class EnemyAIController : MonoBehaviour
    {
        public float rotationSpeed = 5;
        public float fov = 180;
        public float automaticDetectionRadius = 4;
        private GameObject[] possibleTargets { get; set; }
        public NavMeshAgentWithObstacle NavMeshAgent { get; private set; }
        private Vector3 startingPosition;
        public Transform[] patrolPath;
        private PatrolState patrolState;
        public GunControllerEnemy gun;
        private EnemyHealthController _healthController;
        private IState state;

        private void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgentWithObstacle>();
            possibleTargets = GameObject.FindGameObjectsWithTag("Player");
            _healthController = GetComponent<EnemyHealthController>();
        }

        private void OnEnable()
        {
            startingPosition = transform.position;
            _healthController.OnDeath += OnDeath;
            if (patrolPath is not null && patrolPath.Length > 0)
            {
                patrolState = new PatrolState(this, patrolPath);
            }

            SetState(DefaultState());
        }

        void OnDisable()
        {
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
            foreach (var target in possibleTargets)
            {
                if (CanSee(target))
                {
                    Debug.Log("Player Detected!");
                    return target;
                }
            }

            return null;
        }

        public IState DefaultState()
        {
            if (patrolState is not null)
            {
                return patrolState;
            }

            if (Vector3.Distance(startingPosition, transform.position) > 3)
            {
                return new ChaseState(this, startingPosition);
            }

            return new WaitState(this);
        }
    }
}