using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashRefil : MonoBehaviour
{
    public AudioSource source;
    public AudioClip dashRefilSound;

    private PlayerControllerRigidbody playerController;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerControllerRigidbody>();    
    }

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
