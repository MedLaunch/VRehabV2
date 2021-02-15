using System.Globalization;
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

    // Start is called before the first frame update
    public void Start() {
        outlineRender = outline.GetComponent<SpriteRenderer>();
        objectQueue = GameObject.Find("Foods").GetComponent<ObjectQueue>();
        yHeight = outline.transform.position.y;
        RandomizeOutlinePosition();
        outline.SetActive(false);
        sound = GetComponent<AudioSource>();
    }
    
    public void AddFood(){
        numFood += 1;
        objectQueue.AddObject();
    }

    public void RemoveFood(){
        if(objectQueue.GetCount() > 1){
            numFood -= 1;
            currentFood = objectQueue.GetNextObject();
            if(!currentFood.activeSelf){
                currentFood.SetActive(false);
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
        if (other.CompareTag("Food") && !placed) {
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
            distance = 0.7f;
        } else if(difficulty == 1){
            distance = 0.5f;
        } else if(difficulty == 2){
            distance = 0.3f;
        } else {
            distance = 0.1f;
        }
        if (dist < distance){
            ScorePoint(dist);
        }
    }

    public void SetDifficulty(int level){
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
        sound.PlayOneShot(sound.clip); 
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
    void RandomizeOutlinePosition() {
        float x = Random.Range(transform.position.x - xMax, transform.position.x + xMax);
        float z = Random.Range(transform.position.z - zMax, transform.position.z + zMax);
        outline.transform.position = new Vector3(x, yHeight + 0.01f, z);
        outline.SetActive(true);
        currentOutlineBounds = outline.GetComponent<Collider>();
    }

    public void TestFood(){
        if(currentFood != null){
            Vector3 up = new Vector3(0f,1f,0f);
            Vector3 left = new Vector3(0.2f, 0f, 0f);
            currentFood.transform.position = outline.transform.position + up + left;
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