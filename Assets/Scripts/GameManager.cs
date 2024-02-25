using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
            ScoreCheck();
        } else{
            PlayerScoreL = PlayerScoreL + 10;
            txtPlayerScoreL.text = PlayerScoreL.ToString();
            ScoreCheck();
        }
    }

    public void ScoreCheck(){
        if(PlayerScoreL == 50){
            Debug.Log("Player L Win!");
            this.gameObject.SendMessage("ChangeScene", "MainMenu");
        } else if(PlayerScoreR == 50){
            Debug.Log("Player R Win!");
            this.gameObject.SendMessage("ChangeScene", "MainMenu");
        }
    }
}
