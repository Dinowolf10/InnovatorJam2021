using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // Declare variables
    public GameObject gameManager;

    public Transform playerTransform;

    public Rigidbody2D rb;

    public Animator animator;

    public SpriteRenderer spriteRenderer;

    public ParticleSystem trail;

    public Transform trailTransform;

    public int direction;

    public bool movingRight;

    public float speed;

    public float jumpForce;

    public float forwardWallForce;

    public float verticalWallForce;

    public float superJumpForwardForce;

    public float superJumpVerticalForce;

    public bool superJump;

    [SerializeField]
    private bool isGrounded;

    [SerializeField]
    private bool isOnWall;

    // Start is called before the first frame update
    private void Start()
    {


        // Starts with the player moving right
        direction = 1;
        movingRight = true;
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
        Debug.Log(isGrounded);
    }

    private void FixedUpdate()
    {
        // Moves the player
        Move();

        // Checks for what direction the player if facing to update the sprite
        CheckDirection();
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
    /// Changes direction the player is facing based on their movement direction
    /// </summary>
    private void CheckDirection()
    {
        // If moving to the right, have sprite face right and set the location of the particle system
        if (movingRight)
        {
            spriteRenderer.flipX = false;

            trailTransform.localPosition = new Vector3(-0.25f, -0.21f, 0);
            trailTransform.localRotation = Quaternion.Euler(0, -90f, 90f);

        }
        // If moving to the left, have sprite face left and set the location of the particle system
        else
        {
            spriteRenderer.flipX = true;

            trailTransform.localPosition = new Vector3(0.25f, -0.21f, 0);
            trailTransform.localRotation = Quaternion.Euler(0, 90f, 90f);
        }

        // If the player is moving up and on a wall
        if (rb.velocity.y > 0 && isOnWall)
        {
            // Set the particle position to the right side of the player
            if (movingRight)
            {
                trailTransform.localPosition = new Vector3(0.08f, -0.25f, 0);
            }
            // Set the particle position to the left side of the player
            else
            {
                trailTransform.localPosition = new Vector3(-0.06f, -0.25f, 0);
            }

            // Set the rotation and gravity scale of the particle system
            // Particles spawn downwards
            trailTransform.localRotation = Quaternion.Euler(90f, -90, 90f);
            SetParticleGravity(0);
        }
        // If the player is moving up and not on a wall
        else if (rb.velocity.y > 0)
        {
            // Set the particle gravity to 1
            SetParticleGravity(1);
        }
        // If the palyer is falling and is not grounded or on a wall
        else if (rb.velocity.y < 0 && !isGrounded && !isOnWall)
        {
            // Set the particle gravity to 1
            SetParticleGravity(1);
        }
        // If the player is falling and is not grounded
        else if (rb.velocity.y < 0 && !isGrounded)
        {
            // Set the particle position to the right side of the player
            if (movingRight)
            {
                trailTransform.localPosition = new Vector3(0.08f, 0.25f, 0);
            }
            // Set the particle position to the left side of the player
            else
            {
                trailTransform.localPosition = new Vector3(-0.06f, 0.25f, 0);
            }

            // Set the rotation and gravity scale of the particle system
            // Particles spawn upwards
            trailTransform.localRotation = Quaternion.Euler(-90f, 0, 90f);
            SetParticleGravity(0);
        }
        // If the player is grounded set the particle system gravity to 0
        else if (isGrounded)
        {
            SetParticleGravity(0);
        }
    }

    /// <summary>
    /// Adds force to the player on the Y axis to simulate a jump
    /// </summary>
    private void Jump()
    {
        // Adds a force to the player's rigidbody on the Y axis using the set jumpForce
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

        // Activate the particle system and set the gravity to 1
        SetParticleGravity(1);
        trail.gameObject.SetActive(true);

        // Set player gravity scale to 1.5
        rb.gravityScale = 1.5f;

        // If the player jumps while running into a wall
        if (isOnWall)
        {
            // Stop the particle system
            trail.Stop();

            // Hide the particle system
            trail.gameObject.SetActive(false);

            // Start a coroutine that waits 0.5 seconds until activating the particle system again
            StartCoroutine(HideParticleSystem());
        }
    }

    /// <summary>
    /// Switches the player direction and adds force to the x and y axis to simulate a wall jump
    /// </summary>
    private void WallJump()
    {
        // Sets the rigidbody velocity to 0
        rb.velocity = new Vector2(0, 0);

        // If player is currently moving right, change the direction to move left
        if (movingRight)
        {
            direction = -1;
            movingRight = false;
        }
        // If player is currently moving left, change the direction to move right
        else
        {
            direction = 1;
            movingRight = true;
        }

        // If player has a super jump
        if (superJump)
        {
            // Add force to the player using the player direction, superJumpForward, and superJumpVertical wall force
            rb.AddForce(new Vector2(direction * superJumpForwardForce, superJumpVerticalForce), ForceMode2D.Impulse);

            // Set superJump to false
            superJump = false;
        }
        else
        {
            // Add force to the player using the player direction, forward, and verticle wall force
            rb.AddForce(new Vector2(direction * forwardWallForce, verticalWallForce), ForceMode2D.Impulse);
        }

        // Activate the particle system and set the gravity to 1
        SetParticleGravity(1);
        trail.gameObject.SetActive(true);
    }

    /// <summary>
    /// Sets the particle system gravity
    /// </summary>
    /// <param name="num"></param>
    private void SetParticleGravity(float num)
    {
        var trailSettings = trail.main;
        trailSettings.gravityModifier = num;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If a game object tagged with ground enters the player collision
        // and the player is not on a wall, set the direction and isGrounded to true
        if (collision.gameObject.tag == "Ground")
        {
            // Set animator jumping bool to false
            animator.SetBool("IsJumping", false);

            // Set isGrounded to true
            isGrounded = true;

            // Activate the particle system and set the gravity to 0
            SetParticleGravity(0);
            trail.gameObject.SetActive(true);

            if (!isOnWall)
            {
                // If player is movingRight, set direction to 1
                if (movingRight)
                {
                    direction = 1;
                }
                // Otherwise set direction to -1
                else
                {
                    direction = -1;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // If a game object tagged with ground exits the player collision, set isGrounded to false
        if (collision.gameObject.tag == "Ground")
        {
            // Set isGrounded to false
            isGrounded = false;

            // Set IsJumping animator bool to true
            animator.SetBool("IsJumping", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If a game object tagged with wall enters the player collision
        // and the player is not on a wall
        if (collision.gameObject.tag == "Wall" && !isOnWall)
        {
            // Set the rigidbody velocity to 0
            rb.velocity = new Vector2(0, 0);

            // Set isOnWall to true
            isOnWall = true;

            // Set the direction to 0
            direction = 0;

            // Set the gravity scale to 0.5
            rb.gravityScale = 0.5f;

            // If the player is not grounded and runs into a wall
            if (!isGrounded)
            {
                // Stop the particle system
                trail.Stop();

                // Hide the particle system
                trail.gameObject.SetActive(false);

                // Start a coroutine that waits 0.5 seconds until activating the particle system again
                StartCoroutine(HideParticleSystem());
            }
        }

        // If this player touches oil, destroy this player
        if (collision.gameObject.tag == "Oil")
        {
            Destroy(this.gameObject);
        }

        // If player hits a super jump power-up, set super jump to true and destroy the super jump object
        if (collision.gameObject.tag == "SuperJump")
        {
            superJump = true;

            Destroy(collision.gameObject);
        }
    }

    /// <summary>
    /// Waits 0.5 seconds before activating the particle system
    /// </summary>
    /// <returns></returns>
    private IEnumerator HideParticleSystem()
    {
        // Wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        // Unhide and activate the particle system
        trail.gameObject.SetActive(true);

        // Start the particle system
        trail.Play();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If a game object tagged with wall exits the player collision
        // and the player is on a wall
        if (collision.gameObject.tag == "Wall" && isOnWall)
        {
            // Set isOnWall to false
            isOnWall = false;

            // If the player is moving right, set the direction to 1
            if (movingRight)
            {
                direction = 1;
            }
            // Otherwise set direction to -1
            else
            {
                direction = -1;
            }

            // Reset the gravity scale to 1.5
            rb.gravityScale = 1.5f;
        }
    }
}
