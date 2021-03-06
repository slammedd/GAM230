﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSpawnPoint : MonoBehaviour
{
    public Collider doorCollider;
    public int roomCompleteScore;

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
            uiManager.score += roomCompleteScore;
            gameObject.SetActive(false);
            doorCollider.enabled = false;
        }
    }
}

