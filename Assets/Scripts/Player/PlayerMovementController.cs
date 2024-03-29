using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController), typeof(InputManager))]
    public class PlayerMovementController : MonoBehaviour
    {
        private CharacterController _characterController;
        private InputManager _inputManager;
        public float speed = 8;
        public float jumpStrength = 20;
        public float gravity = 0.25f;
        public float acceleration = 0.5f;
        private float _currentAcceleration;
        public float CurrentAcceleration => _currentAcceleration;

        private float _yVelocity;
        public float Velocity => _characterController.velocity.magnitude;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _inputManager = GetComponent<InputManager>();
        }

        private void OnDisable()
        {
            _yVelocity = 0;
        }

        private void Update()
        {
            var forward = transform.forward * _inputManager.GetMovementVertical();
            var right = transform.right * _inputManager.GetMovementHorizontal();
            var groundDirection = (forward + right).normalized * (speed + _currentAcceleration);
            if (_characterController.isGrounded)
            {
                if (_inputManager.GetJump())
                {
                    _yVelocity += jumpStrength;
                }
                else
                {
                    _yVelocity = 0;
                }
            }
            else
            {
                _yVelocity -= gravity * Time.unscaledDeltaTime;
            }

            var moveDirection = groundDirection + _yVelocity * Vector3.up;
            _characterController.Move(moveDirection * Time.unscaledDeltaTime);

            if (_characterController.velocity.magnitude > 1)
            {
                _currentAcceleration += acceleration * Time.unscaledDeltaTime;
            }
            else
            {
                _currentAcceleration = 0;
            }
        }
    }
}