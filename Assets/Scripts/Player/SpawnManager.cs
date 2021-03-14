using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public LayerMask acid;
    public Transform[] spawnPoints;
    public GameObject playerCamera;
    public int spawnCounter = 0;
    [HideInInspector]public bool kill;

    private float checkRadius = 0.2f;
    private Transform acidCheck;
    private Animator screenWipeAnimator;
    private PlayerControllerRigidbody playerController;

    private void Start()
    {
        acidCheck = GameObject.Find("Player").GetComponent<PlayerControllerRigidbody>().groundCheck;
        screenWipeAnimator = GameObject.Find("Screen Wipe").GetComponent<Animator>();
        playerController = GameObject.Find("Player").GetComponent<PlayerControllerRigidbody>();

        GameObject.Find("Player").transform.position = spawnPoints[spawnCounter].position;
        GameObject.Find("Player").transform.rotation = spawnPoints[spawnCounter].rotation;

        StartCoroutine(InitialScreenWipe());
    }

    private void Update()
    {
        Kill();
    }

    void Kill()
    {
        kill = Physics.CheckSphere(acidCheck.position, checkRadius, acid);

        if (kill)
        {
            StartCoroutine(ScreenWipe());
        }
    }
    
    IEnumerator InitialScreenWipe()
    {
        yield return new WaitForSeconds(0.5f);
        screenWipeAnimator.SetBool("Trigger", true);        
    }

    IEnumerator ScreenWipe()
    {
        playerController.canMove = false;   
        screenWipeAnimator.SetBool("Trigger", false);
        yield return new WaitForSeconds(0.5f);       
        GameObject.Find("Player").transform.position = spawnPoints[spawnCounter].position;
        GameObject.Find("Player").transform.rotation = spawnPoints[spawnCounter].rotation;
        screenWipeAnimator.SetBool("Trigger", true);
        yield return new WaitForSeconds(0.8f);
        playerController.canMove = true;
    }

}
