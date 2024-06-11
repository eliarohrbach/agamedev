using System.Collections.Generic;
using UnityEngine;

namespace SceneController
{
    /// <summary>
    /// Author: Alexander Wyss
    /// </summary>
    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool SharedInstance;
        public GameObject bulletPrefab;
        public int amountToPoolBullet = 20;
        private List<GameObject> pooledBulletObjects;

        void Awake()
        {
            SharedInstance = this;
        }

        /// <summary>
        /// Instantiate pooled bullets.
        /// </summary>
        void Start()
        {
            pooledBulletObjects = new List<GameObject>(amountToPoolBullet);
            InitPool(bulletPrefab, amountToPoolBullet, pooledBulletObjects);
        }

        /// <summary>
        /// Get Bullet from the object pool, if no are available a new one is created.
        /// </summary>
        /// <param name="position">The bullets position</param>
        /// <param name="rotation">The bullets rotation</param>
        /// <returns>the bullet game object</returns>
        public GameObject InstantiateBullet(Vector3 position, Quaternion rotation)
        {
            var bullet = GetPooledObject(pooledBulletObjects);
            if (bullet is null)
            {
                bullet = InitObject(bulletPrefab, pooledBulletObjects);
            }

            pooledBulletObjects.Remove(bullet);
            bullet.transform.position = position;
            bullet.transform.rotation = rotation;
            bullet.SetActive(true);
            return bullet;
        }
        
        /// <summary>
        /// Deactivates the bullet and adds it back to the pool
        /// </summary>
        /// <param name="bullet"></param>
        public void DestroyBullet(GameObject bullet)
        {
            bullet.SetActive(false);
            pooledBulletObjects.Add(bullet);
        }

        private static void InitPool(GameObject prefab, int amountToPool, ICollection<GameObject> poolList)
        {
            for (int i = 0; i < amountToPool; i++)
            {
                InitObject(prefab, poolList);
            }
        }

        private static GameObject InitObject(GameObject prefab, ICollection<GameObject> poolList)
        {
            var obj = Instantiate(prefab);
            obj.SetActive(false);
            poolList.Add(obj);
            return obj;
        }

        private static GameObject GetPooledObject(List<GameObject> poolList)
        {
            return poolList.Count > 0 ? poolList[0] : null;
        }
    }
}