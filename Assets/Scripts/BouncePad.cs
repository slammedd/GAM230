﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public float bounceForce;
    public AudioSource source;
    public AudioClip bounceSound;

    private Vector3 movementDirection;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 wallNormal = collision.contacts[0].normal;
            movementDirection = Vector3.Reflect(playerRigidbody.velocity, wallNormal).normalized;
            playerRigidbody.AddForce(movementDirection * bounceForce, ForceMode.Impulse);

            source.PlayOneShot(bounceSound);
            
        }
    }
}