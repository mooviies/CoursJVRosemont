using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe représentant le personnage du jeu
///
/// Contient toutes les informations sur l'état duy personnage (sa vie, sa vitesse, etc)
/// Permet le contrôle du personnage via les différentes méthodes (sauter, marcher, courir , attaquer)
/// </summary>
public class PersonnageCtrl : MonoBehaviour
{
    [SerializeField] private float vitesse = 2f;

    [SerializeField] private float vitesseSautInitiale = 5f;

    [SerializeField] private float amortiSaut = 0.1f;

    [SerializeField] private LayerMask layerSol;

    [SerializeField] private int maxHealth = 100;

    [SerializeField] private float tempInvincibilite = 2;

    private Rigidbody2D _rb;
    private Animator _anim;
    private CapsuleCollider2D _collider;
    private UiCtrl _uiCtrl;

    private bool _regarderDroite = true;
    private bool _isJumping = false;

    private float _vitesseSaut;

    private int health;

    private bool _estInvincible = false;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider2D>();
        _uiCtrl = GameObject.FindWithTag("UI").GetComponent<UiCtrl>();
        health = maxHealth;

        _uiCtrl.MinHealth = 0;
        _uiCtrl.MaxHealth = maxHealth;
        _uiCtrl.Health = maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _anim.SetFloat("deplacement", Mathf.Abs(_rb.velocity.x));

        if (RaycastUtil.DebugMode)
        {
            EstSurLeSol();
            ToucheMurDroite();
            ToucheMurGauche();
        }
    }

    public void RecevoirDegat(int degats)
    {
        if (_estInvincible) return;
        
        health -= degats;
        _uiCtrl.Health = health;
        _estInvincible = true;
        StartCoroutine(RendreVulnerable());
    }

    /// <summary>
    /// Déplace le personnage vers la droite
    /// </summary>
    public void Avancer ()
    {
        if (ToucheMurDroite())
            return;
        
        _rb.velocity = new Vector2(vitesse, _rb.velocity.y);
        if (!_regarderDroite)
        {
            _regarderDroite = true;
            Retourner();
        }
    }

    public void Reculer ()
    {
        if (ToucheMurGauche())
            return;
        
        _rb.velocity = new Vector2(-vitesse, _rb.velocity.y);

        if (_regarderDroite)
        {
            _regarderDroite = false;
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
        _anim.SetTrigger("attaque");
    }

    public void SauterDebut()
    {
        if (!_isJumping && EstSurLeSol())
        {
            _isJumping = true;
            _vitesseSaut = vitesseSautInitiale;
        }
    }

    public void Sauter()
    {
        if (_isJumping)
        {
            _rb.velocity += Vector2.up * _vitesseSaut;
            _vitesseSaut -= amortiSaut;
            if (_vitesseSaut < 0)
            {
                _vitesseSaut = 0;
                _isJumping = false;
            }
        }
    }

    public void SauterFin()
    {
        _isJumping = false;
    }

    private IEnumerator RendreVulnerable()
    {
        yield return new WaitForSeconds(tempInvincibilite);

        _estInvincible = false;
    }

    private bool EstSurLeSol()
    {
        Bounds bounds = _collider.bounds;
        return RaycastUtil.TesterCollision2D(bounds.center, Vector2.down, bounds.extents.y, layerSol);
    }

    private bool ToucheMurGauche()
    {
        Bounds bounds = _collider.bounds;
        return RaycastUtil.TesterCollision2D(bounds.center, Vector2.left, bounds.extents.x, layerSol);
    }
    
    private bool ToucheMurDroite()
    {
        Bounds bounds = _collider.bounds;
        return RaycastUtil.TesterCollision2D(bounds.center, Vector2.right, bounds.extents.x, layerSol);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Collectible collectible = other.GetComponent<Collectible>();
        if (collectible)
        {
            collectible.Collect();
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyCtrl enemyCtrl = other.gameObject.GetComponent<EnemyCtrl>();
        if (enemyCtrl)
        {
            RecevoirDegat(10);
        }
    }
}
