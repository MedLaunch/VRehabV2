using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateTime : MonoBehaviour
{
    private float defaultTime;
    public Text time;
    // Start is called before the first frame update
    void Start()
    {
        defaultTime = GameObject.Find("Pouring Wine").GetComponent<Timer>().GetCurrentTime();
        time.text = ((int)defaultTime) / 60 + ":" + defaultTime % 60;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void UpdateTime()
    {
        defaultTime = GameObject.Find("Pouring Wine").GetComponent<Timer>().GetCurrentTime();
        time.text = ((int)defaultTime) / 60 + ":" + defaultTime % 60;
    }
}
