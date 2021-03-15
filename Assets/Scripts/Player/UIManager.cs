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
    public Slider timerSlider;
    public Text dashText;
    [HideInInspector] public int roomNumber;
    [HideInInspector] public int killNumber;
    public float roomTimer;
    [HideInInspector] public float actualTimer = 100f;

    private PlayerControllerRigidbody playerController;
    private NextSpawnPoint nextSpawn;
    private Animator dashAnimatior;
    private bool canAnimate = true;
    private SpawnManager spawnManager;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerControllerRigidbody>();
        dashAnimatior = GameObject.Find("Dash Text").GetComponent<Animator>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        roomNumber++;
        actualTimer = roomTimer;
    }

    private void Update()
    {
        healthText.text = playerController.health.ToString() + "%";

        room.text = roomNumber.ToString();

        kills.text = killNumber.ToString();

        actualTimer -= Time.deltaTime;

        if(actualTimer <= 0)
        {
            StartCoroutine(spawnManager.GetComponent<SpawnManager>().ScreenWipe());           
        }

        timerSlider.value = actualTimer;

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
