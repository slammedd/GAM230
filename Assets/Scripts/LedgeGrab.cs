using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrab : MonoBehaviour
{
    public Rigidbody rb;
    public PlayerControllerRigidbody playerController;
    public float grabForce;

    private bool hasGrabbed;

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
