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

    [SerializeField]
    private bool isGrounded;

    // Start is called before the first frame update
    private void Start()
    {
        // Starts with the player moving right
        direction = 1;
    }

    // Update is called once per frame
    private void Update()
    {
        // Checks for jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If a game object tagged with ground enters the player collision, set isGrounded to false
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // If a game object tagged with ground leaves the player collision, set isGrounded to false
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
