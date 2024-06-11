using System;
using System.Collections;
using Enemy;
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
        /// <summary>
        /// How faraway enemies are alerted when the player fires his gun.
        /// </summary>
        public float alertRadius = 50;

        private void Awake()
        {
            _inputManager = GetComponent<InputManager>();
            _healthController = GetComponent<PlayerHealthController>();

        }

        private void OnEnable()
        {
            _healthController.OnDeath += Pause;
            gun.OnFire += AlertNearbyEnemies;
        }

   

        private void OnDisable()
        {
            _healthController.OnDeath -= Pause;
            _isPaused = false;
            gun.OnFire -= AlertNearbyEnemies;
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
        
        /// <summary>
        /// On a gun shot, all nearby enemies are alerted to the player.
        /// </summary>
        private void AlertNearbyEnemies()
        {
            Collider[] results = new Collider[20];
            var numberOrResults = Physics.OverlapSphereNonAlloc(transform.position, alertRadius, results, LayerMask.GetMask("MovingEntities"));
            for (int i = 0; i < numberOrResults; i++)
            {
                var result = results[i];
                if (result.CompareTag("Enemy"))
                {
                    result.GetComponent<EnemyAIController>()?.InformAboutTarget(gameObject);
                }
            }
        }
    }
}