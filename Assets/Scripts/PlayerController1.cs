using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController1 : MonoBehaviour
{
    public float speed = 0;

    public float upForce = 100.0f;
    public float expF = 100.0f;

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    // private dont set in initialization
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        rb = GetComponent<Rigidbody>();
    }
    
    //void OnMove(InputValue movementValue)
    //{
    //    Vector2 movementVector = movementValue.Get<Vector2>();
    //    movementX = movementVector.x;
    //    movementY = movementVector.y;
    //}
    
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            count++;
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Spring"))
        {
            rb.AddExplosionForce(expF, other.transform.position, 10.0f, upForce, ForceMode.Impulse);
        }
    }
    
}
