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
    public float pushForce;
    public AudioSource source;
    public AudioClip explosionSound;

    private Vector3 startPoint;
    private UIManager uiManager;
    private bool canMove = true;
    private float actualHealth;
    private SpawnManager spawnManager;
    private Rigidbody playerRigidbody;

    private void Start()
    {
        startPoint = transform.position;
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        health = actualHealth;
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        playerRigidbody = player.GetComponent<Rigidbody>();
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

            Vector3 droneNormal = collision.contacts[0].normal;
            Vector3 movementDirection = Vector3.Reflect(playerRigidbody.velocity, droneNormal).normalized;
            playerRigidbody.AddForce(movementDirection * pushForce, ForceMode.Impulse);
            source.PlayOneShot(explosionSound);
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
