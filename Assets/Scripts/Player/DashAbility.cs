using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    public AudioSource source;
    public AudioClip unlockSound;

    private PlayerControllerRigidbody playerController;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerControllerRigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            source.PlayOneShot(unlockSound);
            playerController.dashUnlocked = true;
            Destroy(gameObject);
        }
    }
}
