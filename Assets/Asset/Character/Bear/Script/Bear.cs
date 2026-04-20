using UnityEngine;
using UnityEngine.InputSystem;

public class Bear : Entity
{
    float movex;

    public GameObject bear;
    float directionX;

    Rigidbody2D rb;
    float speed = 3f;

    Animator animator;

    DetectionSolDroite detectionSolDroite;
    DetectionSolGauche detectionSolGauche;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
        rb = bear.GetComponent<Rigidbody2D>();
        animator = bear.GetComponentInChildren<Animator>();
        detectionSolDroite = bear.GetComponentInChildren<DetectionSolDroite>();
        detectionSolGauche = bear.GetComponentInChildren<DetectionSolGauche>();
        maxHealth = 1;
    }

    void FixedUpdate()
    {
        float directionX = movex;

        // Bloquer vers la droite si pas de sol à droite
        if (movex > 0 && !detectionSolDroite.estAuSolDroite)
        {
            directionX = 0;
        }

        // Bloquer vers la gauche si pas de sol à gauche
        if (movex < 0 && !detectionSolGauche.estAuSolGauche)
        {
            directionX = 0;
        }

        if (IsInWater)
        {
            rb.gravityScale = 2f;
            rb.linearVelocity = new Vector2(0, 0);
        }
        else
        {
            rb.gravityScale = 1f;
            rb.linearVelocity = new Vector2(directionX * speed, rb.linearVelocity.y);
        }


    }
    // Update is called once per frame
    void Update()
    {
        GererAnimation();
    }

    void OnMove(InputValue value)
    {
        movex = value.Get<Vector2>().x;
    }

    void GererAnimation()
    {
        if (!Hurt)
        {
            if (movex > 0)
            {
                GetComponentInChildren<SpriteRenderer>().flipX = false;
            }
            else if (movex < 0)
            {
                GetComponentInChildren<SpriteRenderer>().flipX = true;
            }

            animator.SetBool("IsWalking", movex != 0);
        }
        else
        {
            animator.SetTrigger("Hurt");
            Hurt = false; // réinitialise l'état de blessure après avoir déclenché l'animation
        }

    }

    void OnAttack(InputValue value)
    {
        if (value.isPressed)
        {
            animator.SetTrigger("Attack");
        }
    }


}


