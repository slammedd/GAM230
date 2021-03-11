﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRigidbody : MonoBehaviour
{
    public float health;
    public float movementSpeed;
    public float maxSpeed;
    public float sensitivity;
    public Camera playerCamera;
    public float minFOV;
    public float maxFOV;
    public float FOVChangeTime;
    public float FOVReturnTime;
    public Transform groundCheck;
    [HideInInspector] public bool isGrounded;
    public LayerMask ground;
    public float jumpForce;
    public float coyoteTime;
    public float jumpBufferLength;
    public float dashForce;
    [HideInInspector] public bool hasDashed;
    public float slideForce;
    public float slideTime;
    public float slideJumpMultiplier;
    public AudioSource source;
    public AudioClip jumpSound;
    public AudioClip slideSound;
    public AudioClip dashSound;
    [HideInInspector] public bool canMove;

    Rigidbody rb;
    private float yValue;
    private float checkRadius = 0.2f;
    private float coyoteCounter;
    private float jumpBufferCount;
    CapsuleCollider playerCollider;
    float colliderHeight;   
    private float slideHeightDecrease = 1f;
    private bool isSliding;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        colliderHeight = playerCollider.height;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        canMove = true;
    }

    private void Update()
    {
        MouseLook();
        Jump();
        Dash();
        Slide();
        CameraFOV();
        VelocityCap();
        health = Mathf.Clamp(health, 0, 100);
        Health();
    }
   
    private void FixedUpdate()
    {
        PlayerMovement();
    }

    void MouseLook()
    {
        if (canMove)
        {
            Vector2 mouseDirection = new Vector2(Input.GetAxis("Mouse X") * sensitivity, Input.GetAxis("Mouse Y") * sensitivity);

            yValue -= mouseDirection.y;
            yValue = Mathf.Clamp(yValue, -75, 75);

            playerCamera.transform.localEulerAngles = Vector3.right * yValue;
            transform.Rotate(Vector3.up * mouseDirection.x);
        }
    }

    void PlayerMovement()
    {
        if (canMove)
        {
            float x = Input.GetAxis("Horizontal") * movementSpeed;
            float z = Input.GetAxis("Vertical") * movementSpeed;

            Vector3 Movement = new Vector3(x, 0, z);
            Vector3 newPos = rb.position + rb.transform.TransformDirection(Movement);
            rb.MovePosition(newPos);
        }    
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, checkRadius, ground);

        if (isGrounded)
        {
            hasDashed = false;
        }

        if (Input.GetKeyDown("space"))
        {
            jumpBufferCount = jumpBufferLength;
        }

        else
        {
            jumpBufferCount -= Time.deltaTime;
        }

        if (jumpBufferCount >= 0 && coyoteCounter > 0 && !isSliding)
        {
            //Debug.Log("Jump");
            rb.velocity += new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            source.PlayOneShot(jumpSound);
            jumpBufferCount = 0;
        }

        if (isGrounded)
        {
            coyoteCounter = coyoteTime;
        }

        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        if (Input.GetKeyUp("space"))
        {
            coyoteCounter = -1;
        }          
    }

    void Dash()
    {
        float z = Input.GetAxis("Vertical") * movementSpeed;
        Vector3 dashDirection = transform.forward * z;
        
        if (Input.GetKeyDown("left shift") && !hasDashed && !isGrounded)
        {
            //Debug.Log("Dash");
            rb.AddRelativeForce(new Vector3(0 , 5 , dashForce), ForceMode.VelocityChange);
            source.PlayOneShot(dashSound);
            hasDashed = true;
        }        
    }

    void Slide()
    {
        if (Input.GetKeyDown("left ctrl") && isGrounded && !isSliding)
        {
            //Debug.Log("Slide");
            StartCoroutine(SlideMovement());
            source.PlayOneShot(slideSound);
        }

        if (Input.GetKeyDown("space") && isSliding)
        {
            //Debug.Log("Slide Jump");
            playerCollider.height = colliderHeight;
            rb.velocity += new Vector3(rb.velocity.x, jumpForce * slideJumpMultiplier, rb.velocity.z);
        }
    }

    IEnumerator SlideMovement()
    {       
        isSliding = true;
        playerCollider.height = slideHeightDecrease;
        rb.AddRelativeForce(new Vector3(0, 0, slideForce), ForceMode.VelocityChange);
        
        yield return new WaitForSeconds(slideTime);
        playerCollider.height = colliderHeight;
        isSliding = false;
    }

    void CameraFOV()
    {
        if(rb.velocity.magnitude > 8)
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, maxFOV, FOVChangeTime);
        }

        else if (rb.velocity.magnitude < 7)
        {            
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, minFOV, FOVReturnTime);
        }
    }

    void VelocityCap()
    {
        if(rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }

    void Health()
    {
        if(health <= 0)
        {
            Debug.Log("Player Died");
        }
    }

}