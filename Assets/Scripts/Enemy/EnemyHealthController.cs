using System;
using Gun;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Collider))]
    public class EnemyHealthController : MonoBehaviour, IDamageable
    {

        private ParticleSystem deathEffect;
        private GameObject whitebox;
        private AudioSource deathSound;
        private EnemyAIController enemyAIController;
        private bool isDead = false;
        
        public event Action OnDeath = delegate { };

        private void Awake()
        {
            deathEffect = transform.Find("DeathEffect")?.GetComponent<ParticleSystem>();
            whitebox = transform.Find("Whitebox")?.gameObject;
            deathSound = GetComponent<AudioSource>();
        }

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
                
                if (whitebox != null)
                    Destroy(whitebox);

                if (deathEffect && !deathEffect.isPlaying)
                    deathEffect.Play();

                if (deathSound != null)
                    deathSound.Play();

                GetComponent<Collider>().enabled = false;

                float delay = deathEffect ? deathEffect.main.duration : 0f;
                Destroy(gameObject, delay);
            }
        }
    }
}