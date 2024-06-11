using System;
using UnityEngine;

namespace World
{
    /// <summary>
    /// Author: Alexander Wyss
    /// Provides a save zone for the player, at the start of a level.
    /// As soon the player leaves, the save zone is destroyed.
    /// </summary>
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