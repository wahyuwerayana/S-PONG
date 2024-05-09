using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenu;

    public void checkPaused(){
            isPaused = !isPaused;

            if(isPaused){
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            } else{
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
            }
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            checkPaused();
        }
    }
}