using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    private float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float vert = GetMovementInput();
        transform.position += new UnityEngine.Vector3(0,vert * speed * Time.deltaTime,0);
    }
    protected virtual float GetMovementInput()
    {
        return 0f;
    }
}
