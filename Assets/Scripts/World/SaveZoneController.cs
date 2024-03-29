using System;
using UnityEngine;

namespace World
{
    [RequireComponent(typeof(Collider))]
    public class SaveZoneController : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                gameObject.SetActive(false);
            }
        }
    }
}