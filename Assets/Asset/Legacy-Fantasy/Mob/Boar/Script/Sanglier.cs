using Unity.VisualScripting;
using UnityEngine;

public class Sanglier : Entity
{
    public enum State
    {
        Patrouille,
        Chase,
        Stunt,
    }
    //variable mouvement
    public float speed;

    public float chaseSpeed = 4f;

    public float walkSpeed = 2f;
    public State currentState;

    public float direction;

    Rigidbody2D rb;

    Animator anim;
    //Script Detection
    private DetectionSolDroite detectionDroite;
    private DetectionSolGauche detectionGauche;

    //SpriteRenderer spriteRenderer;

    SpriteRenderer sr;


    GameObject player;
    Entity entity;

    //compteur chase
    float chaseTime = 0f;
    float maxChaseTime = 3f;
    //compteur stunt
    float stuntTime = 0f;
    float maxStuntTime = 3f;

    DetecterPlayer detecterPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IsDead = false;
        rb = GetComponent<Rigidbody2D>();
        currentState = State.Patrouille;
        direction = 1f;
        detectionDroite = GetComponentInChildren<DetectionSolDroite>();
        detectionGauche = GetComponentInChildren<DetectionSolGauche>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        detecterPlayer = GetComponentInChildren<DetecterPlayer>();
        speed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (detecterPlayer.chasePlayer && currentState != State.Stunt)
        {
            currentState = State.Chase;
            speed = chaseSpeed;
        }
        //Debug.Log("Current State: " + currentState);
        switch (currentState)
        {
            case State.Patrouille:
                Patrouille();
                break;
            case State.Chase:
                Chase();
                break;
            case State.Stunt:
                Stunt();
                break;
        }
        GererAnimation();
    }

    void Patrouille()
    {
        verifierSiIlestAuSolOuEstBloqueParUnMur();
        Flip();
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
    }

    void verifierSiIlestAuSolOuEstBloqueParUnMur()
    {
        if (detectionDroite.estAuSolDroite == true && detectionGauche.estAuSolGauche == false)
        {

            direction = 1f;
        }
        else if (detectionDroite.estAuSolDroite == false && detectionGauche.estAuSolGauche == true)
        {

            direction = -1f;
        }
        if(detectionDroite.bloquer == true)
        {
            direction = -1f;
        }
        else if(detectionGauche.bloquer == true)
        {
            direction = 1f;
        }
    }

    void Flip()
    {
        if (direction == 1f)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

    void Chase()
    {
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
        Flip();

        if (chaseTime < maxChaseTime)
        {
            chaseTime += Time.deltaTime;
        }
        else
        {
            detecterPlayer.chasePlayer = false;
            currentState = State.Patrouille;
            speed = walkSpeed;
            chaseTime = 0f;
        }

    }

    void Stunt()
    {
        rb.linearVelocity = Vector2.zero;
        detecterPlayer.chasePlayer = false;
        if (stuntTime < maxStuntTime)
        {
            stuntTime += Time.deltaTime;
        }
        else
        {
            currentState = State.Patrouille;
            stuntTime = 0f;
        }
    }

    void GererAnimation()
    {
        if (!Hurt)
        {
            if (currentState == State.Patrouille)
            {
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
            }
            else if (currentState == State.Chase)
            {
                anim.SetBool("isRunning", true);
                anim.SetBool("isWalking", false);
            }
            else if (currentState == State.Stunt)
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
            }
        }
        else
        {
            anim.SetTrigger("Hurt");
            Hurt = false;
        }
    }

    //fonction pour savoir si il rentre en collision avec le joueur ou un mur
    //si cette un joueur il lui inflige des dégats et le repousse sinon il rentre en stunt
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision avec: " + collision.gameObject.name + " tag: " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Player" && currentState == State.Chase)
        {
            player = collision.gameObject;
            if (player.name == "Ours")
            {
                entity = player.GetComponentInParent<Bear>();

            }
            if (player.name == "Druide")
            {
                entity = player.GetComponentInParent<Player>();

            }
            if (player.name == "Poisson")
            {
                entity = player.GetComponentInParent<Fish>();

            }
            if (player.name == "Oiseau")
            {
                entity = player.GetComponentInParent<Bird>();

            }

            if (entity != null && entity.Hurt == false)
            {
                //Debug.Log("Player touché par le sanglier");
                EnleverDegat();

            }
        }
        if (collision.gameObject.tag == "Wall" && currentState == State.Chase)
        {
            Debug.Log("tape contre le mur");
            currentState = State.Stunt;
        }
    }

    //fonction pour enlever les dégats au player
    public void EnleverDegat()
    {
        if (entity != null)
        {
            entity.TakeDamage(1);
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * 200, 10), ForceMode2D.Impulse);
            entity = null;
        }
        else
        {
            Debug.Log("Player est null");
        }
    }
}
