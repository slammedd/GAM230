using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashRefil : MonoBehaviour
{
    public PlayerControllerRigidbody playerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Dash Refilled");
            playerController.hasDashed = false;
            Destroy(this.gameObject);
        }
    }
}
