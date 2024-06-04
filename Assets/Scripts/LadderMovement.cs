using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private bool isLadder;
    private bool isClimbing;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float climbingSpeed = 4f;

    void Update()
    {
        // Check if the player is on a ladder and the up arrow key is pressed
        if (isLadder && Input.GetKeyDown(KeyCode.UpArrow))
        {
            isClimbing = true;
        }
        else
        {
            isClimbing = false;
        }
    }

    void FixedUpdate()
    {
        if (isClimbing)
        {
            // Calculate vertical movement for climbing
            float verticalInput = Input.GetAxisRaw("Vertical");
            float verticalVelocity = verticalInput * climbingSpeed;

            // Apply the vertical velocity to move the player
            rb.velocity = new Vector2(rb.velocity.x, verticalVelocity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false; // Stop climbing when leaving the ladder
        }
    }
}
