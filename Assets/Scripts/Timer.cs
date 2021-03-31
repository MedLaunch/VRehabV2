using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float defaultTime = 30f;
    public Text timerText;

    public bool isPaused;
    public bool isGameDone;
    IEnumerator time;

    // Update is called once per frame

    void Start(){
        isPaused = false;
        isGameDone = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            StartTimer();
    }

    public float GetCurrentTime() {
        return defaultTime;
    }

    public void ChangeTime(float deltaTime) {
        defaultTime += deltaTime;
    }

    public void StartTimer() {
        if(time != null) { StopCoroutine(time); }
        Debug.Log("starting timer again");
        time = _StartTimer();
        StartCoroutine(time);
    }

    IEnumerator _StartTimer() {
        float currTime = defaultTime;
        isGameDone = false;
        Debug.Log("curr time: " + currTime);
        while (currTime > 0) {
            if(!isPaused){
                currTime -= Time.deltaTime;
            }
            DisplayTime(currTime);
            yield return null;
        }
        timerText.text = "Time: Done!";
        isGameDone = true;
    }

    private void DisplayTime(float time) {
        time += 1;
        float seconds = Mathf.FloorToInt(time);
        timerText.text = "Time: " + seconds.ToString();
    }

    public bool GetGameStatus(){
        return isGameDone;
    }

    public void SetPaused(bool paused)
    {
        isPaused = paused;
    }
    public void CompleteGame()
    {
        StopCoroutine(time);
        timerText.text = "Time: Done!";
    }

    public void ResetTimer(){
        float seconds = Mathf.FloorToInt(defaultTime);
        timerText.text = "Time: " + seconds.ToString();
    }
}
