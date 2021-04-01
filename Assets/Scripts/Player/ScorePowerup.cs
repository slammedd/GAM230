using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePowerup : MonoBehaviour
{
    public int scoreToAdd;
    public AudioClip pickupSound;

    private UIManager uiManager;
    private AudioSource source;

    private void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        source = GameObject.Find("Head").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.score += scoreToAdd;
            source.PlayOneShot(pickupSound);
            Destroy(gameObject);
        }
    }
}
