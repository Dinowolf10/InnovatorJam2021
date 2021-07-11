using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    public Transform enemyTransform;

    public SpriteRenderer spriteRenderer;

    public int direction;

    public float speed;

    public float maxLeftDistance;

    public float maxRightDistance;

    public bool movingRight;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();

        CheckDirection();

        CheckMaxDistance();
    }

    /// <summary>
    /// Moves the enemy in the current direction they are facing
    /// </summary>
    private void Move()
    {
        // Move enemy in current direction they are facing multiplied by the speed and the time that
        // has passed since the last FixedUpdate call
        enemyTransform.position += new Vector3(direction, 0, 0) * speed * Time.fixedDeltaTime;
    }

    /// <summary>
    /// Changes direction the enemy is facing based on their movement direction
    /// </summary>
    private void CheckDirection()
    {
        // If moving to the right, have sprite face right
        if (direction == 1)
        {
            spriteRenderer.flipX = false;
        }
        // If moving to the left, have sprite face left
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    /// <summary>
    /// Checks to see if the enemy has moved the max distance to the right or left
    /// </summary>
    private void CheckMaxDistance()
    {
        // If the enemy is past the max right distance and the enemy is moving right,
        // change the direction to left and set movingRight to false
        if (enemyTransform.position.x >= maxRightDistance && movingRight)
        {
            direction = -1;
            movingRight = false;
        }
        // If the enemy is past the max left distance and the enemy is not moving right,
        // change the direction to right and set movingRight to true
        else if (enemyTransform.position.x <= maxLeftDistance && !movingRight)
        {
            direction = 1;
            movingRight = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player collides with this enemy, destroy the player and this enemy
        if(collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
