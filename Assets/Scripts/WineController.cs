using UnityEngine;
using System.Collections;

public class WineController : MonoBehaviour {
    public GameObject wineGlassPrefab;
    public Vector3 spawnPos;
    public int numGlasses = 3; // Max: 10

    private GameObject[] wineGlasses;
    private GameObject currGlass;
    private bool currGlassFilled = false;  // changed by SignalController 
    /// </summary>
    private int numFilled = 0;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (currGlassFilled && numFilled < numGlasses) {
            currGlassFilled = false;
            ++numFilled;
            if (numFilled == numGlasses)
                Debug.Log("Done");
                // Stop game
            else
                GetNextGlass();
        }


        // ===== For testing =====
        if (Input.GetKeyDown(KeyCode.S)) {
            StartGame();
        } else if (Input.GetKeyDown(KeyCode.N)) {
            ++numFilled;
            GetNextGlass();
        }
            
    }

    public void StartGame() {
        GameObject[] temp = new GameObject[numGlasses];
        temp[numFilled] = Instantiate(wineGlassPrefab, transform.position + new Vector3(-0.35f, 0.35f, 0f), Quaternion.identity);
        int offset = (int)Mathf.Ceil((numGlasses - 2f) / 2f);
        for (int i = 1; i < numGlasses; ++i) {
            temp[i] = Instantiate(wineGlassPrefab, transform.position + new Vector3(0f, 0.35f, 0.2f * (offset - i + 1)), Quaternion.identity);
        }
        wineGlasses = temp;
        currGlass = temp[numFilled];
    }

    public void SignalController() { // called by DetectParticles.cs
        Debug.Log("Signalled");
        currGlassFilled = true;
    }

    private void GetNextGlass() {
        GameObject temp = currGlass;
        currGlass = wineGlasses[numFilled];
        currGlass.transform.position = temp.transform.position;
        Destroy(temp);
    }
}
