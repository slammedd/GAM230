using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headbob : MonoBehaviour
{
    public Animation anim;
    private bool isWalking;
    private bool left;
    private bool right;  

    private void Start()
    {
        left = true;
        right = false;
    }

    private void Update()
    {
        CameraAnimation();
    }
    void CameraAnimation()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if(x != 0 || z !=0 )
        {
            isWalking = true;
        }

        else if(x == 0 && z == 0)
        {
            isWalking = false;
            anim.Stop();
        }

        var playerController = GetComponentInParent<PlayerControllerRigidbody>();

        if (playerController.isGrounded && isWalking && !anim.isPlaying)
        {
            if (left)
            {
                anim.Play("WalkLeft");
                left = false;
                right = true;
            }

            if (right)
            {
                anim.Play("WalkRight");
                right = false;
                left = true;
            }
        }
    }

}
