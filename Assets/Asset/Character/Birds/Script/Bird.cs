using UnityEngine;
using UnityEngine.InputSystem;

public class Bird : Entity
{
    public GameObject bird;

    private Rigidbody2D rb;

    Animator anim;
    float movey;

    public float speed = 3;

    bool IsFlying;

    DetectionSol detectionSol;

    SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = bird.GetComponent<Rigidbody2D>();
        anim = bird.GetComponentInChildren<Animator>();
        detectionSol = bird.GetComponentInChildren<DetectionSol>();
        sr = bird.GetComponentInChildren<SpriteRenderer>();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInWater)
        {
            rb.gravityScale = 1f;
            rb.linearVelocity = new Vector2(0, 0);
        }
        else
        {
            rb.linearVelocity = new Vector2(movex * speed, movey * speed);
        }

        GererAnimation();
    }

    void OnMove(InputValue inputValue)
    {
        Vector2 d = inputValue.Get<Vector2>();
        movex = d.x;
        movey = d.y;
    }

    void GererAnimation()
    {
        if (!Hurt)
        {


            if (movey > 0.1 || !detectionSol.ToucheLeSol)
            {
                IsFlying = true;
            }

            if (detectionSol.ToucheLeSol)
            {
                IsFlying = false;
            }

            anim.SetBool("IsWalking", (movex > 0.1 || movex < -0.1) && IsFlying == false && !IsInWater);
            anim.SetBool("IsFlying", IsFlying && !IsInWater);



            if (movex > 0.1f)
            {
                sr.flipX = true;   // va à droite
            }
            else if (movex < -0.1f)
            {
                sr.flipX = false;  // va à gauche (par défaut)
            }
        }
        else
        {
            anim.SetTrigger("Hurt");
            Hurt = false; // réinitialise l'état de blessure après avoir déclenché l'animation
        }



    }




}
