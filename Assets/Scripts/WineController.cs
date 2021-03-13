using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class WineController : MonoBehaviour {
    public GameObject wineGlassPrefab;
    public Vector3 spawnPos;
    public int numGlasses = 3; // Max: 10

    private GameObject[] wineGlasses;
    private GameObject currGlass;
    private bool currGlassFilled = false;  // changed by SignalController 
    /// </summary>
    private int numFilled = 0;
    public float totalTimeFilled = 0f;
    private DetectParticles detectParticles;
    public Text spilled;

    public float duration = 2f;
    public Vector3 startPos, endPos;
    private bool moving = false;
    private Vector3[] positions;
    public int glassCounter;
    public AnimationCurve curve;

    private Timer timerScript;
    public Slider glassesSlider;

    // Use this for initialization
    void Start() {
        endPos = transform.position + new Vector3(-0.35f, 0.38f, 0f);
        timerScript = gameObject.GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update() {
        if (currGlassFilled && numFilled < numGlasses) {
            currGlassFilled = false;
            ++numFilled;
            if (numFilled == numGlasses) {
                SwitchGlasses();
                UpdateTimeFilled();
                DisplaySpilled();
                timerScript.CompleteGame();
            }
            else {
                UpdateTimeFilled();
                SwitchGlasses();
            }
        }


        // ===== For testing =====
        if (Input.GetKeyDown(KeyCode.S)) {
            StartGame();
        } else if (Input.GetKeyDown(KeyCode.N)) {
            ++numFilled;
            //GetNextGlass();
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            SwitchGlasses();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            DisplaySpilled();
        }

    }

    public void StartGame() {
        timerScript.StartTimer();
        ResetVariables();
        GameObject[] temp = new GameObject[numGlasses];
        Vector3[] tempPos = new Vector3[numGlasses];
        //temp[numFilled] = Instantiate(wineGlassPrefab, transform.position + new Vector3(-0.35f, 0.35f, 0f), Quaternion.identity);
        int offset = (int)Mathf.Ceil((numGlasses - 2f) / 2f);
        for (int i = 0; i < numGlasses; ++i) {
            tempPos[i] = transform.position + new Vector3(0f, 0.38f, 0.2f * (offset - i)); // 0.35 --> 0.38
            temp[i] = Instantiate(wineGlassPrefab, tempPos[i], Quaternion.identity);
        }
        wineGlasses = temp;
        positions = tempPos;
        currGlass = temp[numFilled];
        SwitchGlasses();
    }

    public void SignalController() { // called by DetectParticles.cs
        currGlassFilled = true;
    }

    private void UpdateTimeFilled() {
        // gets first child (detect particles) of currGlass
        detectParticles = currGlass.transform.GetChild(0).gameObject.GetComponent<DetectParticles>();
        totalTimeFilled += detectParticles.GetTimeIn();
    }

    private void DisplaySpilled()
    {
        PourLiquid pourLiquid = GameObject.Find("Wine Bottle").GetComponent<PourLiquid>();
        float lout = Mathf.Round(pourLiquid.GetTimeOut() * 100.0f) * 0.01f;
        float lin = Mathf.Round(totalTimeFilled * 100.0f) * 0.01f;
        spilled.text = "Spilled: " + lin.ToString() + " / " + lout.ToString();
    }

    private void SwitchGlasses()
    {
        if (!moving)
        {
            StartCoroutine(_SwitchGlasses());
        }
    }
    IEnumerator _SwitchGlasses()
    {
        if (glassCounter != 0)
        {
            yield return _MoveGlassBack();
            GameObject temp = currGlass;
            if (numFilled < positions.Length) { currGlass = wineGlasses[numFilled]; } // avoid index out of bounds
            currGlass.transform.position = temp.transform.position;
        }
        if (glassCounter != positions.Length)
        {
            yield return _MoveNextGlass();
        }
        glassCounter++;
    }
    IEnumerator _MoveNextGlass()
    {
        startPos = positions[glassCounter];
        moving = true;
        float initialTime = Time.time;
        float progress = 0;
        while (progress < 1f)
        {
            progress = (Time.time - initialTime) / duration;
            currGlass.transform.position = Vector3.LerpUnclamped(startPos, endPos, curve.Evaluate(progress));
            yield return null;
        }
        moving = false;
    }
    IEnumerator _MoveGlassBack()
    {
        // swaps endPos and startPos to move back
        startPos = positions[glassCounter - 1];
        moving = true;
        float initialTime = Time.time;
        float progress = 0;
        while (progress < 1f)
        {
            progress = (Time.time - initialTime) / duration;
            currGlass.transform.position = Vector3.LerpUnclamped(endPos, startPos, curve.Evaluate(progress));
            yield return null;
        }
        moving = false;
    }

    private void ResetVariables()
    {
        glassCounter = 0;
        numFilled = 0;
        currGlassFilled = false;
        totalTimeFilled = 0f;
   
        spilled.text = "Spilled: ";
        if (wineGlasses != null)
        {
            for (int i = 0; i < wineGlasses.Length; i++)
            {
                Destroy(wineGlasses[i]);
            }
        }
        
    }

    public void SetGlasses()
    {
        numGlasses = (int)glassesSlider.value;
    }
}
