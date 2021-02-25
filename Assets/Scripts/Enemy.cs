using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public ParticleSystem deathParticleSystem;

    public void Damaged(float damageAmount)
    {
        health -= damageAmount;

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        deathParticleSystem.Play();
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        Destroy(gameObject, 2f);
    }
}
