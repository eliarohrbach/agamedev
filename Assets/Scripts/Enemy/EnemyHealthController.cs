using System;
using Gun;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Collider))]
    public class EnemyHealthController : MonoBehaviour, IDamageable
    {
        public GameObject deathEffect;
        public GameObject ammunition;
        private bool isDead = false;
        
        public event Action OnDeath = delegate { };


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
                Destroy(gameObject);
            }
        }
    }
}