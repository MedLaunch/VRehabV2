using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private int difficulty = 0;

    public Text minutes;
    public Text seconds;
    public Slider slider;

    public Text foodCount;

    Timer timerScript;

    TableController tableScript;
    public void Start(){
        timerScript = GameObject.Find("Food Placement").GetComponent<Timer>();
        tableScript = GameObject.Find("Table Top").GetComponent<TableController>();
    }
    // cannot change these difficulty parameters mid-game
    public void DecreaseDifficulty(){
        if(difficulty > 0){
            slider.value -= 1;
            tableScript.SetDifficulty((int)slider.value);
        }
        difficulty = (int)slider.value;
    }
    public void IncreaseDifficulty(){
        if(difficulty < 3){
            slider.value += 1;
            tableScript.SetDifficulty((int)slider.value);
        }
        difficulty = (int)slider.value;
    }
    public void IncreaseTime(){
        if(int.Parse(minutes.text) <= 9){
            int new_seconds = int.Parse(seconds.text) + 10;
            if(new_seconds == 60 && int.Parse(minutes.text) + 1 <= 9){
                minutes.text = (int.Parse(minutes.text) + 1).ToString();
                seconds.text = "00";
                timerScript.ChangeTime(10);
            } 
            else if(new_seconds == 60 && int.Parse(minutes.text) + 1 > 9){
                seconds.text = "59";
            }
            else {
                if(new_seconds < 10){
                    seconds.text = "0" + new_seconds.ToString();
                } else {
                    seconds.text = new_seconds.ToString();
                }
                timerScript.ChangeTime(10);
            }
        }
    }
    public void DecreaseTime(){
        int current_minutes = int.Parse(minutes.text);
        int new_seconds = int.Parse(seconds.text) - 10;

        if(new_seconds <= 0){
            seconds.text = "00";
            if(new_seconds == -10 && current_minutes > 0){
                minutes.text = (current_minutes - 1).ToString();
                seconds.text = "50";
            }
            timerScript.ChangeTime(-10);
        } else {
            if(new_seconds < 10){
                seconds.text = "0" + new_seconds.ToString();
            } else {
                seconds.text = new_seconds.ToString();
            }
            timerScript.ChangeTime(-10);
        }
    }
    public void IncreaseNumFood(){
        int numFood = int.Parse(foodCount.text);
        if(numFood < 20){
            foodCount.text = (numFood + 1).ToString();
            tableScript.AddFood();
        }
    }
    public void DecreaseNumFood(){
        int numFood = int.Parse(foodCount.text);
        if(numFood > 1){
            foodCount.text = (numFood - 1).ToString();
            tableScript.RemoveFood();
        }

    }
    public int GetDifficulty(){
        return difficulty;
    }

}
