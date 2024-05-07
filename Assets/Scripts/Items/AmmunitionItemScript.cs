using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Collider))]
    public class AmmunitionItemScript : MonoBehaviour
    {
        [SerializeField] PistoleMagazin pistoleMagazin;
        [SerializeField] GameObject bulletCounter;

        private int ammunition;

        public int maxRange;
        public int minRange;

        // Start is called before the first frame update
        void Start()
        {
            if (bulletCounter == null)
            {
                bulletCounter = GameObject.Find("PistoleBulletCount");
            }
            ammunition = Random.Range(minRange, (maxRange + 1));
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.right * 50 * Time.deltaTime, Space.World);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && pistoleMagazin.bulletCount < pistoleMagazin.maxBullet)
            {
                if (pistoleMagazin.bulletCount + ammunition > 9)
                {
                    pistoleMagazin.bulletCount = 9;
                    bulletCounter.GetComponent<BulletCounterTextScript>().changeCounter();
                    Destroy(gameObject);
                } else
                {
                    pistoleMagazin.bulletCount += ammunition;
                    bulletCounter.GetComponent<BulletCounterTextScript>().changeCounter();
                    Destroy(gameObject);
                }
            }
        }
    }
}
