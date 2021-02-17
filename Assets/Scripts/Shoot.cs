using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float damage;
    public float range;
    public Camera playerCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
            
            if(hit.transform.tag == "Enemy")
            {
                Debug.Log("Hit");
                enemy.Damaged(damage);
            }
            
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }

    }
}
