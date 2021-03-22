using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysMatMarker : MonoBehaviour
{
    public PlayerControllerRigidbody playerController;

    private void Update()
    {
        if (playerController.physMatChanged && playerController.isGrounded)
        {
            GetComponent<Collider>().material = null;
        }
    }
}
    