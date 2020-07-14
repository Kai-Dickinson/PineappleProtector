using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunScript : MonoBehaviour
{
    public Slider runSlider;
    // Start is called before the first frame update
    void Start()
    {
        runSlider = GetComponent<Slider>();
    }

    public void SetMaxRun(float time)
    {
        runSlider.maxValue = time;
        runSlider.value = time;
    }

    public void IncreaseTime(float time)
    {
        runSlider.value += time;
    }

    public void DecreaseTime(float time)
    {
        runSlider.value -= time;
    }
}
