using System;
using System.Collections;
using Gun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    /// <summary>
    /// Author: Alexander Wyss
    /// Responsible for the death animation and restarting the scene after death.
    /// Implements the interface IDamageable. This is called if a bullet hits a collider.
    /// </summary>
    [RequireComponent(typeof(Collider), typeof(CharacterController))]
    public class PlayerHealthController : MonoBehaviour, IDamageable
    {
        /// <summary>
        /// Make the player invulnerable. For testing purposes.
        /// </summary>
        public bool invulnerable = false;
        private bool _isDead;

        private void OnEnable()
        {
            _isDead = false;
        }

        /// <summary>
        /// Invoked once the player dies.
        /// </summary>
        public event Action OnDeath = delegate { };

        /// <summary>
        /// Invoke OnDeath and start the death animation.
        /// Ensure both is done only once.
        /// </summary>
        public void ApplyDamage()
        {
            if (!invulnerable && !_isDead)
            {
                _isDead = true;
                OnDeath.Invoke();
                StartCoroutine(DeathAnimation());
            }
        }

        /// <summary>
        ///  On Death we want the player to fall to the ground.
        /// We add a rigidbody and normal collider, and apply a small force to make the player tip over.
        /// The CharacterController must be disabled, as it interferes with the rigidbody.
        /// After a short time we reload the scene. 
        /// </summary>
        /// <returns></returns>
        private IEnumerator DeathAnimation()
        {
            var rigidbody = gameObject.AddComponent<Rigidbody>();
            var collider = gameObject.AddComponent<CapsuleCollider>();
            var characterController = GetComponent<CharacterController>();
            characterController.enabled = false;
            collider.radius = characterController.radius;
            collider.height = characterController.height;
            rigidbody.AddForce(transform.right * 10);
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}