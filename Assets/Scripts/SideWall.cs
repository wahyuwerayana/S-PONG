using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class SideWall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D hitInfo) {
        if(hitInfo.name == "Ball"){
            string wallName = transform.name;
            GameManager.instance.Score(wallName);
            hitInfo.gameObject.SendMessage("RestartGame", 1.0f, SendMessageOptions.RequireReceiver);
        }
    }
}
