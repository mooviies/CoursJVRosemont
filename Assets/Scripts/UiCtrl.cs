using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCtrl : MonoBehaviour
{
    [SerializeField] private Text coinsText;
    [SerializeField] private Text starsText;
    [SerializeField] private Slider healthSlider;

    public int Coins
    {
        set
        {
            if (value >= 0)
                coinsText.text = value.ToString();
        }
    }

    public int Stars
    {
        set
        {
            if (value >= 0)
                starsText.text = value.ToString();
        }
    }

    public int MinHealth
    {
        set
        {
            if (value >= 0)
                healthSlider.minValue = 0;
        }
    }
    
    public int MaxHealth
    {
        set => healthSlider.maxValue = 0;
    }
    
    public int Health
    {
        set => healthSlider.value = value;
    }

    void Start()
    {
        Coins = 0;
        Stars = 0;
        healthSlider.value = healthSlider.maxValue;
    }
}
