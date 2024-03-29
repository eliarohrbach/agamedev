using Gun;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(InputManager))]
    public class PlayerGunController : MonoBehaviour
    {
        public GunController gun;
        private InputManager _inputManager;

        private void Awake()
        {
            _inputManager = GetComponent<InputManager>();
        }

        private void Update()
        {
            if (_inputManager.GetFireGun())
            {
                gun.Fire();
            }
        }
    }
}