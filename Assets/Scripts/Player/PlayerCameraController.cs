using UnityEngine;

namespace Player
{
    /// <summary>
    /// Author: Alexander Wyss
    /// Responsible for translation the camera input into the correct rotation.
    /// </summary>
    [RequireComponent(typeof(InputManager), typeof(PlayerHealthController))]
    public class PlayerCameraController : MonoBehaviour
    {
        private InputManager _inputManager;
        public new Camera camera;
        /// <summary>
        /// How fast the camera moves.
        /// </summary>
        public float sensitivity = 7;
        /// <summary>
        /// How far the player can look up and down.
        /// </summary>
        [Range(0f, 90f)] public float yRotationLimit = 88f;
        private float _yRotation;
        private PlayerHealthController _healthController;
        private bool _isPaused;

        private void Awake()
        {
            _inputManager = GetComponent<InputManager>();
            _healthController = GetComponent<PlayerHealthController>();
        }

        /// <summary>
        /// Hides the cursor and locks it. Registers events
        /// </summary>
        private void OnEnable()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            _yRotation = 0;
            _healthController.OnDeath += Pause;
        }

        /// <summary>
        /// Releases the cursor. Unregisters events.
        /// </summary>
        private void OnDisable()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            _healthController.OnDeath -= Pause;
            _isPaused = false;
        }

        /// <summary>
        /// Translates the camera input into the correct rotation. 
        /// </summary>
        private void Update()
        {
            if (!_isPaused)
            {
                var cameraInput = _inputManager.GetCamera();
                _yRotation += cameraInput.y * sensitivity;
                _yRotation = Mathf.Clamp(_yRotation, -yRotationLimit, yRotationLimit);
                camera.transform.localRotation = Quaternion.AngleAxis(_yRotation, Vector3.left);

                var xRotation = cameraInput.x * sensitivity;
                transform.rotation *= Quaternion.AngleAxis(xRotation, Vector3.up);
            }
        }

        private void Pause()
        {
            _isPaused = true;
        }
    }
}