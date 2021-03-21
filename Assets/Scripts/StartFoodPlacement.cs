using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartFoodPlacement : MonoBehaviour
{
    public Text timer;
    public Text points;
    public Slider slider;

    private TableController tableScript;
    private Settings settingsScript;

    private Timer timerScript;

    // 0 is paused, 1 is playing
    private int gameStatus;

    private bool waitForPlayer = false;

    public void Play() {
        timerScript.StartTimer();
        gameStatus = 1;
        tableScript.SetGameStatus(true);
        tableScript.ToggleOutlineVisibility(true);
        tableScript.SetCurrentFood();
        points.text = "Points: 0";
        Debug.Log("Set square to visible");   
    }

    public void Pause(){
        if(tableScript.GetGameStatus()){
            gameStatus = 0;
            tableScript.SetGameStatus(false);
            timerScript.isPaused = true;
        }
    }

    public void Continue(){
        if(!tableScript.GetGameStatus()){
            gameStatus = 1;
            tableScript.SetGameStatus(true);
            timerScript.isPaused = false;
        }
    }
    
    public void GameOver(){
        gameStatus = 0;
        tableScript.SetGameStatus(false);
        timerScript.isPaused = true;
        tableScript.ResetGame();
        waitForPlayer = true;
    }
    void Start(){
        tableScript = GameObject.Find("Table Top").GetComponent<TableController>();
        settingsScript = gameObject.GetComponent<Settings>();
        timerScript = gameObject.GetComponent<Timer>();
        timerScript.defaultTime = 30f;
        gameStatus = 0;
    }
    void Update(){
        // For testing purposes
        if(Input.GetKeyDown(KeyCode.W)){
            Play();
            Debug.Log("started game");
        } else if(Input.GetKeyDown(KeyCode.RightArrow)){
            if(timerScript.isPaused){
                Continue();
            } else {
                Pause();
            }
        } else if(Input.GetKeyDown(KeyCode.UpArrow)){
            settingsScript.IncreaseTime();
        } else if(Input.GetKeyDown(KeyCode.DownArrow)){
            settingsScript.DecreaseTime();
        } else if(Input.GetKeyDown(KeyCode.LeftBracket)){
            settingsScript.DecreaseNumFood();
        } else if(Input.GetKeyDown(KeyCode.RightBracket)){
            settingsScript.IncreaseNumFood();
        } else if(Input.GetKeyDown(KeyCode.Comma)){
            settingsScript.DecreaseDifficulty();
        } else if(Input.GetKeyDown(KeyCode.Period)){
            settingsScript.IncreaseDifficulty();
        } else if(Input.GetKeyDown(KeyCode.Q)){
            tableScript.TestFood();
        } else if (Input.GetKeyDown(KeyCode.A)) {
            tableScript.RandomizeOutlinePosition();
        }

        // make this only happen once
        if(gameStatus != 0 && (!waitForPlayer && tableScript.GetGameOver() || timerScript.GetGameStatus())){
            GameOver();
        }
    }

    public void SetGameStatus(int status){
        gameStatus = status;
    }

}
