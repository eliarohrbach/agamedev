using Enemy.ai;
using Gun;
using UnityEngine;
using IState = Enemy.ai.IState;

namespace Enemy
{
    /// <summary>
    /// Author: Alexander Wyss
    /// The Enemy AI Controller is the master script of the AI. It manages the state machine and provides common actions for the different state.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgentWithObstacle), typeof(EnemyHealthController))]
    public class EnemyAIController : MonoBehaviour
    {
        /// <summary>
        /// How fast the enemy can rotate.
        /// </summary>
        public float rotationSpeed = 5;
        /// <summary>
        /// Defines the width of the enemies line of sight in deg.
        /// </summary>
        public float fov = 180;
        /// <summary>
        /// If a target is within this radius, it is automatically detected, no matter the line of sight.
        /// </summary>
        public float automaticDetectionRadius = 4;
        /// <summary>
        /// Initialized with all possible targets on awake. Only targets in this list will be considered by EnemyAIController.CheckForTargetInSight.
        /// This is for performence reasons.
        /// </summary>
        private GameObject[] possibleTargets { get; set; }
        public NavMeshAgentWithObstacle NavMeshAgent { get; private set; }
        private Vector3 startingPosition;
        /// <summary>
        /// If defined, the default state of the enemy is the Patrol State. Following the defined path.
        /// </summary>
        public Transform[] patrolPath;
        /// <summary>
        /// PatrolState is persisted, to not loose the last visited path node.
        /// </summary>
        private PatrolState patrolState;
        public GunController gun;
        private EnemyHealthController _healthController;
        /// <summary>
        /// The current state.
        /// </summary>
        private IState state;

        /// <summary>
        /// How faraway allies are alerted when the player is detected.
        /// </summary>
        public float alertRadius = 50;
        /// <summary>
        /// When was the last time the enemy alerted others or has been alerted. This is used to prevent it from spamming alerts.
        /// </summary>
        private float timeOfLastAlert;
        /// <summary>
        /// Temp variable to store target that the enemy has been informed about.
        /// </summary>
        private GameObject informedTarget;

        
        /// <summary>
        /// Initializes fixed fields.
        /// </summary>
        private void Awake()
        {
            timeOfLastAlert = -5;
            NavMeshAgent = GetComponent<NavMeshAgentWithObstacle>();
            possibleTargets = GameObject.FindGameObjectsWithTag("Player");
            _healthController = GetComponent<EnemyHealthController>();
        }

        /// <summary>
        /// Registers event listener and the default state.
        /// </summary>
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

        
        /// <summary>
        /// Calls update on the current state, and sets the new one if changed.
        /// </summary>
        void Update()
        {
            var newState = state.Update();
            SetState(newState);
        }

        /// <summary>
        /// If the state changed, sets the state value and calls start.
        /// </summary>
        /// <param name="state">the new or current state</param>
        private void SetState(IState state)
        {
            if (state != this.state)
            {
                this.state = state;
                this.state.Start();
            }
        }

        /// <summary>
        /// Convenience function to fire the gun. 
        /// </summary>
        public void Fire()
        {
            gun.Fire();
        }

        /// <summary>
        /// Checks whether the defined target is within the line of sight.
        /// </summary>
        /// <param name="target"></param>
        /// <returns>True if the target is visible, false otherwise.</returns>
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

        /// <summary>
        /// Rotates towards the defined rotation.
        /// </summary>
        /// <param name="rotation"></param>
        public void RotateTowards(Quaternion rotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

        /// <summary>
        /// Checks is currently any target is visible. Only considers targets within the list possibleTargets.
        /// If the enemy has been informed about a target location, this target is returned instead.
        /// </summary>
        /// <returns>The first visible target or null if none are visible.</returns>
        public GameObject CheckForTargetInSight()
        {
            if (informedTarget is not null)
            {
                var newTarget = informedTarget;
                informedTarget = null;
                return newTarget;
            }
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

        /// <summary>
        /// Gets the default State.
        /// This is the patrol state if a path is defined.
        /// Otherwise it returns to its starting location and waits there.
        /// </summary>
        /// <returns>the default state</returns>
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

        /// <summary>
        /// Informs all nearby allies of the detected target.
        /// </summary>
        /// <param name="target"></param>
        public void InformNearbyAllies(GameObject target)
        {
            if (timeOfLastAlert + 5 > Time.time)
            {
                return;
            }
            timeOfLastAlert = Time.time;
            Collider[] results = new Collider[20];
            var numberOrResults = Physics.OverlapSphereNonAlloc(transform.position, alertRadius, results, LayerMask.GetMask("MovingEntities"));
            for (int i = 0; i < numberOrResults; i++)
            {
                var result = results[i];
                if (result.CompareTag("Enemy"))
                {
                    result.GetComponent<EnemyAIController>()?.InformAboutTarget(target);
                }
            }
        }

        public void InformAboutTarget(GameObject target)
        {
            timeOfLastAlert = Time.time;
            informedTarget = target;
        }
    }
}