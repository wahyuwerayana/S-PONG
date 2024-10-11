using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;

public class BallControlNetwork : AttributesSync
{
    public AudioClip bounceSoundClip;
    public AudioSource bounceSound;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    public TrailRenderer tr;
    private bool isHost;
    private Multiplayer multiplayer;

    [SynchronizableField]
    private Vector2 syncedPosition;

    [SynchronizableField]
    private Vector2 syncedVelocity;

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        multiplayer = FindObjectOfType<Multiplayer>();
        isHost = multiplayer.Me.IsHost;
        tr = GetComponent<TrailRenderer>();
    }

    void Start()
    {
        if(isHost){
            GoBall();
        }


        // tr = GetComponent<TrailRenderer>();
        // //bounceSound.clip = bounceSoundClip;
        // BroadcastRemoteMethod(nameof(GoBall));
    }

    [SynchronizableMethod]
    private void GoBall(){
        float rand = Random.Range(-1f, 1f);
        Vector2 startForce = new Vector2(rand, rand).normalized * moveSpeed;
        rb.velocity = startForce;

        syncedVelocity = startForce;
        syncedPosition = rb.position;
        Commit();
    }

    private void FixedUpdate() {
        if(isHost){
            syncedPosition = rb.position;
            syncedVelocity = rb.velocity;
            Commit();
        } else{
            rb.MovePosition(Vector2.Lerp(rb.position, syncedPosition, Time.fixedDeltaTime * 10f));
            rb.velocity = syncedVelocity;
        }
    }

    private void OnPositionChanged(Vector2 newPosition){
        if(!isHost){
            if(Vector2.Distance(rb.position, newPosition) > 1f){
                rb.position = newPosition;
            }
        }
    }

    private void OnVelocityChanged(Vector2 newVelocity){
        if(!isHost){
            rb.velocity = newVelocity;
        }
    }

    public void OnBallHit(Vector2 hitPoint, Vector2 newDirection){
        if(isHost){
            rb.velocity = newDirection * moveSpeed;
            syncedVelocity = rb.velocity;
            Commit();
        }
    }

    [SynchronizableMethod]
    void ResetBall(){
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;
        tr.Clear();
    }

    [SynchronizableMethod]
    public void RestartGame(){
        //if(GameManagerNetwork.instance.PlayerScoreL >= 50 || GameManagerNetwork.instance.PlayerScoreR >= 50)
            //return;

        BroadcastRemoteMethod(nameof(ResetBall));
        //ResetBall();
        BroadcastRemoteMethod(nameof(GoBall));
        //Invoke("GoBall", 1);
    }

    void OnCollisionEnter2D(Collision2D other) {
        //bounceSound.Play();
    }
}
