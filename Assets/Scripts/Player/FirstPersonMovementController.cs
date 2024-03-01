using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController), typeof(InputManager))]
    public class FirstPersonMovementController : MonoBehaviour
    {
        private CharacterController _characterController;
        private InputManager _inputManager;
        public float speed = 8;
        public float jumpStrength = 20;
        public float gravity = 0.25f;
        private float _yVelocity;

        void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _inputManager = GetComponent<InputManager>();
        }

        private void OnDisable()
        {
            _yVelocity = 0;
        }

        void Update()
        {
            var forward = transform.forward * _inputManager.GetMovementVertical();
            var right = transform.right * _inputManager.GetMovementHorizontal();
            var groundDirection = (forward + right).normalized * speed;
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
                _yVelocity -= gravity;
            }

            _characterController.Move((groundDirection + _yVelocity * Vector3.up) * Time.unscaledDeltaTime);
        }
    }
}