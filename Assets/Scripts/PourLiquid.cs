using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourLiquid : MonoBehaviour {
    ParticleSystem part;
    private int wineScore;
    public float totalTimeOut = 0f;  // total time liquid is poured
    private bool isPouring = false;
    Vector3 vv;
    Rigidbody rb;

    void Start() {
        part = GetComponent<ParticleSystem>();
        part.transform.rotation = this.transform.rotation;

        part.Stop();

        //vv = new Vector3(0f, 100f, 0f);
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    rb.AddForce(vv);
        //}
        if (Vector3.Dot(transform.up, Vector3.down) > 0) {
            isPouring = true;
            totalTimeOut += Time.deltaTime;
        } else {
            isPouring = false;
        }
        Pour(isPouring);
    }
    void FixedUpdate() {
        ParticleSystem.Particle[] particleList = new ParticleSystem.Particle[part.particleCount];
        int length = part.GetParticles(particleList);
    }
    void Pour(bool isPouring) {
        if (isPouring) {
            part.Play();
        } else {
            part.Stop();
        }
    }
    public float GetTimeOut()
    {
        return totalTimeOut;
    }
}
