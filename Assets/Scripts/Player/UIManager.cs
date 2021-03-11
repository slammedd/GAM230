using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text healthText;
    public Text dashText;

    private PlayerControllerRigidbody playerController;
    private Animator dashAnimatior;
    private bool canAnimate = true;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerControllerRigidbody>();
        dashAnimatior = GameObject.Find("Dash Text").GetComponent<Animator>();
    }

    private void Update()
    {
        healthText.text = playerController.health.ToString() + "%";

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
