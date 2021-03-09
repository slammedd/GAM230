using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject explosionParticleSystem;
    public float damageAmount;
    public AudioClip impactObject;
    public AudioClip impactPlayer;

    private AudioSource source;
    Rigidbody rb;
    private PlayerControllerRigidbody playerController;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = (transform.forward * bulletSpeed);        
        StartCoroutine(DestroyBullet());
        playerController = GameObject.Find("Player").GetComponent<PlayerControllerRigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        explosionParticleSystem.SetActive(true);
        source.PlayOneShot(impactObject);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        if (collision.gameObject.CompareTag("Player"))
        {
            explosionParticleSystem.SetActive(true);
            source.PlayOneShot(impactPlayer);
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
