using System;
using UnityEngine;

namespace Enemy
{
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
        }

        private void OnDisable()
        {
        }

        void Update()
        {
            animator.SetFloat("speed", _navMeshAgentWithObstacle.Velocity > 1 ? 1 : 0);
        }
    }
}