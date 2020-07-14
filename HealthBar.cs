using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider hpSlider;
    // Start is called before the first frame update
    void Start()
    {
        hpSlider = GetComponent<Slider>();
    }

    public void SetMaxHealth(float hp)
    {
        hpSlider.maxValue = hp;
        hpSlider.value = hp;
    }

    public void SetHealth(float hp)
    {
        hpSlider.value = hp;
    }
}
