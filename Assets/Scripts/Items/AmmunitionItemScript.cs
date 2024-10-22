using UnityEngine;

// written by Severin Landolt
// added Audio Source capabilities by Elia Rohrbach

namespace Items
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(AudioSource))]

    /// <summary>
    /// Class <c>AmmunitionItemScript</c> controls the behaviour of the ammunition item
    /// </summary>
    public class AmmunitionItemScript : MonoBehaviour
    {
        [SerializeField] PistoleMagazin pistoleMagazin;
        [SerializeField] GameObject bulletCounter;
        private AudioSource audioSource;

        private int ammunition;

        public int maxRange;
        public int minRange;

        void Start()
        {
            // Check if the UI Bullet Counter has been assigned. If it is NULL, assign it via script
            if (bulletCounter == null)
            {
                bulletCounter = GameObject.Find("PistoleBulletCount");
            }

            // Adds a random number of bullets between minRange and maxRange to the Ammunition Item
            // minRange and maxRange are included in the Range
            ammunition = Random.Range(minRange, (maxRange + 1));

            // Get the AudioSource component
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            // The ammunition Item rotates around its own axis
            transform.Rotate(Vector3.right * 50 * Time.deltaTime, Space.World);
        }

        // Trigger that only responds to the player
        private void OnTriggerEnter(Collider other)
        {
            // Checks if it's the player and if there is still space for bullets in the pistol magazine
            if (other.CompareTag("Player") && pistoleMagazin.bulletCount < pistoleMagazin.maxBullet)
            {
                // Checks if the additional ammunition exceeds the magazine capacity
                if (pistoleMagazin.bulletCount + ammunition > pistoleMagazin.maxBullet)
                {
                    // Sets the ammunition counter to the maximum
                    pistoleMagazin.bulletCount = pistoleMagazin.maxBullet;
                    bulletCounter.GetComponent<BulletCounterTextScript>().changeCounter();
                }
                else
                {
                    // Adds the picked-up amount of ammunition to the pistol magazine
                    pistoleMagazin.bulletCount += ammunition;
                    bulletCounter.GetComponent<BulletCounterTextScript>().changeCounter();
                }

                // Play the collection sound effect
                if (audioSource != null && audioSource.clip != null)
                {
                    audioSource.Play();
                }

                // Destroy the ammunition object after the sound has played
                Destroy(gameObject, audioSource.clip.length);
            }
        }
    }
}
