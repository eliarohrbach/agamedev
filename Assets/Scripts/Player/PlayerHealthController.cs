using Gun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    [RequireComponent(typeof(Collider))]
    public class PlayerHealthController : MonoBehaviour, IDamageable
    {
        public bool invulnerable = false;

        public void ApplyDamage()
        {
            if (!invulnerable)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}