using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectParticles : MonoBehaviour {
    public float timeInterval = 1f;

    private GameObject[] liquids;
    private int idx = 0;
    private float timeFilled = 0f;
    private float timer = 0f;
    private WineController wineController;
    private bool signaled = false;

    // Start is called before the first frame update
    void Start() {
        wineController = GameObject.Find("Pouring Wine").GetComponent<WineController>();
        liquids = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; ++i) {
            liquids[i] = transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnParticleCollision(GameObject other) {
        if (signaled)
            return;

        if (idx >= transform.childCount) {
            wineController.SignalController();
            signaled = true;
            return;
        }

        timeFilled += Time.deltaTime;
        timer += Time.deltaTime;
        if (timer > timeInterval) {
            if (idx != 0)
                liquids[idx - 1].SetActive(false);
            liquids[idx++].SetActive(true);
            timer = 0;
        }
    }

    public float GetTimeIn() {
        return timeFilled;
    }
}
