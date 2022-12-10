using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private Vector3 movement;
    private Rigidbody rb;

    private void Start()
    {
        movement = Vector3.zero;
        rb = GetComponent<Rigidbody>();
    }

    [Header("Movement")]
    // Move
    [SerializeField] [Range(0.5f, 20)] private float walkSpeed;
    // Jump
    [SerializeField] [Range(0.5f, 10)] private float jumpHeight;
    // Ground check
    private bool isGrounded = true;
    private float groundCheckDistance = 1.2f;

    private void FixedUpdate()
    {
        Vector3 movement = transform.forward * verticalAxisValue + transform.right * horizontalAxisValue;

        rb.MovePosition(transform.position + movement * walkSpeed * Time.deltaTime);

        // Ground check
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), groundCheckDistance))
        {
            isGrounded = true;

        }
        else
        {
            isGrounded = false;
        }
    }


    private float verticalAxisValue;
    private float horizontalAxisValue;

    // Vertical Axis Move
    public void Move(float moveAxisChange)
    {
        verticalAxisValue = moveAxisChange;
    }

    // Horizontal Axis Move
    public void Strafe(float strafeAxisChange)
    {
        horizontalAxisValue = strafeAxisChange;
    }

    // Jump
    public void Jump()
    {
        if (isGrounded)
        {
            float jumpForce = Mathf.Sqrt(jumpHeight * -20 * Physics.gravity.y);
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }
}
