using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TableController : MonoBehaviour {
    public float speed = 0;
    public float remainingTime = 45;
    //public TextMeshProUGUI scoreText;
    //public TextMeshProUGUI timer;

    //public GameObject winTextObject;

    //public GameObject loseObject;
    public GameObject toast;
    public float xMax;
    public float zMax;
    public GameObject outline;

    ObjectQueue objectQueue;
    private Collider currentOutlineBounds;

    private SpriteRenderer outlineRender;
    Queue<GameObject> food = new Queue<GameObject>();

    private GameObject currentFood;
    // private Rigidbody rb;

    private float movementX;
    private float movementY;

    private int score;
    private bool gameOver = false;
    private bool inScore = false;
    private bool timerIsRunning = false;

    private float t = 0f;
    bool placed = false;

    // Start is called before the first frame update


    void Start() {
        // timer.text = "Time: " + remainingTime.ToString();
        outlineRender = outline.GetComponent<SpriteRenderer>();
        objectQueue = GameObject.Find("Foods").GetComponent<ObjectQueue>();
        // loseObject.SetActive(false);
        outline.SetActive(false);
        currentFood = objectQueue.GetNextObject();
        RandomizeOutlinePosition();
        // winTextObject.SetActive(false);
        timerIsRunning = true;
    }

    void Update() {
        if (currentFood != null) {
            Vector3 modifiedPosition = currentFood.transform.position;
            modifiedPosition.y = gameObject.transform.position.y + 0.1f;

            if (timerIsRunning) {
                if (remainingTime > 0) {
                    remainingTime -= Time.deltaTime;
                    if (remainingTime < 10) {
                        // timer.color = Color.red;
                    }
                }
                else {
                    timerIsRunning = false;
                    remainingTime = 0;
                    gameOver = true;
                    // loseObject.SetActive(true);
                    inScore = false;
                    Debug.Log("here");
                }

                // timer.text = "Time: " + Mathf.FloorToInt(remainingTime).ToString();
            }

            if (inScore) {
                StartCoroutine(FadeOut());
            }
        }
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Food") && !placed) {
            ScorePoint();
            placed = true;
        }
            
    }

    void ScorePoint() {
        score += 1;
        inScore = true;
        // scoreText.text = "Items Placed: " + score.ToString();
        outlineRender.color = Color.green;

        GameObject temp = currentFood;
        currentFood = objectQueue.GetNextObject();
        if (currentFood != null) {
            // remove food item for next one
            StartCoroutine(Create(temp));
        } else {
            // winTextObject.SetActive(true);
            timerIsRunning = false;
            // timer.color = Color.green;
            gameOver = true;
            inScore = false;
        }
    }

    IEnumerator Create(GameObject temp) {
        yield return new WaitForSeconds(1);
        Destroy(temp);
        outlineRender.color = Color.white;
        RandomizeOutlinePosition();
        t = 0;
        inScore = false;
        placed = false;
    }

    IEnumerator FadeOut() {
        while (outlineRender.color.a > 0) {
            Color outColor = outlineRender.color;
            float fadeAmount = outColor.a - (0.05f * Time.deltaTime);
            outColor = new Color(outColor.r, outColor.g, outColor.b, fadeAmount);
            outlineRender.color = outColor;
            yield return null;
        }
    }

    public void SetCurrentFood(GameObject food) {
        currentFood = food;
    }

    void RandomizeOutlinePosition() {
        float x = Random.Range(transform.position.x - xMax, transform.position.x + xMax);
        float z = Random.Range(transform.position.z - zMax, transform.position.z + zMax);
        outline.transform.position = new Vector3(x, transform.position.y + 0.01f, z);
        outline.SetActive(true);
        currentOutlineBounds = outline.GetComponent<Collider>();
        //if (currentFood != null) {
        //    Vector3 up = new Vector3(0f, 1f, 0f);
        //    currentFood.transform.position = outline.transform.position + up;
        //}
    }
}