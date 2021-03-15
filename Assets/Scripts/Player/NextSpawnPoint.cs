using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSpawnPoint : MonoBehaviour
{
    private SpawnManager spawnManager;
    private UIManager uiManager;

    private void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spawnManager.spawnCounter++;
            uiManager.roomNumber++;
            uiManager.actualTimer = uiManager.roomTimer;
            gameObject.SetActive(false);
        }
    }
}
