using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private string type;
    [SerializeField] private int value;

    public string Type => type;
    public int Value => value;

    private Score _score;

    void Start()
    {
        _score = GameObject.FindWithTag("Score").GetComponent<Score>();
    }

    public void Collect()
    {
        switch (type)
        {
            case "coin":
                _score.Coins += value;
                break;
            case "star":
                _score.Stars += value;
                break;
        }
        
        Destroy(gameObject);
    }
}
