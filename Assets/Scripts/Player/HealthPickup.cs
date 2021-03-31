using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public PlayerControllerRigidbody playerController;
    public int healthToAdd;
    public AudioClip healthPickupSound;

    private AudioSource source;

    private void Start()
    {
        source = GameObject.Find("Head").GetComponent<AudioSource>();   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController.health += healthToAdd;
            source.PlayOneShot(healthPickupSound);
            Destroy(gameObject);
        }
    }
}
