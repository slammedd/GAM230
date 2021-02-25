using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float damage;
    public float range;
    public float impactForce;
    public float fireRate; 
    public Camera playerCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Animator weaponAnimatior;
    private float fireCooldown = 0;

    private void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= fireCooldown)
        {
            fireCooldown = Time.time + 1 / fireRate;
            weaponAnimatior.SetTrigger("Trigger");
            Shot();
        }               
    }

    void Shot()
    {
        muzzleFlash.Play();
        RaycastHit hit;        

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();

            if (hit.transform.tag == "Enemy")
            {
                Debug.Log("Hit");
                enemy.Damaged(damage);
            }

            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 2);

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }

      
    } 
}
