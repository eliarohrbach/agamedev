using Gun;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(InputManager))]
    public class PlayerGunController : MonoBehaviour
    {
        public GunController gun;
        private InputManager _inputManager;

        void Start()
        {
            _inputManager = GetComponent<InputManager>();
        }

        void Update()
        {
            if (_inputManager.GetFireGun())
            {
                gun.Fire();
            }
        }
    }
}