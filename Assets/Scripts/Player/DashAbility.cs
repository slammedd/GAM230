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
            uiManager.timerSlider.maxValue = uiManager.roomTimer * 4;
            uiManager.roomTimer = uiManager.roomTimer * 4;
            uiManager.actualTimer = uiManager.roomTimer;
            Destroy(gameObject);
        }
    }
}
