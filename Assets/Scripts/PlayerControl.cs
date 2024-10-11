using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 10.0f;
    public float boundY = 4f;
    public float boundXfront = -2f;
    public float boundXback = -8f;
    private Rigidbody2D rb;
    public bool isLeftPlayer;

    private float _vertical;
    private float _horizontal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isLeftPlayer){
            _vertical = Input.GetAxis("Vertical_WASD");
            _horizontal = Input.GetAxis("Horizontal_WASD");
        } else{
            _vertical = Input.GetAxis("Vertical_Arrow");
            _horizontal = Input.GetAxis("Horizontal_Arrow");
        }

        Vector3 movement = new Vector3(_horizontal, _vertical, 0) * speed;
        rb.velocity = movement;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, boundXback, boundXfront), Mathf.Clamp(transform.position.y, -boundY, boundY), transform.position.z);
    }
}