using Gun;
using UnityEngine;

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