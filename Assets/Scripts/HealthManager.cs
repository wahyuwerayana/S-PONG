using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image currHealthL;
    public Sprite fourHealthL, threeHealthL, twoHealthL, oneHealthL;
    public Image currHealthR;
    public Sprite fourHealthR, threeHealthR, twoHealthR, oneHealthR;
    public int healthL = 5, healthR = 5;
    public static HealthManager instance;
    public void Awake(){
        if(instance == null){
            instance = this;
        } else{
            Destroy(gameObject);
        }
    }

    public void healthChange(char side){
        if(side == 'L'){
            if(healthL == 4){
                currHealthL.sprite = fourHealthL;
            } else if(healthL == 3){
                currHealthL.sprite = threeHealthL;
            } else if(healthL == 2){
                currHealthL.sprite = twoHealthL;
            } else if(healthL == 1){
                currHealthL.sprite = oneHealthL;
            }
        } else if(side == 'R'){
            if(healthR == 4){
                currHealthR.sprite = fourHealthR;
            } else if(healthR == 3){
                currHealthR.sprite = threeHealthR;
            } else if(healthR == 2){
                currHealthR.sprite = twoHealthR;
            } else if(healthR == 1){
                currHealthR.sprite = oneHealthR;
            }
        }
    }

    public void Score(string wallID){
        if(wallID == "Line L"){
            healthL -= 1;
            healthChange('L');
            Invoke("ScoreCheck", 1);
        } else{
            healthR -= 1;
            healthChange('R');
            Invoke("ScoreCheck", 1);
        }
    }

    public void ScoreCheck(){
        if(healthL == 0){
            Debug.Log("Player R Win!");
            this.gameObject.SendMessage("ChangeScene", "MainMenu");
        } else if(healthR == 0){
            Debug.Log("Player L Win!");
            this.gameObject.SendMessage("ChangeScene", "MainMenu");
        }
    }
}
