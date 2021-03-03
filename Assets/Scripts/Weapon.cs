using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    public float range;
    public float impactForce;
    public float fireRate;
    public float maxSway;
    public float swaySmoothing;
    public Camera playerCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Animator weaponAnimatior;
    public AudioSource source;
    public AudioClip fireSound;
    public AudioClip impactSound;

    private float fireCooldown = 0;
    private Vector3 originPosition;

    private void Start()
    {
        originPosition = transform.localPosition;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= fireCooldown)
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

    void WeaponSway()
    {
        float movementX = -Input.GetAxis("Mouse X");
        float movementY = Input.GetAxis("Mouse Y");
        movementX = Mathf.Clamp(movementX, -maxSway, maxSway);
        movementY = Mathf.Clamp(movementY, -maxSway, maxSway);

        Vector3 finalPosition = new Vector3(movementX, movementY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + originPosition, Time.deltaTime * swaySmoothing);
    }
}
