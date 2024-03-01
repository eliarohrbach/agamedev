using UnityEngine;

namespace Player
{
    public class InputManager : MonoBehaviour
    {
        public float GetMovementVertical()
        {
            return Input.GetAxisRaw("Vertical");
        }

        public float GetMovementHorizontal()
        {
            return Input.GetAxisRaw("Horizontal");
        }

        public float GetCameraX()
        {
            return Input.GetAxis("Mouse X");
        }

        public float GetCameraY()
        {
            return Input.GetAxis("Mouse Y");
        }

        public bool GetJump()
        {
            return Input.GetButton("Jump");
        }
    }
}