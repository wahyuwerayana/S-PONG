using Alteruna;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SideWallNetwork : AttributesSync
{
    public AudioSource goalSound;
    private Multiplayer multiplayer;

    private void Start() {
        multiplayer = FindObjectOfType<Multiplayer>();
    }

    void OnTriggerEnter2D(Collider2D hitInfo) {
        if(hitInfo.name == "Ball"){
            goalSound.Play();
            string wallName = transform.name;
            if(multiplayer.Me.IsHost){
                GameManagerNetwork.instance.BroadcastRemoteMethod(nameof(Score), wallName);
                hitInfo.GetComponent<BallControlNetwork>().RestartGame();
                hitInfo.gameObject.SendMessage("RestartGame", 1.0f, SendMessageOptions.RequireReceiver);
            }
        }
    }
}
