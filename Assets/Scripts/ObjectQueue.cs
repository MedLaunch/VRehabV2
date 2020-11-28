using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectQueue : MonoBehaviour {
    Queue<GameObject> objectQ = new Queue<GameObject>();
    bool getNextObject = true;

    // Start is called before the first frame update
    void Start() {
        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
            objectQ.Enqueue(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update() {
        if (getNextObject && objectQ.Count != 0) {
            getNextObject = false;
            GameObject currObject = objectQ.Dequeue();
            currObject.SetActive(true);
        }
    }

    public void SetGetNextObject(bool getNext) {
        getNextObject = getNext;
    }
}
