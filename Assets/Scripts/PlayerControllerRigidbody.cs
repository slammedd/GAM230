using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRigidbody : MonoBehaviour
{

    public float movementSpeed;

    public float maxSpeed;

    Rigidbody rb;

    public float sensitivity;

    private float yValue;

    public Camera playerCamera;

    public float minFOV;

    public float maxFOV;

    public float FOVTime;

    public float FOVReturnTime;

    public Transform groundCheck;

    [HideInInspector] public bool isGrounded;

    private float checkRadius = 0.2f;

    public LayerMask ground;

    public float jumpForce;

    public float dashForce;

    [HideInInspector] public bool hasDashed;

    CapsuleCollider playerCollider;

    float colliderHeight;
   
    public float slideForce;

    public float slideTime;

    private float slideHeightDecrease = 1f;

    private bool isSliding;

    public float slideJumpMultiplier;

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
        CameraFOV();
        VelocityCap();
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

        playerCamera.transform.localEulerAngles = Vector3.right * yValue;
        transform.Rotate(Vector3.up * mouseDirection.x);
    }

    void PlayerMovement()
    {
        float x = Input.GetAxis("Horizontal") * movementSpeed;
        float z = Input.GetAxis("Vertical") * movementSpeed;
      
        Vector3 Movement = new Vector3(x, 0, z);
        Vector3 newPos = rb.position + rb.transform.TransformDirection(Movement);
        rb.MovePosition(newPos);   
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, checkRadius, ground);

        if (isGrounded)
        {
            hasDashed = false;
        }

        if (Input.GetKeyDown("space") && isGrounded && !isSliding)
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
            rb.AddRelativeForce(new Vector3(0 , 5 , dashForce), ForceMode.VelocityChange);            
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

        if (Input.GetKeyDown("space") && isSliding)
        {
            Debug.Log("Slide Jump");
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
        if(rb.velocity.magnitude > 10)
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, maxFOV, FOVTime);
        }

        else if (rb.velocity.magnitude < 9.5)
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

}
