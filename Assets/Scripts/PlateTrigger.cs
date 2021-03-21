using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    TableController tableScript;
    void Start()
    {
        tableScript = GameObject.Find("Table Top").GetComponent<TableController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Food")) {
            // CheckPosition(other.gameObject);
            // placed = true;
            Debug.Log("Food collided with plate!");
        }
            
    }
}
