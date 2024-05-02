using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class InputManager : MonoBehaviour
    {
        public InputAction moveInput;
        public InputAction cameraInput;
        public InputAction jump;
        public InputAction fire;
        
        public void OnEnable()
        {
            moveInput.Enable();
            cameraInput.Enable();
            jump.Enable();
            fire.Enable();
        }

        public void OnDisable()
        {
            moveInput.Disable();
            cameraInput.Disable();
            jump.Disable();
            fire.Disable();
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