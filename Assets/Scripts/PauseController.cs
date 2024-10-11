using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenu;

    void Start(){
        //Cursor.visible = false;
    }

    public void checkPaused(){
            isPaused = !isPaused;

            if(isPaused){
                //Cursor.visible = true;
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            } else{
                //Cursor.visible = false;
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