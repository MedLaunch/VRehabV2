using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectQueue : MonoBehaviour {
    TableController tableController;
    Queue<GameObject> objectQ = new Queue<GameObject>();

    GameObject[] availableFoodItems = new GameObject[6];
    bool getNextObject;


    // put the object queue filling in awake
    void Awake() {
        tableController = GameObject.Find("Table Top").GetComponent<TableController>();
        int i = 0;
        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
            objectQ.Enqueue(child.gameObject);
            availableFoodItems[i] = Instantiate(child.gameObject);
            i += 1;
        }
        getNextObject = objectQ.Count != 0;
    }

    public void ResetQueue(){
        int food = tableController.GetNumFood();
        objectQ.Clear();
        for(int i = 0; i < food; i = i + 1){
            AddObject();
        }
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

    public void AddObject(){
        int index = Random.Range(0, 5);
        GameObject food = Instantiate(availableFoodItems[index]);
        food.transform.parent = gameObject.transform;
        food.transform.localPosition = availableFoodItems[index].transform.position;
        food.transform.localRotation = availableFoodItems[index].transform.rotation;
        food.transform.localScale = availableFoodItems[index].transform.localScale;
        food.SetActive(false);
        objectQ.Enqueue(food);
    }

    public int GetCount(){
        return objectQ.Count;
    }

}
