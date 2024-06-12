using System;
using Gun;
using UnityEngine;

namespace Enemy
{
    /// <summary>
    /// Author: Alexander Wyss
    /// Ammunition Author: Severin Landolt
    /// Sound Effect/Death Animation Author: Martin Hoeger, Alexander Wyss
    ///
    /// Implements the interface IDamageable. This is called if a bullet hits a collider.
    /// Destroys the enemy and initiates the death effect and ammunition drop.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class EnemyHealthController : MonoBehaviour, IDamageable
    {
        public GameObject deathEffect;
        public GameObject ammunition;
        private bool isDead = false;

        public event Action OnDeath = delegate { };

        // Add a reference to the EnemyManager
        public EnemyManager enemyManager;

        private void OnEnable()
        {
            isDead = false;
        }

        public void ApplyDamage()
        {
            if (!isDead)
            {
                isDead = true;
                OnDeath.Invoke();
                if (deathEffect is not null)
                {
                    Instantiate(deathEffect, transform.position, transform.rotation);
                    if (ammunition != null)
                    {
                        Instantiate(ammunition, transform.position, transform.rotation);
                    }
                }

                // Notify the EnemyManager that this enemy has been killed
                if (enemyManager != null)
                {
                    enemyManager.EnemyKilled();
                }

                Destroy(gameObject);
            }
        }
    }
}
