using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WineTable : MonoBehaviour
{
    // Start is called before the first frame update
    public Text percentText;
    public float percent;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        percent = GameObject.Find("wine glass").GetComponent<IntakeLiquid>().liquid;
        SetPercentCount();
    }
    void SetPercentCount()
    {
        percentText.text = "Percent: " + percent.ToString();
    }

    void OnParticleCollision(GameObject other)
    {

    }
}
