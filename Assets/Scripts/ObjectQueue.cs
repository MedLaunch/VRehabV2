using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectQueue : MonoBehaviour {
    TableController tableController;
    Queue<GameObject> objectQ = new Queue<GameObject>();

    GameObject[] availableFoodItems = new GameObject[6];
    GameObject[] availablePlates = new GameObject[3];
    bool getNextObject;


    // put the object queue filling in awake
    void Awake() {
        tableController = GameObject.Find("Table Top").GetComponent<TableController>();
        int i = 0;
        int p = 0;
        foreach (Transform child in transform) {
            if(child.gameObject.tag == "Plate"){
                child.gameObject.SetActive(false);
                availablePlates[p] = Instantiate(child.gameObject);
                p += 1;
            }
        }
        foreach (Transform child in transform) {
            if(child.gameObject.tag == "Food"){
                child.gameObject.SetActive(false);
                int plateIndex = Random.Range(0, 2);
                objectQ.Enqueue(Instantiate(availablePlates[plateIndex]));
                objectQ.Enqueue(child.gameObject);
                availableFoodItems[i] = Instantiate(child.gameObject);
                i += 1;
            }
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
            if(currObject.tag == "Plate"){
                currObject.transform.position = new Vector3(158.2f, 0.6f, 63.6f);
            }
            Debug.Log(currObject.transform.position);
            return currObject;
        } else {
            return null;
        }
    }

    public void AddObject(){
        // TODO: When adding a new food item, also add a plate!
        int index = Random.Range(0, 5);
        int plateIndex = Random.Range(0, 2);
        GameObject food = Instantiate(availableFoodItems[index]);
        GameObject plate = Instantiate(availablePlates[plateIndex]);
        food.transform.parent = gameObject.transform;
        food.transform.localPosition = availableFoodItems[index].transform.position;
        food.transform.localRotation = availableFoodItems[index].transform.rotation;
        food.transform.localScale = availableFoodItems[index].transform.localScale;
        food.SetActive(false);
        plate.SetActive(false);
        objectQ.Enqueue(plate);
        objectQ.Enqueue(food);
    }

    public int GetCount(){
        return objectQ.Count;
    }

}
