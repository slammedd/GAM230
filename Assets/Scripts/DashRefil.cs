using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashRefil : MonoBehaviour
{
    public PlayerControllerRigidbody playerController;
    public AudioSource source;
    public AudioClip dashRefilSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Dash Refilled");
            source.PlayOneShot(dashRefilSound);
            playerController.hasDashed = false;
            Destroy(gameObject);
        }
    }
}
