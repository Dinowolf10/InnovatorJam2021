using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Declare variables
    public Transform playerTransform;

    public Rigidbody2D rb;

    public int direction;

    public float speed;

    public float jumpForce;

    public float forwardWallForce;

    public float verticalWallForce;

    [SerializeField]
    private bool isGrounded;

    [SerializeField]
    private bool isOnWall;

    public GameObject levelManager;

    // Start is called before the first frame update
    private void Start()
    {
        // Starts with the player moving right
        direction = 1;
    }

    // Update is called once per frame
    private void Update()
    {
        // Checks for jump input on ground
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        // Checks for jump input on wall
        else if (Input.GetButtonDown("Jump") && isOnWall)
        {
            WallJump();
        }
    }

    private void FixedUpdate()
    {
        // Moves the player
        Move();
    }

    /// <summary>
    /// Moves the player in the current direction they are facing
    /// </summary>
    private void Move()
    {
        // Moves the player in the current direction they are facing multiplied by the speed and time passed
        // since the last FixedUpdate was called
        playerTransform.position += new Vector3(direction, 0, 0) * speed * Time.fixedDeltaTime;
    }

    /// <summary>
    /// Adds force to the player on the Y axis to simulate a jump
    /// </summary>
    private void Jump()
    {
        // Adds a force to the player's rigidbody on the Y axis using the set jumpForce
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    /// <summary>
    /// Switches the player direction and adds force to the x and y axis to simulate a wall jump
    /// </summary>
    private void WallJump()
    {
        // If player is currently moving right, change the direction to move left
        if (direction == 1)
        {
            direction = -1;
        }
        // If player is currently moving left, change the direction to move right
        else if (direction == -1)
        {
            direction = 1;
        }

        // Add force to the player using the player direction, forward, and verticle wall force
        rb.AddForce(new Vector2(direction * forwardWallForce, verticalWallForce), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If a game object tagged with ground enters the player collision, set isGrounded to true
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        // If a game object tagged with wall enters the player collision, set isOnWall to true
        else if (collision.gameObject.tag == "Wall")
        {
            isOnWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // If a game object tagged with ground exits the player collision, set isGrounded to false
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
        // If a game object tagged with wall exits the player collision, set isOnWall to false
        else if (collision.gameObject.tag == "Wall")
        {
            isOnWall = false;
        }
    }
}
