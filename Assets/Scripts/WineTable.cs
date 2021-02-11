using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WineLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public Text percentText;
    public float percent;
    public float totalTimeOut = 0f;
    public float totalTimeLanded = 0f;  // total time liquid is inside wine
    public bool drip;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        totalTimeOut = GameObject.Find("wine bottle").GetComponent<PourLiquid>().totalTimeOut;
        SetPercentCount();
    }
    void SetPercentCount()
    {
        percentText.text = "Percent: " + totalTimeLanded.ToString() + " / " + totalTimeOut.ToString();
    }

    void OnParticleCollision(GameObject other)
    {
        totalTimeLanded += Time.deltaTime;
    }
}
