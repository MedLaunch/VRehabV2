﻿using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.InvalidOperationException;
using System.Threading.Tasks;

public class TableController : MonoBehaviour {

    AudioSource sound;

    private int difficulty;

    public GameObject toast;
    public float xMax;
    public float zMax;
    public GameObject outline;

    public Text pointText;

    public ObjectQueue objectQueue;
    private Collider currentOutlineBounds;

    private SpriteRenderer outlineRender;

    private GameObject currentFood;

    private int score;
    private bool gameOver = false;
    private bool inScore = false;
    private bool timerIsRunning = false;

    bool placed = false;

    private float yHeight;

    private int numFood = 6;

    private float originalX = 0f;

    private int rows = 5;

    private float distance = 0.7f;
    // Start is called before the first frame update
    public void Start() {
        outlineRender = outline.GetComponent<SpriteRenderer>();
        objectQueue = GameObject.Find("Foods").GetComponent<ObjectQueue>();
        yHeight = outline.transform.position.y;
        // RandomizeOutlinePosition();
        originalX = outline.transform.position.x;
        outline.SetActive(false);
        sound = GetComponent<AudioSource>();
    }
    
    public void AddFood(){
        numFood += 1;
        objectQueue.AddObject();
        if(numFood >= 8){
            distance = 0.5f;
            rows = 6;
        } else {
            distance = 0.6f;
        }
    }

    public void RemoveFood(){
        if(objectQueue.GetCount() > 1){
            numFood -= 1;
            // the first current food is the plate, second current food is the actual food object
            currentFood = objectQueue.GetNextObject();
            currentFood.SetActive(false);
            currentFood = objectQueue.GetNextObject();
            if(!currentFood.activeSelf){
                currentFood.SetActive(false);
            }
            if(numFood < 8){
                distance = 0.6f;
                rows = 5;
            }
        }
    }

    public int GetNumFood(){
        return numFood;
    }
    public void ToggleOutlineVisibility(bool visible){
        outline.SetActive(visible);
    }
    void Update() {
        if(timerIsRunning){
            if (currentFood != null) {
                Vector3 modifiedPosition = currentFood.transform.position;
                modifiedPosition.y = gameObject.transform.position.y + 0.1f;
                if (inScore) {
                    StartCoroutine(FadeOut());
                }
            }
        }     
    }
    
    public void SetGameStatus(bool status){
        timerIsRunning = status;
    }
    
    public bool GetGameStatus(){
        return timerIsRunning;
    }

    public bool GetGameOver(){
        return gameOver;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Food")) {
            CheckPosition(other.gameObject);
            placed = true;
        }
            
    }

    private void CheckPosition(GameObject other) {
        Vector2 outlinePos = new Vector2(outline.transform.position.x, outline.transform.position.z);
        Vector2 objectPos = new Vector2(other.transform.position.x, other.transform.position.z);
        float dist = Vector2.Distance(outlinePos, objectPos);
        float distance = 0.5f;
        if(difficulty == 0){
            distance = 0.08f;
        } else if(difficulty == 1){
            distance = 0.04f;
        } else if(difficulty == 2){
            distance = 0.02f;
        } else {
            distance = 0.01f;
        }
        if (dist < distance){
            ScorePoint(dist);
        }
    }

    public void SetDifficulty(int level){
        if (level < difficulty)
            outline.transform.localScale += new Vector3(0.1f, 0.2f, 0);
        if (level > difficulty)
            outline.transform.localScale -= new Vector3(0.1f, 0.2f, 0);
        difficulty = level;
    }
    void ScorePoint(float distance) {
        score += (int)((1 / distance) * 100);
        pointText.text = "Points: " + score.ToString();
        inScore = true;
        outlineRender.color = Color.green;

        GameObject temp = currentFood;
        currentFood = objectQueue.GetNextObject();
        Debug.Log(currentFood);
        if (currentFood != null) {
            StartCoroutine(Create(temp));
        } else {
            temp.SetActive(false);
            Destroy(temp);
            timerIsRunning = false;
            gameOver = true;
            inScore = false;
        }
    }

    IEnumerator Create(GameObject temp){
        yield return new WaitForSeconds(0.2f);
        temp.SetActive(false);
        Destroy(temp);
        outlineRender.color = Color.white;
        RandomizeOutlinePosition();
        inScore = false;
        placed = false;
    }

    IEnumerator FadeOut(){
        //sound.Play();
        //sound.PlayOneShot(sound.clip); 
        while (outlineRender.color.a > 0) {
            Color outColor = outlineRender.color;
            float fadeAmount = outColor.a - (0.05f * Time.deltaTime);
            outColor = new Color(outColor.r, outColor.g, outColor.b, fadeAmount);
            outlineRender.color = outColor;
            yield return null;
        }
        //sound.Stop();
    }

    public void SetCurrentFood(GameObject food) {
        currentFood = food;
    }
    public void SetCurrentFood(){
        currentFood = objectQueue.GetNextObject();
    }

    // TODO: change name of function (TranslateOutlinePosition)
    public void RandomizeOutlinePosition() {
        //float x = Random.Range(transform.position.x - xMax, transform.position.x + xMax);
        // float z = Random.Range(transform.position.z - zMax, transform.position.z + zMax);
        // if the num food index % rows == 1, then it's a start of a new row
        int numItems = objectQueue.GetCount();
        float x = outline.transform.position.x + distance;
        float z = outline.transform.position.z;
        Debug.Log("num items, no plate index");
        Debug.Log(numItems);
        if(numItems % 2 != 0){
            // plate is at top of queue, set outline on plate
            int noPlateIndex = Mathf.Abs(numItems / 2  - numFood);
            Debug.Log(noPlateIndex % rows);
            if(noPlateIndex % rows == 0 && noPlateIndex > 0){
                // start of a new row
                z = outline.transform.position.z - .4f;
                x = originalX;
            }
        } else {
            // stuff to do when food is being placed instead
        }
        outline.transform.position = new Vector3(x, yHeight + 0.01f, z);
        outline.SetActive(true);
        currentOutlineBounds = outline.GetComponent<Collider>();
    }

    public void TestFood(){
        if(currentFood != null){
            currentFood.SetActive(true);
            Vector3 up = new Vector3(0f,1f,0f);
            Vector3 left = new Vector3(0.022f, 0f, 0f);
            if(currentFood.tag == "Plate"){
                currentFood.transform.position = outline.transform.position + new Vector3(0f, 1.25f, 0f) + left;
            } else {
                currentFood.transform.position = outline.transform.position + up + left;
            }
            currentFood = objectQueue.GetNextObject();
            if(objectQueue.GetCount() % 2 != 0){
                RandomizeOutlinePosition();
            }
        }
    }

    public void ResetGame(){
        Debug.Log("Resetting game");
        objectQueue.ResetQueue();
        Debug.Log(currentFood);
        if(currentFood != null){
            currentFood.SetActive(false);
        }
        outline.SetActive(false);
    }
}