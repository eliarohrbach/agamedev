using Gun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Enemy
{
    [RequireComponent(typeof(Collider))]
    public class EnemyHealthController : MonoBehaviour, IDamageable
    {
        public void ApplyDamage()
        {
           Destroy(gameObject);
        }
    }
}