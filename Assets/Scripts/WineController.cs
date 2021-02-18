using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WineController : MonoBehaviour
{
    public int glassesLeft;
    public float totalTimeIn = 0f;
    private bool playClicked = false;  // determine if update() should be running
    public Slider sliderUI;  // get glassesLeft
    public Text displayGame;  // win or lose
    public Text glassesLeftText;  // show glassesLeft
    public GameObject glass;
    private GameObject clone;
    //private Rigidbody rb;  // rb of clone
    public Transform spawn;
    private IntakeLiquid script;  // script to GetFull()
    bool glassSpawned = false;
    bool lost = false;
    private GameObject pouringWine;

    void Start()
    {
        //displayGame = GetComponent<Text>();
        //glassLeftText = GetComponent<Text>();
        pouringWine = GameObject.Find("Pouring Wine");
        glass.SetActive(false);  // disable initial glass
        SetGlassesLeft();
        SetGlassesText();
    }
    public void StartWine()
    {
        playClicked = true;
    }
    private bool CheckLost()  // returns true if game is won or lost
    {
        if(/*glassesLeft != 0 && */pouringWine.GetComponent<Timer>().GetGameStatus()) { 
            displayGame.text = "You Lost!";
            playClicked = false;
            return true;
        }
        if (glassesLeft == 0 && !GetComponent<Timer>().GetGameStatus()) { 
            displayGame.text = "Congrats, you win!";
            pouringWine.GetComponent<Timer>().SetPaused(true);
            playClicked = false;
            return true;
        }
        return false;
    }
    // Update is called once per frame
    void SetGlassesText()
    {
        glassesLeftText.text = "Glasses Left: " + glassesLeft.ToString();
    }
    public void SetGlassesLeft()
    {
        glassesLeft = (int) sliderUI.value;
    }
    void Update()
    {
        if (playClicked)
        {
            if (CheckLost()) { break; }
            if (!glassSpawned)
            {
                clone = Instantiate(glass, spawn.position, spawn.rotation);  // Spawns gameObject
                clone.SetActive(true);
                //rb = clone.GetComponent<Rigidbody>();
                script = clone.GetComponent<IntakeLiquid>();
                glassSpawned = true;
            }
            else if (script.GetFull())
            {
                // tell glass amount before destroying
                totalTimeIn += script.GetTimeIn();
                Destroy(clone);
                glassSpawned = false;
                glassesLeft--;
                SetGlassesText();
            }

            // currently glassesLeft == 1 and last glass just spawned
            if (glassesLeft == 1 && !CheckLost())
            {
                IntakeLiquid script = clone.GetComponent<IntakeLiquid>();
                if (script.GetFull())
                {
                    glassesLeft--;
                    SetGlassesText();
                }
                // now glassesLeft == 0 and we won 
            }
        }
    }
}
