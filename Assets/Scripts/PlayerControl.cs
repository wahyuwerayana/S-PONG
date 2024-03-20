using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerControl : MonoBehaviour
{
    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;
    public float speed = 10.0f;
    public float boundY = 3f;
    public float boundYdown = 4f;
    public float boundXfront = -2f;
    public float boundXback = -8f;
    private Rigidbody2D rb;
    private GameObject playerTagsL;
    private GameObject playerTagsR;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTagsL = GameObject.FindWithTag("Player L");
        playerTagsR = GameObject.FindWithTag("Player R");
    }

    // Update is called once per frame
    void Update()
    {
        var vel = rb.velocity;
        if(Input.GetKey(moveUp) || Input.GetKey(moveDown) || Input.GetKey(moveLeft) || Input.GetKey(moveRight)){
            if(Input.GetKey(moveUp)){
            vel.y = speed;
            } if(Input.GetKey(moveDown)){
            vel.y = -speed;
            } if(Input.GetKey(moveLeft)){
            vel.x = -speed;
            } if(Input.GetKey(moveRight)){
            vel.x = speed;
            }
        } else{
            vel.y = 0;
            vel.x = 0;
        }
        rb.velocity = vel;

        var pos = transform.position;
        if(pos.y > boundY){
            pos.y = boundY;
        } else if(pos.y < -boundYdown){
            pos.y = -boundYdown;
        } else if((pos.x > boundXfront) && (rb.gameObject.tag == "Player L")){
            pos.x = boundXfront;
        } else if(pos.x < boundXback && (rb.gameObject.tag == "Player L")){
            pos.x = boundXback;
        } else if((pos.x < -boundXfront) && (rb.gameObject.tag == "Player R")){
            pos.x = -boundXfront;
        } else if(pos.x > -boundXback && (rb.gameObject.tag == "Player R")){
            pos.x = -boundXback;
        }
        transform.position = pos;
    }
}
