using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void ChangeScene(string sceneName){
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "2P Online Netcode"){
            Destroy(NetworkManager.Singleton.gameObject);
            NetworkManager.Singleton.Shutdown();
        }
        SceneManager.LoadScene(sceneName);
    }
}
