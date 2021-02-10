using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRigidbody : MonoBehaviour
{

    public float movementSpeed;

    Rigidbody rb;

    public float sensitivity;

    private float yValue;

    public Transform playerCamera;

    public Transform groundCheck;

    private bool isGrounded;

    private float checkRadius = 0.2f;

    public LayerMask ground;

    public float jumpForce;

    public float dashForce;

    public bool hasDashed;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    private void Update()
    {
        MouseLook();
        Jump();
        Dash();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    void MouseLook()
    {
        Vector2 mouseDirection = new Vector2(Input.GetAxis("Mouse X") * sensitivity, Input.GetAxis("Mouse Y") * sensitivity);

        yValue -= mouseDirection.y;
        yValue = Mathf.Clamp(yValue, -75, 75);

        playerCamera.localEulerAngles = Vector3.right * yValue;
        transform.Rotate(Vector3.up * mouseDirection.x);
    }

    void PlayerMovement()
    {
        float x = Input.GetAxis("Horizontal") * movementSpeed;
        float z = Input.GetAxis("Vertical") * movementSpeed;

        Vector3 movementDirection = transform.right * x + transform.forward * z;
        Vector3 movement = new Vector3(movementDirection.x, rb.velocity.y, movementDirection.z);
        rb.velocity = movement;
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, checkRadius, ground);

       

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("Jump");
            rb.velocity += new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);            
        }
            
    }

    void Dash()
    {
        float z = Input.GetAxis("Vertical") * movementSpeed;
        Vector3 dashDirection = transform.forward * z;
        const int dashMultiplier = 1000;

        if (Input.GetButtonDown("Dash") && !hasDashed)
        {
            Debug.Log("Dash");
            rb.AddRelativeForce(new Vector3(0 , 0 , dashMultiplier * dashForce));          
        }
        
    }
}
