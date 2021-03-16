using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePowerup : MonoBehaviour
{
    public int scoreToAdd;

    private UIManager uiManager;

    private void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.score += scoreToAdd;
            Destroy(gameObject);
        }
    }
}
