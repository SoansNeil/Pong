using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class PaddleMovement : NetworkBehaviour, ICollidable
{
    private float speed = 5.0f;
    private NetworkVariable<float> yPos = new NetworkVariable<float>(0f,NetworkVariableReadPermission.Everyone,
    NetworkVariableWritePermission.Owner);
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    if (IsOwner)
    {
        float vert = GetMovementInput();
        transform.position += new Vector3(0, vert * speed * Time.deltaTime, 0);
        yPos.Value = transform.position.y;
    }
    else
    {
        // Non-owners follow the synced value
        transform.position = new Vector3(transform.position.x, yPos.Value, 0);
    }
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
public override void OnNetworkSpawn()
{
        if (OwnerClientId == 0)
            transform.position = new Vector3(-14, 0, 0);
        else
            transform.position = new Vector3(14, 0, 0);
}
}
