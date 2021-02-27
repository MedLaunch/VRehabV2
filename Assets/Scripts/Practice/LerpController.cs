using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpController : MonoBehaviour {
    public GameObject ball;
    public float duration = 3f;
    public Vector3 startPos, endPos;
    public AnimationCurve curve;

    private bool moving = false;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.M)) {
            MoveBall();
        } else if (Input.GetKeyDown(KeyCode.U)) {
            MoveBallUnclamped();
        }

    }

    private void MoveBall() {
        if (!moving) {
            StartCoroutine(_MoveBall());
        }
    }

    IEnumerator _MoveBall() {
        moving = true;
        float initialTime = Time.time;
        float progress = 0;
        while (progress < 1f) {
            progress = (Time.time - initialTime) / duration;
            ball.transform.position = Vector3.Lerp(startPos, endPos, progress);
            yield return null;
        }
        Debug.Log("Done");
        moving = false;
    }

    private void MoveBallUnclamped() {
        if (!moving)
            StartCoroutine(_MoveBallUnclamped());
    }

    IEnumerator _MoveBallUnclamped() {
        moving = true;
        float initialTime = Time.time;
        float progress = 0;
        while (progress < 1f) {
            progress = (Time.time - initialTime) / duration;
            ball.transform.position = Vector3.LerpUnclamped(startPos, endPos, curve.Evaluate(progress));
            yield return null;
        }
        moving = false;
    }
}
