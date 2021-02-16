using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntakeLiquid : MonoBehaviour
{
    // Start is called before the first frame update
    public Text liquidCount;
    public double liquid = 0;
    double oldLiquid = 0;
    GameObject[] children;
    int currentIndex;
    float interval = 0f;
    float step = 1.5f;
    float currTime = 0f;
    void Start()
    {
        bool skipFirst = transform.childCount > 4;
        children = new GameObject[skipFirst ? transform.childCount - 1 : transform.childCount];
        for (int i = 0; i < children.Length; i++)
        {
            children[i] = transform.GetChild(skipFirst ? i + 1 : i).gameObject;
            if (children[i].activeInHierarchy)
                currentIndex = i;
        }
        liquidCount.gameObject.SetActive(true);
        SetLiquidCount();
        //Debug.Log(children.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (oldLiquid != liquid)
        {
            ChangeLiquid();
            oldLiquid = liquid;
        }
        SetLiquidCount();  // for debugging
    }

    void SetLiquidCount()
    {
        liquidCount.text = "Filled: " + GetFillPercent().ToString() + "%";
    }

    int GetFillPercent()
    {
        return (int) (liquid / (children.Length) * 100);
    }

    void OnParticleCollision(GameObject other)
    {
        currTime += Time.deltaTime;
        if (currTime > step && liquid != 3)
        {
            liquid++;
            currTime = 0f;
        }
    }
    void ChangeLiquid()
    {
        if (liquid == 1)
        {
            children[currentIndex].SetActive(true);
            SetLiquidCount();
        }
        if (liquid == 2)
        {
            children[currentIndex++].SetActive(false);
            children[currentIndex].SetActive(true);
            SetLiquidCount();
        }
        if (liquid == 3)
        {
            children[currentIndex++].SetActive(false);
            children[currentIndex].SetActive(true);
            SetLiquidCount();
        }
    }
    public bool GetFull()
    {
        if(liquid == 3)
        {
            return true;
        }
        return false;
    }
}

// in start
// private Queue<GameObject> foodObjects;
// GetComponentInChildren<>;
//foreach (Transform child in transform)
//{
//   foodObjects.Enqueue(child.gameObject);
//}