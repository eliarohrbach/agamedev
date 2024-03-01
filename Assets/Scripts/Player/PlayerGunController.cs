using Gun;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(InputManager))]
    public class PlayerGunController : MonoBehaviour
    {
        public GameObject gun;
        private GunController _gunController;
        private InputManager _inputManager;

        void Start()
        {
            _inputManager = GetComponent<InputManager>();
            _gunController = gun.GetComponent<GunController>();
        }

        void Update()
        {
            if (_inputManager.GetFireGun())
            {
                _gunController.Fire();
            }
        }
    }
}