﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    public float range;
    public float impactForce;
    public float fireRate;
    public float maxSwayX;
    public float maxSwayY;
    public float swaySmoothing;
    public Camera playerCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Animator weaponAnimatior;
    public AudioSource source;
    public AudioClip fireSound;
    public AudioClip impactSound;
    public AudioClip turretImpactSound;

    private float fireCooldown = 0;
    private Vector3 originPosition;
    private PlayerControllerRigidbody playerController;

    private void Start()
    {
        originPosition = transform.localPosition;
        playerController = GameObject.Find("Player").GetComponent<PlayerControllerRigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= fireCooldown && playerController.canMove)
        {
            fireCooldown = Time.time + 1 / fireRate;
            weaponAnimatior.SetTrigger("Trigger");
            Shot();
            source.PlayOneShot(fireSound);
        }

        WeaponSway();
    }

    void Shot()
    {
        muzzleFlash.Play();
        RaycastHit hit;        

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            source.PlayOneShot(impactSound);
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            Drone drone = hit.transform.GetComponent<Drone>();

            if (hit.transform.tag == "Enemy")
            {
                Debug.Log("Hit");
                source.PlayOneShot(turretImpactSound);
                hit.collider.gameObject.GetComponent<Enemy>().damaged = true;
                enemy.Damaged(damage);
            }

            if (hit.transform.tag == "Drone")
            {
                Debug.Log("Hit");
                source.PlayOneShot(turretImpactSound);
                drone.Damaged(damage);
            }

            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 2);

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }      
    } 

    void WeaponSway()
    {
        float movementX = -Input.GetAxis("Mouse X");
        float movementY = Input.GetAxis("Mouse Y");
        movementX = Mathf.Clamp(movementX, -maxSwayX, maxSwayX);
        movementY = Mathf.Clamp(movementY, -maxSwayY, maxSwayY);

        Vector3 finalPosition = new Vector3(movementX, movementY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + originPosition, Time.deltaTime * swaySmoothing);
    }
}
