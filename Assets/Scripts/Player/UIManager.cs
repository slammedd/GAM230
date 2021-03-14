using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI room;
    public TextMeshProUGUI kills;
    public Slider movementSlider;
    public Text dashText;
    [HideInInspector] public int roomNumber;
    [HideInInspector] public int killNumber;

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

        kills.text = killNumber.ToString();

        movementSlider.value = playerController.movementTimer;

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
