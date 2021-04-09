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
    public float range;
    public float pushForce;
    public int damageAmount;
    public AudioSource source;
    public AudioClip explosionSound;
    public AudioClip detectedSound;
    public SphereCollider detectionCollider;

    private Vector3 startPoint;
    private Quaternion startRotation;
    private UIManager uiManager;
    private bool canMove = true;
    private float actualHealth;
    private SpawnManager spawnManager;
    private Rigidbody playerRigidbody;

    private void Start()
    {
        startPoint = transform.position;
        startRotation = transform.rotation;
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        health = actualHealth;
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        playerRigidbody = player.GetComponent<Rigidbody>();
        detectionCollider.radius = range;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);


        if (canMove && distanceToPlayer <= range)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed);       
            transform.LookAt(player.transform.position);
        }

        if (spawnManager.kill)
        {
            Respawn();
        }

        if(uiManager.canPause == false)
        {
            canMove = false;
        }

        else
        {
            canMove = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Die();
            impactParticleSystem.Play();
            player.GetComponent<PlayerControllerRigidbody>().health -= damageAmount;
            Vector3 droneNormal = collision.contacts[0].normal;
            Vector3 movementDirection = Vector3.Reflect(playerRigidbody.velocity, droneNormal).normalized;
            playerRigidbody.AddForce(movementDirection * pushForce, ForceMode.Impulse);
            source.PlayOneShot(explosionSound);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        source.PlayOneShot(detectedSound);    
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
        transform.rotation = startRotation;
        foreach (GameObject obj in attachedObjects)
        {
            obj.SetActive(true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
