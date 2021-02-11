using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderValueToText : MonoBehaviour
{
    public Slider sliderUI;
    private Text textSliderValue;  // text objects value
    private string initialText;

    void Start()
    {
        textSliderValue = GetComponent<Text>();  // need this line to interact with text
        initialText = textSliderValue.text;
        ShowSliderValue();
    }

    public void ShowSliderValue()
    {
        // have to use player to interact with slider rather than in code to 
        // see the change ... common bug?
        // e.g. initalText = "Glasses: ", sliderUI.value = 1
        textSliderValue.text = initialText + " " + sliderUI.value;
    }
}