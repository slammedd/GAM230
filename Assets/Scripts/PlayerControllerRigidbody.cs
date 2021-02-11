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

    [HideInInspector] public bool hasDashed;

    public float slideForce;

    public float slideTime;

    CapsuleCollider playerCollider;

    float colliderHeight;

    public float slideHeightDecrease;

    private bool isSliding;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        colliderHeight = playerCollider.height;
        Cursor.lockState = CursorLockMode.Locked;        
    }

    private void Update()
    {
        MouseLook();
        Jump();
        Dash();
        Slide();
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

        //Vector3 movementDirection = transform.right * x + transform.forward * z;
        //Vector3 movement = new Vector3(movementDirection.x, rb.velocity.y, movementDirection.z);
        //rb.velocity = movement;

        Vector3 nMovement = new Vector3(x, 0, z);
        Vector3 newPos = rb.position + rb.transform.TransformDirection(nMovement);
        rb.MovePosition(newPos);
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, checkRadius, ground);

        if (isGrounded)
        {
            hasDashed = false;
        }

        if (Input.GetKeyDown("space") && isGrounded)
        {
            Debug.Log("Jump");
            rb.velocity += new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);            
        }            
    }

    void Dash()
    {
        float z = Input.GetAxis("Vertical") * movementSpeed;
        Vector3 dashDirection = transform.forward * z;
        
        if (Input.GetKeyDown("left shift") && !hasDashed && !isGrounded)
        {
            Debug.Log("Dash");
            rb.AddRelativeForce(new Vector3(0 , 0 , dashForce), ForceMode.VelocityChange);            
            hasDashed = true;
        }
        
    }

    void Slide()
    {
        if (Input.GetKeyDown("left ctrl") && isGrounded && !isSliding)
        {
            Debug.Log("Slide");
            StartCoroutine(SlideMovement());
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

    
}
