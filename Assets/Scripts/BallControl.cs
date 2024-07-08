using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    public AudioSource bounceSound;
    public float maxSpeed = 5f;
    private Rigidbody2D rb;
    public TrailRenderer tr;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        Invoke("GoBall", 2);
    }

    void GoBall(){
        float rand = Random.Range(0, 2);
        if (rand < 1){
            rb.AddForce(new Vector2(20, -15));
        } else{
            rb.AddForce(new Vector2(-20, -15));
        }
    }

    void ResetBall(){
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;
        tr.Clear();
    }

    void RestartGame(){
        ResetBall();
        Invoke("GoBall", 1);
    }

    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log(other);
        // bounceSound.Play();
        // Vector2 vel;
        // vel.x = rb.velocity.x;
        // vel.y = (rb.velocity.y / 2) + (other.collider.attachedRigidbody.velocity.y / 3);
        // rb.velocity = vel;
    }
}
