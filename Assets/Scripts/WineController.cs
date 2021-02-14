using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WineController : MonoBehaviour
{
    public int numGlasses;
    public GameObject glass;
    public GameObject clone;
    public Transform spawn;
    bool glassSpawned;
    // Start is called before the first frame update
    void Start()
    {
        glassSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        while (numGlasses > 0)
        {
            clone = Instantiate(glass, spawn.position, spawn.rotation);  // Spawns gameObject
            
        }
    }
}
