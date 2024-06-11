using UnityEngine;

namespace Enemy.ai
{
    /// <summary>
    /// Author: Alexander Wyss
    /// Follows the defined patrol points in a loop, attacking any target it can see.
    /// This is a default state.
    /// </summary>
    public class PatrolState : IState
    {
        private EnemyAIController enemyController;
        private readonly Transform[] path;
        private int _currentPatrolIndex = 0;

        public PatrolState(EnemyAIController enemyController, Transform[] path)
        {
            this.enemyController = enemyController;
            this.path = path;
        }

        public void Start()
        {
            enemyController.NavMeshAgent.OnNavEnded += OnNavEnded;
            enemyController.NavMeshAgent.SetDestination(path[_currentPatrolIndex].position);
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

            return this;
        }
        
         
        private void OnNavEnded()
        {
            _currentPatrolIndex++;
            if (_currentPatrolIndex >= path.Length)
            {
                _currentPatrolIndex = 0;
            }
            enemyController.NavMeshAgent.SetDestination(path[_currentPatrolIndex].position);
        }
    }
}