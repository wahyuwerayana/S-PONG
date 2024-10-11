using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;

public class BallControlNetwork : AttributesSync
{
    public AudioClip bounceSoundClip;
    public AudioSource bounceSound;
    public float moveSpeed = 5f;
    private float syncedCountdown = 1f;
    private float maxSpeed = 10f;
    private Rigidbody2DSynchronizable rb;
    public TrailRenderer tr;
    private bool isHost;
    private Multiplayer multiplayer;

    [SynchronizableField]
    private Vector2 syncedPosition;

    [SynchronizableField]
    private Vector2 syncedVelocity;

    private void Awake(){
        rb = GetComponent<Rigidbody2DSynchronizable>();
        multiplayer = FindObjectOfType<Multiplayer>();
        isHost = multiplayer.Me.IsHost;
        tr = GetComponent<TrailRenderer>();
    }

    void Start()
    {
        if(isHost){
            StartCoroutine(StartBallCooldown());
        }
        // tr = GetComponent<TrailRenderer>();
        // //bounceSound.clip = bounceSoundClip;
        // BroadcastRemoteMethod(nameof(GoBall));
    }

    private void FixedUpdate() {
        Debug.Log(rb.velocity + " magnitude -> " + rb.velocity.magnitude);
    }

    [SynchronizableMethod]
    IEnumerator StartBallCooldown(){
        while(syncedCountdown > 0){
            syncedCountdown -= Time.deltaTime;
            Commit();
            yield return null;
        }
        
        BroadcastRemoteMethod(nameof(GoBall));
    }

    [SynchronizableMethod]
    private void GoBall(){
        float rand = Random.Range(-1f, 1f);
        Vector2 startForce = new Vector2(rand, rand).normalized * moveSpeed;
        rb.velocity = startForce;
        Commit();
    }

    [SynchronizableMethod]
    void ResetBall(){
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;
        tr.Clear();
        Commit();
    }

    public void RestartGame(){
        if(isHost){
            if(GameManagerNetwork.instance.PlayerScoreL >= 50 || GameManagerNetwork.instance.PlayerScoreR >= 50)
                return;

            BroadcastRemoteMethod(nameof(ResetBall));
            syncedCountdown = 1f;
            Commit();
            StartCoroutine(StartBallCooldown());
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(isHost){
            if(rb.velocity.magnitude > maxSpeed){
                rb.velocity = rb.velocity.normalized * maxSpeed;
                Commit();
            }
        }
    }
}
