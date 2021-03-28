using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator doorAnimator;
    public AudioSource source;
    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            source.PlayOneShot(doorOpenSound);
            doorAnimator.SetBool("Trigger", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            source.PlayOneShot(doorCloseSound);
            doorAnimator.SetBool("Trigger", false);            
        }
    }
}
