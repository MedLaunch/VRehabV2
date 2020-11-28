using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectQueue : MonoBehaviour {
    TableController tableController;
    Queue<GameObject> objectQ = new Queue<GameObject>();
    bool getNextObject;

    // Start is called before the first frame update
    void Start() {
        tableController = GameObject.Find("Table").GetComponent<TableController>();
        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
            objectQ.Enqueue(child.gameObject);
        }
        getNextObject = objectQ.Count != 0;
    }

    public GameObject GetNextObject() {
        if (objectQ.Count != 0) {
            GameObject currObject = objectQ.Dequeue();
            currObject.SetActive(true);
            return currObject;
        } else {
            return null;
        }
    }
}
