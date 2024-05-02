using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class InputManager : MonoBehaviour
    {
        public InputActionAsset actions;
        private InputActionMap actionMap;
        private InputAction moveInput;
        private InputAction cameraInput;
        private InputAction jump;
        private InputAction fire;
       

        public void Awake()
        {
            actionMap = actions.FindActionMap("input");
            moveInput = actionMap.FindAction("move");
            cameraInput = actionMap.FindAction("camera");
            jump = actionMap.FindAction("jump");
            fire = actionMap.FindAction("fire");
        }

        public void OnEnable()
        {
            actionMap.Enable();
        }

        public void OnDisable()
        {
          actionMap.Disable();
        }
        
        public Vector2 GetMovement()
        {
            return moveInput.ReadValue<Vector2>();
        }

        public Vector2 GetCamera()
        {
            return cameraInput.ReadValue<Vector2>();
        }

        public bool GetJump()
        {
            return jump.IsPressed();
        }

        public bool GetFireGun()
        {
            return fire.IsPressed();
        }
    }
}