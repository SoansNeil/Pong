using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    // Private fields
    private Rigidbody2D rb;
    private Vector2 direction;
    private float speed = 5f;

    // Public getters and setters
    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
        rb.velocity = direction * speed; // immediately update velocity
    }

    public Vector2 GetDirection()
    {
        return direction;
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
        rb.velocity = direction * speed; // immediately update velocity
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        rb.gravityScale = 0; // ensure gravity doesn't affect the ball

        // Initialize ball movement
        direction = new Vector2(2f, 3f).normalized; // initial direction
        speed = 5f;

        rb.velocity = direction * speed; // start moving
    }

    void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.contacts[0].normal;
        direction = Vector2.Reflect(direction, normal).normalized;

        rb.velocity = direction * speed;
    }
}
