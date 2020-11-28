using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{
    ObjectQueue objectQueue;

    // Start is called before the first frame update
    void Start()
    {
        objectQueue = GameObject.Find("Foods").GetComponent<ObjectQueue>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.CompareTag("Food")) {
            objectQueue.SetGetNextObject(true);
        }
    }
}
