using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public LayerMask acid;
    public Transform[] spawnPoints;
    public GameObject playerCamera;
    public int spawnCounter = 0;
    [HideInInspector] public bool kill;
    [HideInInspector] public Animator screenWipeAnimator;
    [HideInInspector] public bool isRunning;

    private float checkRadius = 0.2f;
    private Transform acidCheck;
    private PlayerControllerRigidbody playerController;
    private UIManager uiManager;

    private void Start()
    {
        acidCheck = GameObject.Find("Player").GetComponent<PlayerControllerRigidbody>().groundCheck;
        screenWipeAnimator = GameObject.Find("Screen Wipe").GetComponent<Animator>();
        playerController = GameObject.Find("Player").GetComponent<PlayerControllerRigidbody>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        GameObject.Find("Player").transform.position = spawnPoints[spawnCounter].position;
        GameObject.Find("Player").transform.rotation = spawnPoints[spawnCounter].rotation;

        StartCoroutine(InitialScreenWipe());
    }

    private void Update()
    {
        Kill();
    }

    public void Kill()
    {
        kill = Physics.CheckSphere(acidCheck.position, checkRadius, acid);

        if (kill && !isRunning)
        {
            /*var changedPhysMat = FindObjectOfType<PhysMatMarker>();
            changedPhysMat.GetComponent<Collider>().enabled = false;
            changedPhysMat.GetComponent<Collider>().material = null;
            changedPhysMat.GetComponent<Collider>().enabled = true;*/
            StartCoroutine(ScreenWipe());
        }
    }

    IEnumerator InitialScreenWipe()
    {
        yield return new WaitForSeconds(0.5f);
        screenWipeAnimator.SetBool("Trigger", true);        
    }

    public IEnumerator ScreenWipe()
    {
        isRunning = true;
        playerController.canMove = false;
        screenWipeAnimator.SetBool("Trigger", false);
        yield return new WaitForSeconds(0.5f);
        GameObject.Find("Player").transform.position = spawnPoints[spawnCounter].position;
        GameObject.Find("Player").transform.rotation = spawnPoints[spawnCounter].rotation;
        screenWipeAnimator.SetBool("Trigger", true);
        playerController.health -= 10;
        uiManager.actualTimer = uiManager.roomTimer;
        yield return new WaitForSeconds(0.8f);
        playerController.canMove = true;
        isRunning = false;
    }
}
