using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public LayerMask acid;
    public Transform[] spawnPoints;

    private float checkRadius = 0.2f;
    private bool kill;
    private Transform acidCheck;
    private Animator screenWipeAnimator;
    private int spawnCounter = 0;

    private void Start()
    {
        acidCheck = GameObject.Find("Player").GetComponent<PlayerControllerRigidbody>().groundCheck;
        screenWipeAnimator = GameObject.Find("Screen Wipe").GetComponent<Animator>();

        GameObject.Find("Player").transform.position = spawnPoints[spawnCounter].position;
        GameObject.Find("Player").transform.rotation = spawnPoints[spawnCounter].rotation;
               
       screenWipeAnimator.SetBool("Trigger", true);        
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
    
    IEnumerator ScreenWipe()
    {
        screenWipeAnimator.SetBool("Trigger", false);
        yield return new WaitForSeconds(0.5f);       
        GameObject.Find("Player").transform.position = spawnPoints[spawnCounter].position;
        GameObject.Find("Player").transform.rotation = spawnPoints[spawnCounter].rotation;
        screenWipeAnimator.SetBool("Trigger", true);
    }

}
