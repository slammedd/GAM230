using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI room;
    public float roomTimer;
    public TextMeshProUGUI kills;
    public Slider timerSlider;
    public TextMeshProUGUI diedText;
    public GameObject retryButton;
    public TextMeshProUGUI scoreText;
    public ScorePowerup scorePowerup;
    public TextMeshProUGUI dashText;
    [HideInInspector] public int roomNumber;
    [HideInInspector] public int killNumber;
    [HideInInspector] public float actualTimer = 100f;
    [HideInInspector] public float score;

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

        scoreText.text = score.ToString();
        score = Mathf.Clamp(score, 0, 9999);

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

        if(playerController.health <= 0)
        {
            Retry();
        }
    }

    void Retry()
    {
        playerController.canMove = false;
        spawnManager.screenWipeAnimator.SetBool("Trigger", false);
        diedText.gameObject.SetActive(true);
        retryButton.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("Levels");
        Debug.Log("reload");
    }

    IEnumerator DashTextAnimation()
    {
        dashAnimatior.SetBool("Trigger", true);
        yield return new WaitForSeconds(3.25f);
        dashAnimatior.SetBool("Trigger", false);
    }
}
