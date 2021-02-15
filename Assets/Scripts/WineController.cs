using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WineController : MonoBehaviour
{
    public int glassesLeft;
    public Text displayGame;
    public GameObject glass;
    private GameObject clone;
    public Transform spawn;
    bool glassSpawned = false;
    bool lost = false;
    private GameObject pouringWine;
    // Start is called before the first frame update
    void Start()
    {
        displayGame = GetComponent<Text>();
        pouringWine = GameObject.Find("Pouring Wine")
    }
    public void StartWine()
    {
        do // always spawn a glass
        {
            if (CheckLost()) { break; }
            if (!glassSpawned)
            {
                clone = Instantiate(glass, spawn.position, spawn.rotation);  // Spawns gameObject
                IntakeLiquid script = clone.GetComponent<IntakeLiquid>();
                glassSpawned = true;
            }
            if (glassSpawned && script.GetFull())
            {
                Destroy(clone);
                glassSpawned = false;
            }
        } while (glassesLeft > 1);
        // currently glassesLeft == 1 and last glass just spawned
        while (!CheckLost();)
        {
            IntakeLiquid script = clone.GetComponent<IntakeLiquid>();
            if (script.GetFull())
            {
                glassesLeft--;
            }
            // now glassesLeft == 0 and we won 
        }
    }
    private bool CheckLost()  // returns true if game is won or lost
    {
        if(glassesLeft != 0 && pouringWine.GetComponent<Timer>().GetGameStatus()) { 
            lost = true;
            displayGame.text = "You Lost!";
            return true;
        }
        if (glassesLeft == 0 && !GetComponent<Timer>().GetGameStatus()) { 
            lost = false;
            displayGame.text = "Congrats, you win!";
            pouringWine.GetComponent<Timer>().SetPaused(true);
            return true;
        }
        return false;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
