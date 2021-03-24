using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Launch : MonoBehaviour
{
    private bool notTouched = true;
    public Text scoreText;  // Display score
    private GameObject clone;  // for spawning
    private Rigidbody projectile;  // rigidbody of clone
    public Transform spawn;
    public GameObject Horseshoe;  // prefab
    float interval = 0f;
    float scoreInterval = 0f;
    float smallScoreInterval = 0f;
    float step = 1.0f;  // Time before next respawn of horseshoe
    float stepScoreSet = 1.5f;  // Time before next score is counted
    float stepScoreInit = 0;
    float stepSmallScoreSet = 1.5f;  // Time for getting near target
    float stepSmallScoreInit = 0;
    private float x = -200f, y = 200f, z = 0;
    Vector3 launch;
    Vector3 torque;
    bool firstSpawn = false;

    AudioSource pointSound;

    // Transforms to act as start and end markers for the journey.
    //public Transform startMarker;
    //public Transform endMarker;
    public float compareDistance = 0f;

    // Movement speed in units per second.
    public float speed = 1.25F;

    // Time when the movement started.
    //private float startTime;

    // Total distance between the markers.
    //private float journeyLength;
    //private bool lerp = false;

    public float score = 0f;
    bool newClone = true;  // Checks if object is a new clone for smallScore counting once
    void Start()
    {
        projectile = Horseshoe.GetComponent<Rigidbody>();

        launch = new Vector3(x, y, z);
        torque = new Vector3(0, 0.7f, 0);

        //Hide original gameObject
        Horseshoe.SetActive(false);
        //Start game with clones
        clone = Instantiate(Horseshoe, spawn.position, spawn.rotation);  // Spawns gameObject
        clone.SetActive(true);  // clone spawns false, set to true each time
        newClone = true;
        projectile = clone.GetComponent<Rigidbody>();  // Gets rigidbody of new gameObject clone
        // Keep a note of the time the movement started.
        //startTime = Time.time;
        scoreText.gameObject.SetActive(true);
        SetScoreText();
        firstSpawn = true;
        pointSound = GetComponent<AudioSource>();

        // Calculate the journey length.
        //journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    //Update is called once per frame
    void Update()
    {
        //if (!lerp)
        //{
        // Launches projectile
        if (Input.GetKeyDown(KeyCode.Space))
        {
            projectile.AddForce(launch);
            projectile.AddTorque(torque);
        }
        //Creates new projectile for launching
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Interval makes one click = one action rather than length of click in milliseconds
            if (Time.time - interval > step) // Current time - Time of last action > spacing time                 
            {
                if (notTouched)
                {
                    if ((projectile.transform.position - transform.position).sqrMagnitude < (compareDistance))
                    {
                        score += 1.0f;
                        SetScoreText();
                    }
                }
                notTouched = true;
                newClone = false;
                interval = Time.time;
                clone = Instantiate(Horseshoe, spawn.position, spawn.rotation);  // Spawns gameObject
                clone.SetActive(true); // set gameobject true
                newClone = true;
                projectile = clone.GetComponent<Rigidbody>();  // Gets rigidbody of new gameObject clone
            }
            firstSpawn = false;
        }
        if (Input.GetKeyDown(KeyCode.F))  // Delete object
        {
            // Interval makes one click = one action rather than length of click in milliseconds
            if (Time.time - interval > step) // Current time - Time of last action > spacing time                 
            {
                if (notTouched)
                {
                    if ((projectile.transform.position - transform.position).sqrMagnitude < (compareDistance))
                    {
                        score += 1.0f;
                        SetScoreText();
                    }
                }
                notTouched = true;
                newClone = false;
                Destroy(clone);
                interval = Time.time;
                clone = Instantiate(Horseshoe, spawn.position, spawn.rotation);  // Spawns gameObject
                clone.SetActive(true); // set gameobject true
                newClone = true;
                projectile = clone.GetComponent<Rigidbody>();  // Gets rigidbody of new gameObject clone
            }
            firstSpawn = false;
        }

       /* if (newClone && !firstSpawn)
        {
            if (notTouched)
            {
                if ((projectile.transform.position - transform.position).sqrMagnitude < (compareDistance))
                {
                    float dist = (projectile.transform.position - transform.position).sqrMagnitude;
                    Debug.Log(dist.ToString());
                    score += 1.0f;
                    SetScoreText();
                }
            }
            notTouched = true;
            newClone = false;
        }*/
        //if (Input.GetKey("t"))
        //{
        //    lerp = true;
        //}
        //}
        //if (lerp)
        //{
        //    // Distance moved equals elapsed time times speed..
        //    float distCovered = (Time.time - startTime) * speed;

        //    // Fraction of journey completed equals current distance divided by total distance.
        //    float fractionOfJourney = distCovered / journeyLength;

        //    // Set our position as a fraction of the distance between the markers.
        //    transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney);
        //    if(fractionOfJourney == 1)
        //    {
        //        lerp = false;
        //    }
        //}
    }
    void OnCollisionEnter(Collision collision)
    {
        //pointSound.PlayOneShot(pointSound.clip);

        if (Time.time - scoreInterval > stepScoreInit) { // Stops update from overcounting
            if (collision.gameObject.name == "Horseshoe(Clone)")
            {
                // Start step at 0 initially or else a quick score will not register 
                // (waits stepScoreSet seconds before allowing score to register)
                if (stepScoreInit == 0) { stepScoreInit = stepScoreSet; }
                score += 3.0f;
                SetScoreText();
                scoreInterval = Time.time;
                notTouched = false;
            }
        }
    }
}
