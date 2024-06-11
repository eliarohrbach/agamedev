using UnityEngine;

namespace Enemy.ai
{
    /// <summary>
    /// Author: Alexander Wyss
    /// Walks to the defined last known location, attacking any target it can see. If the target destination is reached it changes to the search state.
    /// </summary>
    public class ChaseState : IState
    {
        private EnemyAIController enemyController;
        private readonly Vector3 lastKnownPosition;
        private bool navEnded = false;

        public ChaseState(EnemyAIController enemyController, Vector3 lastKnownPosition)
        {
            this.enemyController = enemyController;
            this.lastKnownPosition = lastKnownPosition;
        }

        public void Start()
        {
            enemyController.NavMeshAgent.SetDestination(lastKnownPosition);
            enemyController.NavMeshAgent.OnNavEnded += OnNavEnded;
            navEnded = false;
        }

        public IState Update()
        {
            var possibleTarget = enemyController.CheckForTargetInSight();
            if (possibleTarget is not null)
            {
                enemyController.NavMeshAgent.OnNavEnded -= OnNavEnded;
                enemyController.NavMeshAgent.IsStopped = true;
                return new AttackState(enemyController, possibleTarget);
            }

            if (navEnded)
            {
                enemyController.NavMeshAgent.IsStopped = true;
                return new SearchState(enemyController);
            }

            return this;
        }

        private void OnNavEnded()
        {
            navEnded = true;
            enemyController.NavMeshAgent.OnNavEnded -= OnNavEnded;
        }
    }
}