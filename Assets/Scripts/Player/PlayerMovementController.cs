using System;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Author: Alexander Wyss
    /// Responsible for translation player input inta actual motion.
    /// </summary>
    [RequireComponent(typeof(CharacterController), typeof(InputManager), typeof(PlayerHealthController))]
    public class PlayerMovementController : MonoBehaviour
    {
        private CharacterController _characterController;
        private InputManager _inputManager;
        /// <summary>
        /// The players speed.
        /// </summary>
        public float speed = 8;
        
        /// <summary>
        /// The players jump height.
        /// </summary>
        public float jumpStrength = 20;
        /// <summary>
        /// How fast the player returns to ground.
        /// </summary>
        public float gravity = 0.25f;
        /// <summary>
        /// The acceleration increases the speed of the player, the longer he is actively moving. As soon he stops the acceleration is reset.
        /// </summary>
        public float acceleration = 0.5f;
        private float _currentAcceleration;
        public float CurrentAcceleration => _currentAcceleration;

        private float _yVelocity;
        private bool _isPaused;
        private PlayerHealthController _healthController;
        public float Velocity => _characterController.velocity.magnitude;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _inputManager = GetComponent<InputManager>();
            _healthController = GetComponent<PlayerHealthController>();
        }

        private void OnEnable()
        {
            _healthController.OnDeath += OnDeath;
        }

        private void OnDisable()
        {
            _yVelocity = 0;
            _healthController.OnDeath -= OnDeath;
            _isPaused = false;
        }

        /// <summary>
        /// Translates player input into movement.
        /// The player input horizontal movement is based on the movement input multiplied by the speed and current acceleration.
        /// The acceleration increases over time as long as the player is moving.
        /// If the player slows done enough the acceleration is set to zero.
        /// Vertical movement is based on jump and gravity.
        /// </summary>
        private void Update()
        {
            if (!_isPaused)
            {
                var movementInput = _inputManager.GetMovement();
                var forward = transform.forward * movementInput.y;
                var right = transform.right * movementInput.x;
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

        private void OnDeath()
        {
            _isPaused = true;
            _currentAcceleration = 0;
        }
    }
}