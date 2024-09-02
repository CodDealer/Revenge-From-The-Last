using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    public float moveSpeed = 5f;       // Normal movement speed
    public float sprintMultiplier = 2f; // Multiplier for sprinting speed
    public float jumpForce = 5f;        // Force applied when jumping
    public float gravity = 9.81f;       // Gravity force

    private bool isGrounded;            // Checks if the player is on the ground
    private Rigidbody rb;               // Rigidbody component of the GameObject

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input from the WASD keys
        float horizontal = Input.GetAxis("Horizontal"); // A, D or Left Arrow, Right Arrow
        float vertical = Input.GetAxis("Vertical");     // W, S or Up Arrow, Down Arrow

        // Calculate movement direction
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        // Check if the sprint key (Left Shift) is pressed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveDirection *= sprintMultiplier; // Apply sprint multiplier
        }

        // Move the object
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        // Check if the jump key (Space) is pressed and the player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Apply jump force
        }

        // Apply gravity manually
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        }
    }

    // Check if the player is grounded
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // When the player leaves the ground
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
