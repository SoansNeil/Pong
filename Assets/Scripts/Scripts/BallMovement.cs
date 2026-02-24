using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class BallMovement : NetworkBehaviour, ICollidable
{
    // Private fields
    private Rigidbody2D rb;
    private Vector2 direction;
    private float speed = 5f;
    private SpriteRenderer sr;
    private bool gameStopped = false;

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
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        rb.gravityScale = 0; // ensure gravity doesn't affect the ball

        // Initialize ball movement
        direction = Vector2.zero; // initial direction
        speed = 5f;

        rb.velocity = Vector2.zero; // start moving
    }

    void FixedUpdate()
{
    if (!IsServer) return; // Only the server should control the ball's movement
    if (gameStopped) return;

    rb.velocity = direction * speed;
}

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.contacts[0].normal;
        direction = Vector2.Reflect(direction, normal).normalized;

        ICollidable collidable = collision.gameObject.GetComponent<ICollidable>();
        if (collidable != null)        {
            collidable.OnHit(collision);
        }
    }
    public void OnHit(Collision2D collision)
    {
        speed += 0.5f; // increase speed on hit
        StartCoroutine(Flash());

    }
     IEnumerator Flash()
    {
    sr.color = Color.green;
    yield return new WaitForSeconds(0.1f);
    sr.color = Color.white;
    }
    public void ResetBall()
{
    rb.velocity = Vector2.zero;
    transform.position = Vector2.zero;

    float xDirection = Random.value < 0.5f ? -1f : 1f;

    float yDirection = Random.Range(-0.75f, 0.75f);

    direction = new Vector2(xDirection, yDirection).normalized;

    rb.velocity = direction * speed;
}
    public void StopBall()
{
    if (!IsServer) return;
    gameStopped = true;
    rb.velocity = Vector2.zero;
}
}
