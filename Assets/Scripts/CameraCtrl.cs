using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [SerializeField] private float vitesse = 1.0f;
    
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
        float distance = posCamera.x - posPerso.x;
        
        rb.velocity = new Vector2(-distance * vitesse, 0);
    }
}
