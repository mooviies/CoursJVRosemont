using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonnageCtrl : MonoBehaviour
{
    [SerializeField] private float vitesse = 2f;

    [SerializeField] private float vitesseSautInitiale = 5f;

    [SerializeField] private float amortiSaut = 0.1f;

    [SerializeField] private LayerMask layerSol;

    private Rigidbody2D rb;
    private Animator anim;
    private CapsuleCollider2D collider;

    private bool regarderDroite = true;
    private bool isJumping = false;

    private float vitesseSaut;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        anim.SetFloat("deplacement", Mathf.Abs(rb.velocity.x));
    }

    public void Avancer ()
    {
        if (ToucheMurDroite())
            return;
        
        rb.velocity = new Vector2(vitesse, rb.velocity.y);
        if (!regarderDroite)
        {
            regarderDroite = true;
            Retourner();
        }
    }

    public void Reculer ()
    {
        if (ToucheMurGauche())
            return;
        
        rb.velocity = new Vector2(-vitesse, rb.velocity.y);

        if (regarderDroite)
        {
            regarderDroite = false;
            Retourner();
        }
    }

    public void Retourner ()
    {
        Vector2 scale = new Vector2(-transform.localScale.x, transform.localScale.y);

        transform.localScale = scale;
    }

    public void Attaquer()
    {
        anim.SetTrigger("attaque");
    }

    public void SauterDebut()
    {
        if (!isJumping && EstSurLeSol())
        {
            isJumping = true;
            vitesseSaut = vitesseSautInitiale;
        }
    }

    public void Sauter()
    {
        if (isJumping)
        {
            rb.velocity += Vector2.up * vitesseSaut;
            vitesseSaut -= amortiSaut;
            if (vitesseSaut < 0)
            {
                vitesseSaut = 0;
                isJumping = false;
            }
        }
    }

    public void SauterFin()
    {
        isJumping = false;
    }

    private bool EstSurLeSol()
    {
        float ajustement = 0.02f;
        RaycastHit2D raycastHit = Physics2D.Raycast(collider.bounds.center, Vector2.down, 
            collider.bounds.extents.y + ajustement, layerSol);

        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(collider.bounds.center, Vector2.down * (collider.bounds.extents.y + ajustement), rayColor);
        Debug.Log(raycastHit.collider);

        return raycastHit.collider != null;
    }

    private bool ToucheMurGauche()
    {
        float ajustement = 0.02f;
        RaycastHit2D raycastHit = Physics2D.Raycast(collider.bounds.center, Vector2.left, 
            collider.bounds.extents.y + ajustement, layerSol);

        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(collider.bounds.center, Vector2.left * (collider.bounds.extents.y + ajustement), rayColor);
        Debug.Log(raycastHit.collider);

        return raycastHit.collider != null;
    }
    
    private bool ToucheMurDroite()
    {
        float ajustement = 0.02f;
        RaycastHit2D raycastHit = Physics2D.Raycast(collider.bounds.center, Vector2.right, 
            collider.bounds.extents.y + ajustement, layerSol);

        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(collider.bounds.center, Vector2.right * (collider.bounds.extents.y + ajustement), rayColor);
        Debug.Log(raycastHit.collider);

        return raycastHit.collider != null;
    }
}
