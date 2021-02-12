using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeuCtrl : MonoBehaviour
{
    PersonnageCtrl persoCtrl;

    private bool isFiring = false;
    private bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        persoCtrl = GameObject.FindWithTag("Player").GetComponent<PersonnageCtrl>();
        RaycastUtil.DebugMode = true;
        RaycastUtil.VerboseMode = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            persoCtrl.Avancer();
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            persoCtrl.Reculer();
        }
    }

    void Update()
    {
        if (Input.GetAxisRaw("Fire1") != 0)
        {
            // Key est enfoncé
            if (!isFiring)
            {
                // Key vient d'être appuyé
                persoCtrl.Attaquer();
                isFiring = true;
            }
        }
        else
        {
            // Key est relaché
            isFiring = false;
        }

        if (Input.GetAxisRaw("Jump") != 0)
        {
            if (!isJumping)
            {
                persoCtrl.SauterDebut();
                isJumping = true;
            }
            persoCtrl.Sauter();
        }
        else
        {
            persoCtrl.SauterFin();
            isJumping = false;
        }
    }
}
