using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float defaultTime = 30f;
    public Text timerText;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            StartTimer();
    }

    public void StartTimer() {
        StartCoroutine(_StartTimer());
    }

    IEnumerator _StartTimer() {
        float currTime = defaultTime;
        while (currTime > 0) {
            currTime -= Time.deltaTime;
            DisplayTime(currTime);
            yield return null;
        }
        timerText.text = "Time: Done!";
    }

    private void DisplayTime(float time) {
        time += 1;
        float seconds = Mathf.FloorToInt(time);
        timerText.text = "Time: " + seconds.ToString();
    }
}
