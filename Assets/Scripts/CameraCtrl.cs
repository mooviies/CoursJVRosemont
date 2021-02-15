using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [SerializeField] private float vitesseX = 2.0f;
    [SerializeField] private float vitesseY = 2.0f;

    [SerializeField] private bool useMinX;
    [SerializeField] private float minX;

    [SerializeField] private bool useMaxX;
    [SerializeField] private float maxX;
    
    [SerializeField] private bool useMinY;
    [SerializeField] private float minY;

    [SerializeField] private bool useMaxY;
    [SerializeField] private float maxY;

    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;
    
    private PersonnageCtrl personnage;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        personnage = GameObject.FindWithTag("Player").GetComponent<PersonnageCtrl>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posPerso = personnage.transform.position;
        Vector3 posCamera = transform.position;
        float distanceX = (posPerso.x + offsetX) - posCamera.x;
        float distanceY = (posPerso.y + offsetY) - posCamera.y;
        
        rb.velocity = new Vector2(distanceX * vitesseX, distanceY * vitesseY);
        
        if (useMinX && posCamera.x < minX)
            posCamera.x = minX;
        else if (useMaxX && posCamera.x > maxX)
            posCamera.x = maxX;
        
        if (useMinY && posCamera.y < minY)
            posCamera.y = minY;
        else if (useMaxY && posCamera.y > maxY)
            posCamera.y = maxY;

        transform.position = posCamera;
    }
}
