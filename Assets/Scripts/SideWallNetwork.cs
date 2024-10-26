using Unity.Netcode;
using UnityEngine;

public class SideWallNetwork : NetworkBehaviour
{
    public AudioSource goalSound;

    private void OnTriggerEnter2D(Collider2D hitInfo) {
        if(hitInfo.CompareTag("Ball")){
            goalSound.Play();
            string wallName = transform.name;
            GameManagerNetwork.instance.ScoreServerRpc(wallName);
            hitInfo.GetComponent<BallControlNetwork>().RestartGameRpc();
        }
    }
}
