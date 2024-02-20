using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerControl : MonoBehaviour
{
    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    public float speed = 10.0f;
    public float boundY = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var vel = rb.velocity;
        if(Input.GetKey(moveUp)){
            vel.y = speed;
        } else if(Input.GetKey(moveDown)){
            vel.y = -speed;
        } else{
            vel.y = 0;
        }
        rb.velocity = vel;

        var pos = transform.position;
        if(pos.y > boundY){
            pos.y = boundY;
        } else if(pos.y < -boundY){
            pos.y = -boundY;
        }
        transform.position = pos;
    }
}
