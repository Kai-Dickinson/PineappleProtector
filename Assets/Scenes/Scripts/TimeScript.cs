using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour
{
    public Slider timeSlider;
    // Start is called before the first frame update
    void Start()
    {
        timeSlider = GetComponent<Slider>();
    }

    public void SetMaxTime(float time)
    {
        timeSlider.maxValue = time;
        timeSlider.value = time;
    }

    public void SetTime(float time)
    {
        timeSlider.value = time;
    }
}
