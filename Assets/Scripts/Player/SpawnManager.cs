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

        if(GameObject.Find("Player").transform.position == spawnPoints[0].position)
        {
            screenWipeAnimator.SetTrigger("Trigger");
        }
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
            GameObject.Find("Player").transform.position = spawnPoints[spawnCounter].position;
        }
    }

}
