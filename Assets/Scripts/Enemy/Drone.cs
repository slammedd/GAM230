using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Drone : MonoBehaviour
{
    public GameObject player;
    public float movementSpeed;
    public int killScoreIncrease;
    public ParticleSystem deathParticleSystem;
    public ParticleSystem impactParticleSystem;
    public GameObject[] attachedObjects;
    public float health;

    private Vector3 startPoint;
    private UIManager uiManager;
    private bool canMove = true;
    private float actualHealth;
    private SpawnManager spawnManager;

    private void Start()
    {
        startPoint = transform.position;
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        health = actualHealth;
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    private void Update()
    {
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed);       
        }

        transform.LookAt(player.transform.position);

        if (spawnManager.kill)
        {
            Respawn();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Die();
            impactParticleSystem.Play();
        }
    }
    public void Damaged(float damageAmount)
    {

        actualHealth -= damageAmount;

        if (actualHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        uiManager.killNumber++;
        uiManager.score += killScoreIncrease;
        canMove = false;
        deathParticleSystem.Play();
        GetComponent<Collider>().enabled = false;
        foreach(GameObject gO in attachedObjects)
        {
            gO.SetActive(false);
        }
    }

    void Respawn()
    {
        actualHealth = health;
        canMove = true;
        GetComponent<Collider>().enabled = true;
        transform.position = startPoint;
        foreach (GameObject obj in attachedObjects)
        {
            obj.SetActive(true);
        }
    }
}
