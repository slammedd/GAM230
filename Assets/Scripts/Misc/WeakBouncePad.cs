using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakBouncePad : MonoBehaviour
{
    public AudioSource source;
    public AudioClip bounceSound;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            source.PlayOneShot(bounceSound);
        }
    }
}
