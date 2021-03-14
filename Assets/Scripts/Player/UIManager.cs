using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI room;
    public Text dashText;
    [HideInInspector] public int roomNumber;

    private PlayerControllerRigidbody playerController;
    private NextSpawnPoint nextSpawn;
    private Animator dashAnimatior;
    private bool canAnimate = true;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerControllerRigidbody>();
        dashAnimatior = GameObject.Find("Dash Text").GetComponent<Animator>();
        roomNumber++;
    }

    private void Update()
    {
        healthText.text = playerController.health.ToString() + "%";

        room.text = roomNumber.ToString();

        if (playerController.dashUnlocked && canAnimate)
        {
            StartCoroutine(DashTextAnimation());
            canAnimate = false;
        }
    }

    IEnumerator DashTextAnimation()
    {
        dashAnimatior.SetBool("Trigger", true);
        yield return new WaitForSeconds(3.25f);
        dashAnimatior.SetBool("Trigger", false);
    }
}
