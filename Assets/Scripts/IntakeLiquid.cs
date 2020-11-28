using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntakeLiquid : MonoBehaviour
{
    // Start is called before the first frame update
    // public TextMeshProUGUI liquidCount;
    public Text liquidCount;
    public int liquid = 0;
    int oldLiquid = 0;
    GameObject[] children;
    int currentIndex;
    float interval = 0f;
    float step = 1.5f;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (oldLiquid != liquid)
        {
            ChangeLiquid();
            oldLiquid = liquid;
        }

    }

    void SetLiquidCount()
    {
        liquidCount.text = "Liquid Level: " + liquid.ToString();
    }

    void OnParticleCollision(GameObject other)
    {
        if (Time.time - interval > step && liquid != 3)
        {
            liquid++;
            interval = Time.time;
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
}

// in start
// private Queue<GameObject> foodObjects;
// GetComponentInChildren<>;
//foreach (Transform child in transform)
//{
//   foodObjects.Enqueue(child.gameObject);
//}