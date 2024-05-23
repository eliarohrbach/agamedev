using UnityEngine;

namespace Enemy.ai
{
    public class WaitState : IState
    {
        private EnemyAIController enemyController;

        public WaitState(EnemyAIController enemyController)
        {
            this.enemyController = enemyController;
        }

        public void Start()
        {
        }

        public IState Update()
        {
            var possibleTarget = enemyController.CheckForTargetInSight();
            if (possibleTarget is not null)
            {
                return new AttackState(enemyController, possibleTarget);
            }

            return this;
        }
    }
}