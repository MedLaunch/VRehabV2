using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Launch : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private GameObject clone;
    private Rigidbody projectile;
    public Transform spawn;
    public GameObject horseshoe;
    float interval = 0f;
    float scoreInterval = 0f;
    float step = 1;
    float stepScoreSet = 2.5f;
    float stepScoreInit = 0;
    private float x = 0f, y = 10f, z = 10f;
    Vector3 launch;
    Vector3 torque;

    // Transforms to act as start and end markers for the journey.
    public Transform startMarker;
    public Transform endMarker;

    // Movement speed in units per second.
    public float speed = 1.25F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;
    private bool lerp = false;

    public int score = 0;

    void Start()
    {
        projectile = horseshoe.GetComponent<Rigidbody>();

        launch = new Vector3(x, y, z);
        torque = new Vector3(0, 0.7f, 0);

        // Keep a note of the time the movement started.
        startTime = Time.time;
        scoreText.gameObject.SetActive(true);
        SetScoreText();

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        if (!lerp)
        {
            if (Input.GetKey("space"))
            {
                projectile.AddForce(launch);
                projectile.AddTorque(torque);
            }
            if (Input.GetKey("r"))
            {
                if (Time.time - interval > step)
                //if (true)
                {
                    interval = Time.time;
                    clone = Instantiate(horseshoe, spawn.position, spawn.rotation);
                    projectile = clone.GetComponent<Rigidbody>();
                
                }
            }
            if (Input.GetKey("t"))
            {
                lerp = true;
            }
        }
        if (lerp)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney);
            if(fractionOfJourney == 1)
            {
                lerp = false;
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        //if (collision.collider.tag == "Target")
        //{
            if (Time.time - scoreInterval > stepScoreInit)
            {
                if(stepScoreInit == 0) { stepScoreInit = stepScoreSet; }
                score++;
                SetScoreText();
                scoreInterval = Time.time;
            }
        //}
    }
}
