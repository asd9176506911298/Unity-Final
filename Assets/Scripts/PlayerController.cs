using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;

    private Vector3 moveDirection = Vector3.zero;

    private CharacterController controller;

    [Header("Gravity")]
    [SerializeField] private float gravity;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private bool isCharacterGround = false;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        GetReferences();
        InitVariables();
    }

    private void Update()
    {

        HandleIsGrounded();
        HandleJumping();
        HandleGravity();


        HandleRunning();
        HandleMovement();
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(moveX, 0, moveZ);
        moveDirection = moveDirection.normalized;

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void HandleRunning()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = walkSpeed;
        }
    }

    private void HandleIsGrounded()
    {
        isCharacterGround = Physics.CheckSphere(transform.position, groundDistance, groundMask);
    }

    private void HandleGravity()
    {
        if (isCharacterGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isCharacterGround)
        {
            velocity.y += Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    private void GetReferences()
    {
        controller = GetComponent<CharacterController>();
    }

    private void InitVariables()
    {
        moveSpeed = walkSpeed;
    }
}
