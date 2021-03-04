using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrab : MonoBehaviour
{
    public float grabForce;

    private PlayerControllerRigidbody playerController;
    private Rigidbody rb;
    private bool hasGrabbed;

    private void Start()
    {
        rb = GameObject.Find("Player").GetComponent<Rigidbody>();
        playerController = GameObject.Find("Player").GetComponent<PlayerControllerRigidbody>();
    }

    private void Update()
    {
        if (playerController.isGrounded)
        {
            hasGrabbed = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown("space") && !hasGrabbed)
        {
            Debug.Log("Ledge Grab");
            rb.AddRelativeForce(new Vector3(0, grabForce, 0), ForceMode.Impulse);
            hasGrabbed = true;
        }

    }
}
