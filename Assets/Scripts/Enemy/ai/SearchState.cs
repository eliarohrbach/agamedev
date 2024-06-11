using UnityEngine;

namespace Enemy.ai
{
    enum SearchRotationState
    {
        RIGHT,
        RETURNING,
        LEFT,
        FINAL_RETURNING
    }

    /// <summary>
    /// Author: Alexander Wyss
    /// Looks right, returns to start rotation, looks left and returns to start rotation. Attacking any target on sight.
    /// If no target is found, returns to the default state.
    /// </summary>
    public class SearchState : IState
    {
        private EnemyAIController enemyController;
        private SearchRotationState _rotationState;
        private Quaternion _startingRotation;

        public SearchState(EnemyAIController enemyController)
        {
            this.enemyController = enemyController;
        }

        public void Start()
        {
            _rotationState = SearchRotationState.RIGHT;
            _startingRotation = enemyController.transform.rotation;
        }

        public IState Update()
        {
            var possibleTarget = enemyController.CheckForTargetInSight();
            if (possibleTarget is not null)
            {
                return new AttackState(enemyController, possibleTarget);
            }
            
            if (_rotationState == SearchRotationState.RIGHT)
            {
                var targetRotation = GetRotationByAngle(_startingRotation, 110f);
                enemyController.RotateTowards(targetRotation);
                if (Quaternion.Angle(enemyController.transform.rotation, targetRotation) < 1)
                {
                    _rotationState = SearchRotationState.RETURNING;
                }
            }
            else if (_rotationState == SearchRotationState.LEFT)
            {
                var targetRotation = GetRotationByAngle(_startingRotation, -110f);
                enemyController.RotateTowards(targetRotation);
                if (Quaternion.Angle(enemyController.transform.rotation, targetRotation) < 1)
                {
                    _rotationState = SearchRotationState.FINAL_RETURNING;
                }
            }
            else if (_rotationState is SearchRotationState.RETURNING or SearchRotationState.FINAL_RETURNING)
            {
                enemyController.RotateTowards(_startingRotation);
                if (Quaternion.Angle(enemyController.transform.rotation, _startingRotation) < 1)
                {
                    if (_rotationState == SearchRotationState.RETURNING)
                    {
                        _rotationState = SearchRotationState.LEFT;
                    }
                    else
                    {
                        return enemyController.DefaultState();
                    }
                }
            }

            return this;
        }

        private Quaternion GetRotationByAngle(Quaternion startingRotation, float angle)
        {
            var rotation = Quaternion.AngleAxis(angle, Vector3.up);
            return startingRotation * rotation;
        }
    }
}