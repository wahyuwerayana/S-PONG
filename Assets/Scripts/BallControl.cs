using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    public AudioSource bounceSound;
    public float maxSpeed = 0.1f;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("GoBall", 1);
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
    }

    void RestartGame(){
        ResetBall();
        Invoke("GoBall", 1);
    }

    void OnCollisionEnter2D(Collision2D other) {
        bounceSound.Play();
        if(other.collider.CompareTag("Player L") || other.collider.CompareTag("Player R")){
            Vector2 vel;
            vel.x = rb.velocity.x;
            vel.y = (rb.velocity.y / 2) + (other.collider.attachedRigidbody.velocity.y / 3);
         //   if(rb.velocity.magnitude > maxSpeed){
        //        rb.velocity = rb.velocity.normalized * maxSpeed;
       //     } else{
                rb.velocity = vel;
      //      }
        } 
    }
}
