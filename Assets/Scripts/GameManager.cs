using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public string charL, charR;
    public TMP_Text playerSide;
    public GameObject endPanel;
    public int PlayerScoreL = 0;
    public int PlayerScoreR = 0;

    public TMP_Text txtPlayerScoreL;
    public TMP_Text txtPlayerScoreR;

    public static GameManager instance;
    public void Awake(){
        if(instance == null){
            instance = this;
        } else{
            Destroy(gameObject);
        }
    }

    void Start()
    {
        txtPlayerScoreL.text = PlayerScoreL.ToString();
        txtPlayerScoreR.text = PlayerScoreR.ToString();
    }

    public void Score(string wallID){
        if(wallID == "Line L"){
            PlayerScoreR = PlayerScoreR + 10;
            txtPlayerScoreR.text = PlayerScoreR.ToString();
           // Invoke("ScoreCheck", 1);
        } else{
            PlayerScoreL = PlayerScoreL + 10;
            txtPlayerScoreL.text = PlayerScoreL.ToString();
           // Invoke("ScoreCheck", 1);
        }
        ScoreCheck();
    }

    public void ChangetoMenu(){
        this.gameObject.SendMessage("ChangeScene", "MainMenu");
    }

    public void ScoreCheck(){
        if(PlayerScoreL == 50){
            Debug.Log("Player L Win!");
            playerSide.text = charL;
            endPanel.SetActive(true);
        } else if(PlayerScoreR == 50){
            Debug.Log("Player R Win!");
            playerSide.text = charR;
            endPanel.SetActive(true);
        }

        if((PlayerScoreL == 50) || (PlayerScoreR == 50)){
            Invoke("ChangetoMenu", 3);
        }
    }
}
