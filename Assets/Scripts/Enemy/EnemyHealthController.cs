using Gun;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Collider), typeof(EnemyAIController))]
    public class EnemyHealthController : MonoBehaviour, IDamageable
    {

        private ParticleSystem deathEffect;
        private GameObject whitebox;
        private AudioSource deathSound;
        private EnemyAIController enemyAIController;

        private void Awake()
        {
            deathEffect = transform.Find("DeathEffect")?.GetComponent<ParticleSystem>();
            enemyAIController = GetComponent<EnemyAIController>();
            whitebox = transform.Find("Whitebox")?.gameObject;
            deathSound = GetComponent<AudioSource>();
        }

        public void ApplyDamage()
        {
            enemyAIController.enabled = false;
            
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