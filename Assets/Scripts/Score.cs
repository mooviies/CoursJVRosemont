using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private UiCtrl _uiCtrl;

    private int _coins;
    public int Coins
    {
        get => _coins;
        set
        {
            if (value < 0) return;

            _coins = value;
            _uiCtrl.Coins = value;
        }
    }
    
    private int stars;
    public int Stars
    {
        get => stars;
        set
        {
            if (value < 0) return;

            stars = value;
            _uiCtrl.Stars = value;
        }
    }

    void Start()
    {
        _uiCtrl = GameObject.FindWithTag("UI").GetComponent<UiCtrl>();
    }
}
