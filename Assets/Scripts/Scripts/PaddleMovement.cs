using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PaddleMovement : MonoBehaviour, ICollidable
{
    private float speed = 5.0f;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float vert = GetMovementInput();
        transform.position += new UnityEngine.Vector3(0,vert * speed * Time.deltaTime,0);
    }
    protected abstract float GetMovementInput();
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        ICollidable collidable = collision.gameObject.GetComponent<ICollidable>();
        if (collidable != null)        {
            collidable.OnHit(collision);
        }   
    }
    public void OnHit(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name + " hit the ball!");
        StartCoroutine(Flash());
    }
    IEnumerator Flash()
{
    sr.color = Color.red;
    yield return new WaitForSeconds(0.1f);
    sr.color = Color.white;
}
}
