using Gun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    [RequireComponent(typeof(Collider))]
    public class PlayerHealthController : MonoBehaviour, IDamageable
    {
        public void ApplyDamage()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}