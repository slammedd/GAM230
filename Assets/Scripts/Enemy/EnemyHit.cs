using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [HideInInspector] public bool damaged;

    private Weapon weaponScript; 
    private bool canRun = true;

    private void Start()
    {
        weaponScript = GameObject.Find("Gun").GetComponent<Weapon>();
    }
    private void Update()
    {
        if (damaged && canRun)
        {
            StartCoroutine(OnEnemyHit());
        }
    }

    IEnumerator OnEnemyHit()
    {
        canRun = false;
        GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(.25f);
        GetComponent<Renderer>().material.color = Color.white;
        canRun = true;
        damaged = false;
    }
}
