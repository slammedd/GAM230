using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public ParticleSystem deathParticleSystem;
    public GameObject[] attachedObjects;
    public float range;
    public Transform turretHead;
    public float rotationSmoothing;
    public float fireRate;
    public Transform firePoint;
    public AudioSource source;
    public AudioClip shotSound;
    public int roomNumber;
    public int killScoreIncrease;

    private float actualHealth;
    private Transform player;
    private float fireCooldown = 0;
    public GameObject bulletPrefab;
    private bool canFire = true;
    private SpawnManager spawnManager;
    private bool inRoom;
    private UIManager uiManager;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        actualHealth = health;
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    private void Update()
    {
        FindPlayer();

        if (spawnManager.kill)
        {
            Respawn();
        }

        if(spawnManager.spawnCounter == roomNumber)
        {
            inRoom = true;
        }

        else
        {
            inRoom = false;
        }
        
    }

    public void Damaged(float damageAmount)
    {
        actualHealth -= damageAmount;
        
        if(actualHealth <= 0)
        {           
            Die();
        }
    }

    void Die()
    {
        uiManager.killNumber++;
        uiManager.score += killScoreIncrease;
        canFire = false;
        deathParticleSystem.Play();
        GetComponent<Collider>().enabled = false;
        foreach(GameObject obj in attachedObjects)
        {
            obj.SetActive(false);
        }      
    }

    void Respawn()
    {
        actualHealth = health;
        canFire = true;
        GetComponent<Collider>().enabled = true;
        foreach (GameObject obj in attachedObjects)
        {
            obj.SetActive(true);
        }
    }

    void FindPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if(distanceToPlayer <= range)
        {
            Vector3 direction = player.position - transform.position;
            Quaternion turretRotation = Quaternion.LookRotation(-direction);
            Vector3 newRotation = Quaternion.Lerp(turretHead.rotation, turretRotation, Time.deltaTime * rotationSmoothing).eulerAngles;
            turretHead.rotation = Quaternion.Euler (0, newRotation.y, 0);
            
            if(Time.time >= fireCooldown && canFire && inRoom)
            {
                Shoot();
            }
        }
    }
    
    void Shoot()
    {
        source.PlayOneShot(shotSound);
        fireCooldown = Time.time + 1 / fireRate;
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
