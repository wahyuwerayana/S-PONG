using UnityEngine;

public class SideWall : MonoBehaviour
{
    public AudioSource goalSound;
    void OnTriggerEnter2D(Collider2D hitInfo) {
        if(hitInfo.name == "Ball"){
            goalSound.Play();
            string wallName = transform.name;
            GameManager.instance.Score(wallName);
            hitInfo.gameObject.SendMessage("RestartGame", 1.0f, SendMessageOptions.RequireReceiver);
        }
    }
}
