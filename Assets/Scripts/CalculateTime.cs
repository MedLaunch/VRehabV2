using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateTime : MonoBehaviour
{
    private float defaultTime;
    private Timer timerScript;
    public Text time;
    // Start is called before the first frame update
    void Start()
    {
        timerScript = gameObject.GetComponent<Timer>();
        defaultTime = timerScript.GetCurrentTime();
        time.text = ((int)defaultTime) / 60 + ":" + defaultTime % 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            UpdateTime(-10f);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            UpdateTime(10f);
        }
    }
    
    public void UpdateTime(float updateTime)
    {
        if(timerScript.GetCurrentTime() + updateTime < 0)
        {
            updateTime = 0;
        }
        timerScript.ChangeTime(updateTime);
        defaultTime = timerScript.GetCurrentTime();
        if (defaultTime % 60 == 0)
        {
            time.text = ((int)defaultTime) / 60 + ":" + defaultTime % 60 + "0";
        }
        else
        {
            time.text = ((int)defaultTime) / 60 + ":" + defaultTime % 60;
        }
    }
}
