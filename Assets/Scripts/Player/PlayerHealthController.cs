using System;
using System.Collections;
using Gun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    [RequireComponent(typeof(Collider), typeof(CharacterController))]
    public class PlayerHealthController : MonoBehaviour, IDamageable
    {
        public bool invulnerable = false;
        private bool _isDead;

        private void OnEnable()
        {
            _isDead = false;
        }

        public event Action OnDeath = delegate { };

        public void ApplyDamage()
        {
            if (!invulnerable && !_isDead)
            {
                _isDead = true;
                OnDeath.Invoke();
                StartCoroutine(DeathAnimation());
            }
        }

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