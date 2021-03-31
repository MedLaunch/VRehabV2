using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_detection : MonoBehaviour
{
    public Launch other;
    public Timer timeCount;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (!timeCount.GetGameStatus())
        {
            if (!collision.gameObject.CompareTag("Stump"))
            {
                if (collision.gameObject.CompareTag("Target"))
                {
                    other.hitTarget();
                }
                else if (collision.gameObject.CompareTag("Floor"))
                {
                    other.hitGround();
                }

            }
        }
    }
}
