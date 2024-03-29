using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(InputManager))]
    public class PlayerCameraController : MonoBehaviour
    {
        private InputManager _inputManager;
        public new Camera camera;
        public float sensitivity = 7;
        [Range(0f, 90f)] public float yRotationLimit = 88f;
        private float _yRotation;

        private void Awake()
        {
            _inputManager = GetComponent<InputManager>();
        }

        private void OnEnable()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            _yRotation = 0;
        }

        private void OnDisable()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        private void Update()
        {
            _yRotation += _inputManager.GetCameraY() * sensitivity;
            _yRotation = Mathf.Clamp(_yRotation, -yRotationLimit, yRotationLimit);
            camera.transform.localRotation = Quaternion.AngleAxis(_yRotation, Vector3.left);

            var xRotation = _inputManager.GetCameraX() * sensitivity;
            transform.rotation *= Quaternion.AngleAxis(xRotation, Vector3.up);
        }
    }
}