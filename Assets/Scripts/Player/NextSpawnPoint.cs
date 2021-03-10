using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSpawnPoint : MonoBehaviour
{
    private SpawnManager spawnManager;

    private void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spawnManager.spawnCounter++;
            gameObject.SetActive(false);
        }
    }
}
