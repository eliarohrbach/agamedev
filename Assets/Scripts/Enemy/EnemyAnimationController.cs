using System;
using UnityEngine;

namespace Enemy
{
    /// <summary>
    /// Author: Alexander Wyss
    /// The EnemyAnimationController is responsible for setting the correct variables in the animator.
    /// </summary>
    [RequireComponent(typeof(EnemyAIController), typeof(NavMeshAgentWithObstacle))]
    public class EnemyAnimationController : MonoBehaviour
    {
        public Animator animator;
        private EnemyAIController _enemyAIController;
        private NavMeshAgentWithObstacle _navMeshAgentWithObstacle;

        void Awake()
        {
            _enemyAIController = GetComponent<EnemyAIController>();
            _navMeshAgentWithObstacle = GetComponent<NavMeshAgentWithObstacle>();
        }

        private void OnEnable()
        {
            _enemyAIController.gun.OnFire += TriggerShoot;
        }

        private void OnDisable()
        {
            _enemyAIController.gun.OnFire -= TriggerShoot;
        }

        void Update()
        {
            animator.SetFloat("speed", _navMeshAgentWithObstacle.Velocity > 1 ? 1 : 0);
        }

        private void TriggerShoot()
        {
         animator.SetTrigger("fire");   
        }
    }
}