using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float sensitivity;

    private float yValue;

    public Transform playerCamera;

    private CharacterController controller;

    public float movementSpeed;

    Vector3 velocity;

    public float gravity;

    public Transform groundCheck;

    private float checkRadius = 0.2f;

    public LayerMask ground;

    private bool isGrounded;

    public float jumpForce;

    public float dashSpeed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = this.gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        MouseLook();
        PlayerMovement();
        Gravity();
        Dash();
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
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * x + transform.forward * z;

        controller.Move(movement * movementSpeed * Time.deltaTime);

      
    }

    void Gravity()
    {
        Jump();

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, checkRadius, ground);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    void Jump()
    {
        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
        }
    }

    void Dash()
    {

       
    }
}
