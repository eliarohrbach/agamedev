using Gun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    [RequireComponent(typeof(Collider))]
    public class PlayerHealthController : MonoBehaviour, IShootable
    {
        public void Shot()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}