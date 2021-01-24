using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Text timer;

    public void Play() {
        timer.text += "Button pressed";
    }
}
