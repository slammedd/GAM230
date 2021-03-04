using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    public float bulletSpeed;
    Rigidbody rb;
    public GameObject explosionParticleSystem;
    private PlayerControllerRigidbody playerController;
    public float damageAmount;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = (transform.forward * bulletSpeed);        
        StartCoroutine(DestroyBullet());
        playerController = GameObject.Find("Player").GetComponent<PlayerControllerRigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            explosionParticleSystem.SetActive(true);
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            playerController.health -= damageAmount;
        }
    }

    IEnumerator DestroyBullet()
    {
        explosionParticleSystem.SetActive(true);
        yield return new WaitForSeconds(1f);
        explosionParticleSystem.SetActive(false);
        yield return new WaitForSeconds(1f);
        explosionParticleSystem.SetActive(true);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);       
    }  
}
