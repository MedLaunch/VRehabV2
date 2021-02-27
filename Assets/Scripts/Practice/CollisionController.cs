using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour {
    public GameObject ball, cube;
    public Vector3 startPos;

    private GameObject ballObject;
    private bool drop = false;
    private Color startColor;

    // Start is called before the first frame update
    void Start() {
        startColor = cube.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            if (drop) {
                Drop();
                drop = false;
            } else {
                Spawn();
                drop = true;
            }
            
        }
    }

    private void Spawn() {
        cube.GetComponent<Renderer>().material.SetColor("_Color", startColor);
        GameObject temp = ballObject;
        Destroy(temp);
        ballObject = Instantiate(ball, startPos, Quaternion.identity);
        ballObject.GetComponent<Rigidbody>().useGravity = false;
    }

    private void Drop() {
        ballObject.GetComponent<Rigidbody>().useGravity = true;
    }
}
