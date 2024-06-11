using System;
using Gun;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

namespace Player
{
    /// <summary>
    /// Author: Alexander Wyss
    /// Responsible for translating the attack input into a gun shot.
    /// </summary>
    [RequireComponent(typeof(InputManager), typeof(PlayerHealthController))]
    public class PlayerGunController : MonoBehaviour
    {
        public GunController gun;
        private InputManager _inputManager;
        private bool _isPaused;
        private PlayerHealthController _healthController;

        private void Awake()
        {
            _inputManager = GetComponent<InputManager>();
            _healthController = GetComponent<PlayerHealthController>();

        }

        private void OnEnable()
        {
            _healthController.OnDeath += Pause;
        }


        private void OnDisable()
        {
            _healthController.OnDeath -= Pause;
            _isPaused = false;
        }

        private void Pause()
        {
            _isPaused = true;
        }

        /// <summary>
        /// Fires the weapon if the input is pressed.
        /// </summary>
        private void Update()
        {
            if (!_isPaused && _inputManager.GetFireGun())
            {
                gun.Fire();

            }
        }
    }
}