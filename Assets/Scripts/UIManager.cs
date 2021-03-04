using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text healthText;

    private PlayerControllerRigidbody playerController;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerControllerRigidbody>();
    }

    private void Update()
    {
        healthText.text = playerController.health.ToString() + "%";
    }
}
