using System.Collections;
using System.Collections.Generic;
using Alteruna;
using TMPro;
using UnityEngine;

public class GameManagerNetwork : AttributesSync
{
    public string charL, charR;
    public TMP_Text playerSide;
    public GameObject startPanel;
    public GameObject endPanel;
    [SynchronizableField] public int PlayerScoreL = 0;
    [SynchronizableField] public int PlayerScoreR = 0;

    public TMP_Text txtPlayerScoreL;
    public TMP_Text txtPlayerScoreR;

    private Multiplayer multiplayer;

    public static GameManagerNetwork instance;
    public void Awake(){
        if(instance == null){
            instance = this;
        } else{
            Destroy(gameObject);
        }
    }

    void Start()
    {
        multiplayer = FindObjectOfType<Multiplayer>();
        startPanel.SetActive(true);
        txtPlayerScoreL.text = PlayerScoreL.ToString();
        txtPlayerScoreR.text = PlayerScoreR.ToString();
        
    }

    [SynchronizableMethod]
    public void Score(string wallID){
        if(wallID == "Line L"){
            PlayerScoreR += 10;
        } else if(wallID == "Line R"){
            PlayerScoreL += 10;
        }
        txtPlayerScoreR.text = PlayerScoreR.ToString();
        txtPlayerScoreL.text = PlayerScoreL.ToString();
        BroadcastRemoteMethod(nameof(ScoreCheck));
    }

    public void ChangetoMenu(){
        this.gameObject.SendMessage("ChangeScene", "MainMenu");
    }

    [SynchronizableMethod]
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
