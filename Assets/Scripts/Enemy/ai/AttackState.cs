using UnityEngine;

namespace Enemy.ai
{
    /// <summary>
    /// Author: Alexander Wyss
    /// This state tries to fire upon a defined target. If this is not possible it starts chasing the target.
    /// </summary>
    public class AttackState : IState
    {
        private EnemyAIController enemyController;
        private readonly GameObject target;

        public AttackState(EnemyAIController enemyController, GameObject target)
        {
            this.enemyController = enemyController;
            this.target = target;
        }
        
        public void Start()
        {
            enemyController.InformNearbyAllies(target);
        }

        public IState Update()
        {
            if (enemyController.CanSee(target))
            {
                var targetLookRotation = GetTargetLookRotation(target.transform.position);
                enemyController.RotateTowards(targetLookRotation);
                if (Quaternion.Angle(enemyController.transform.rotation, targetLookRotation) < 1)
                {
                    enemyController.Fire();
                }

                return this;
            }

            return new ChaseState(enemyController, target.transform.position);
        }

        private Quaternion GetTargetLookRotation(Vector3 rotationTarget)
        {
            var direction = rotationTarget - enemyController.transform.position;
            direction.y = 0;
            return Quaternion.LookRotation(direction);
        }
    }
}