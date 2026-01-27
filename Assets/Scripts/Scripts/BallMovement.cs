using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new UnityEngine.Vector2(2f,3f);
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 normal = collision.contacts[0].normal;
    
        // Reflect velocity based on surface angle
        rb.velocity = Vector2.Reflect(rb.velocity, normal);
    }
}
