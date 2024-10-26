using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerSettings : NetworkBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public List<Sprite> sprites;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void OnNetworkSpawn(){
        spriteRenderer.sprite = ((int)OwnerClientId < 2) ? sprites[(int)OwnerClientId] : sprites[1];
    }
}