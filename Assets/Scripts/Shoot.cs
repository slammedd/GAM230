using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float damage;
    public float range;
    public Camera playerCamera;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shot();
        }
    }

    void Shot()
    {
        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {            
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            
            if(hit.transform.tag == "Enemy")
            {
                Debug.Log("Hit");
                enemy.Damaged(damage);
            }
        }

    }
}
