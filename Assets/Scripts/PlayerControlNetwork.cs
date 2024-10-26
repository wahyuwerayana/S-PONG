using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerControlNetwork : NetworkBehaviour
{
    public float speed = 10.0f;
    public float boundY = 4f;
    public float boundXfront = -2f;
    public float boundXback = -8f;
    private Rigidbody2D rb;
    private float zRotation = 90f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!IsOwner)
            return;

        MovePlayerServerAuth();
    }

    public override void OnNetworkSpawn()
    {
        UpdatePositionServerRpc();
    }

    public override void OnNetworkDespawn()
    {
        //GameManagerNetwork.instance.ChangetoMenu();
    }

    [Rpc(SendTo.Everyone, RequireOwnership = false)]
    private void UpdatePositionServerRpc(){
        Vector3 startPosition;
        Quaternion startRotation;

        if((int)OwnerClientId == 0){
            startPosition = new Vector3(-6f, 0f, 0f);
            startRotation = Quaternion.Euler(0f, 0f, -zRotation);
        } else{
            startPosition = new Vector3(6f, 0f, 0f);
            startRotation = Quaternion.Euler(0f, 0f, zRotation);
        }

        transform.position = startPosition;
        transform.rotation = startRotation;
    }

    private void MovePlayerServerAuth(){
        float _vertical = Input.GetAxis("Vertical_WASD");
        float _horizontal = Input.GetAxis("Horizontal_WASD");
        MovePlayerServerRpc(_horizontal, _vertical);
    }

    [ServerRpc(RequireOwnership = false)]
    private void MovePlayerServerRpc(float _horizontal, float _vertical){
        Vector3 movement = new Vector3(_horizontal, _vertical, 0) * speed;
        rb.velocity = movement;
        if((int)OwnerClientId == 0){
            transform.position = new Vector3(Mathf.Clamp(rb.position.x, boundXback, boundXfront), 
            Mathf.Clamp(rb.position.y, -boundY, boundY));
        }
        else{
            transform.position = new Vector3(Mathf.Clamp(rb.position.x, -boundXfront, -boundXback),
            Mathf.Clamp(rb.position.y, -boundY, boundY));
        }
    }
}