using System.Collections;
using UnityEngine;
using Unity.Netcode;

public class BallControlNetwork : NetworkBehaviour
{
    public AudioClip bounceClip;
    private AudioSource bounceSound;
    public float moveSpeed = 5f;
    private float syncedCountdown = 1f;
    private Rigidbody2D rb;
    public TrailRenderer tr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        bounceSound = GetComponent<AudioSource>();
        bounceSound.clip = bounceClip;

        StartCoroutine(StartBallCooldown());
    }

    IEnumerator StartBallCooldown(){
        while(syncedCountdown > 0){
            syncedCountdown -= Time.fixedDeltaTime;
            yield return null;
        }

        if(GameManagerNetwork.instance.playerScoreLServer.Value >= GameManagerNetwork.SCORE || 
            GameManagerNetwork.instance.playerScoreRServer.Value >= GameManagerNetwork.SCORE){
            if(IsServer) DespawnBallRpc();
            yield break;
        }
            
        GoBallRpc();
    }

    [Rpc(SendTo.Everyone)]
    private void GoBallRpc(){
        float rand = Random.Range(-1f, 1f);
        Vector2 startForce = new Vector2(rand, rand).normalized * moveSpeed;
        rb.velocity = startForce;
    }

    [Rpc(SendTo.Everyone)]
    void ResetBallRpc(){
        tr.Clear();
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;
    }

    [Rpc(SendTo.Everyone)]
    public void RestartGameRpc(){
        ResetBallRpc();
        syncedCountdown = 2f;
        StartCoroutine(StartBallCooldown());
    }

    [Rpc(SendTo.Server)]
    public void DespawnBallRpc(){
        GameObject ball = gameObject;
        if(ball!= null){
            ball.GetComponent<NetworkObject>().Despawn();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        bounceSound.Play();
    }
}
