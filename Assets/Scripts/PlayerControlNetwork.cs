using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;

public class PlayerControlNetwork : AttributesSync
{
    public float speed = 10.0f;
    public float boundY = 4f;
    public float boundXfront = -2f;
    public float boundXback = -8f;
    private Rigidbody2DSynchronizable rb;
    private Alteruna.Avatar _avatar;
    private Multiplayer multiplayer;
    private bool isHost;

    [SerializeField] private GameObject ballPrefab;

    private float _vertical;
    private float _horizontal;

    void Start()
    {
        _avatar = GetComponent<Alteruna.Avatar>();
        if(!_avatar.IsMe)
            return;
        rb = GetComponent<Rigidbody2DSynchronizable>();
        //multiplayer = FindObjectOfType<Multiplayer>();
        //isHost = multiplayer.Me.IsHost;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!_avatar.IsMe)
            return;
    
        _vertical = Input.GetAxis("Vertical_WASD");
        _horizontal = Input.GetAxis("Horizontal_WASD");


        Vector3 movement = new Vector3(_horizontal, _vertical, 0) * speed;
        rb.velocity = movement;
        // if(isHost){
        //     transform.position = new Vector3(Mathf.Clamp(transform.position.x, boundXback, boundXfront), 
        //     Mathf.Clamp(transform.position.y, -boundY, boundY), 
        //     transform.position.z);
        // }
        // else{
        //     transform.position = new Vector3(Mathf.Clamp(transform.position.x, -boundXfront, -boundXback),
        //     Mathf.Clamp(transform.position.y, -boundY, boundY),
        //     transform.position.z);
        // }
    }
}