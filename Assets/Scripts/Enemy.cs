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

    private Transform player;

    public float fireRate;
    private float fireCooldown = 0;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private bool canFire = true;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        FindPlayer();
    }

    public void Damaged(float damageAmount)
    {
        health -= damageAmount;
        
        if(health <= 0)
        {           
            Die();
        }
    }

    void Die()
    {
        canFire = false;
        deathParticleSystem.Play();
        GetComponent<Collider>().enabled = false;
        foreach(GameObject obj in attachedObjects)
        {
            obj.SetActive(false);
        }      
        Destroy(gameObject, 2f);
    }

    void FindPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if(distanceToPlayer <= range)
        {
            Vector3 direction = player.position - transform.position;
            Quaternion turretRotation = Quaternion.LookRotation(direction);
            Vector3 newRotation = Quaternion.Lerp(turretHead.rotation, turretRotation, Time.deltaTime * rotationSmoothing).eulerAngles;
            turretHead.rotation = Quaternion.Euler (0, newRotation.y, 0);
            
            if(Time.time >= fireCooldown && canFire)
            {
                Shoot();
            }
        }
    }
    
    void Shoot()
    {
        fireCooldown = Time.time + 1 / fireRate;
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
