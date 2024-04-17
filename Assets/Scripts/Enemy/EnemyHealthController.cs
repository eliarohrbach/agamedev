using Gun;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Collider))]
    public class EnemyHealthController : MonoBehaviour, IDamageable
    {

        private ParticleSystem deathEffect;
        private GameObject whitebox;
        private GameObject gun;
        private AudioSource deathSound;

        private void Awake()
        {
            deathEffect = transform.Find("DeathEffect")?.GetComponent<ParticleSystem>();
            whitebox = transform.Find("Whitebox")?.gameObject;
            gun = transform.Find("Gun")?.gameObject;
            deathSound = GetComponent<AudioSource>();
        }

        public void ApplyDamage()
        {
            if (whitebox != null)
                Destroy(whitebox);

            if (gun != null)
                Destroy(gun);

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