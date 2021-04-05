using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    public AudioSource source;
    public AudioClip unlockSound;

    private PlayerControllerRigidbody playerController;
    private UIManager uiManager;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerControllerRigidbody>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            source.PlayOneShot(unlockSound);
            playerController.dashUnlocked = true;
            uiManager.timerSlider.maxValue = uiManager.roomTimer * 2;
            uiManager.roomTimer = uiManager.roomTimer * 2;
            uiManager.actualTimer = uiManager.roomTimer;
            Destroy(gameObject);
        }
    }
}
