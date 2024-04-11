using System;
using System.Collections;
using Gun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    [RequireComponent(typeof(Collider), typeof(CharacterController))]
    public class PlayerHealthController : MonoBehaviour, IDamageable
    {
        public bool invulnerable = false;
        public event Action OnDeath = delegate { };

        public void ApplyDamage()
        {
            if (!invulnerable)
            {
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