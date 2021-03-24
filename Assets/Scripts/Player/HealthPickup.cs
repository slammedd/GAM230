using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public PlayerControllerRigidbody playerController;
    public int healthToAdd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.health += healthToAdd;
            Destroy(gameObject);
        }
    }
}
