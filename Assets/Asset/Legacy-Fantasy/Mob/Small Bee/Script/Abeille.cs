using UnityEngine;
public class Abeille : Entity
{
    public enum State
    {
        Patrouille,
        FlyUp,
        Dive,
        Stunt,
        InWater,
    }
    enum FlyPhase
    {
        AllerAuCentre,
        AllerAGauche,
        AllerADroite,
        Fini
    }

    private FlyPhase flyPhase = FlyPhase.AllerAuCentre;
    public State currentState;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private float speed;

    private float patrolSpeed = 2f;

    private float flyUpSpeed = 4f;

    private float diveSpeed = 10f;

    private float stunDuration = 4f;
    private float stunTimer = 0f;

    private float direction = 1;

    public GameObject DetecteurDroit;
    public GameObject DetecteurGauche;

    public GameObject DetecteurPlayerEtSol;

    DetecterObstacle detecterObstacleDroit;
    DetecterObstacle detecterObstacleGauche;

    DetecterEnnemiSol detecterEnnemiSol;

    GameObject Sol;

    GameObject player;

    float distanceEntreAbeilleEtSol;
    float distanceMaxEntreAbeilleEtSol = 5.5f;
    float distanceMinEntreAbeilleEtSol = 4.5f;

    float distanceActuelleEntreAbeilleEtSol;

    //FlyUp

    //Calcul Distance Y
    float distanceActuelleEntreAbeilleEtPlayer;
    float distanceMaxEntreAbeilleEtPlayer = 5.5f;
    float distanceMinEntreAbeilleEtPlayer = 4.5f;
    //Calcul Distance X    

    public bool IsAttacking = false;

    float flyUpTimer = 0f;
    float maxFlyUpTime = 3.5f;
    //position du joueur au moment du dive;
    Transform targetPlayerPosition;

    Entity entity;

    Vector2 directionBee;

    public GameObject LimitePatrouilleDroite;
    public GameObject LimitePatrouilleGauche;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IsDead = false;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        detecterObstacleDroit = DetecteurDroit.GetComponent<DetecterObstacle>();
        detecterObstacleGauche = DetecteurGauche.GetComponent<DetecterObstacle>();
        currentState = State.Patrouille;
        detecterEnnemiSol = DetecteurPlayerEtSol.GetComponent<DetecterEnnemiSol>();
        speed = patrolSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentState);
        if (Hurt == true)
        {
            anim.SetTrigger("Hurt");
            Hurt = false;
        }
        else
        {
            flip();
            if(IsInWater)
            {
                currentState = State.InWater;
            }
            //Si pas de joueur détecter et qu'il n'est pas entrain d'attaquer
            if (detecterEnnemiSol.player != null && !IsAttacking)
            {
                player = detecterEnnemiSol.player;
                currentState = State.FlyUp;
            }
            switch (currentState)
            {
                case State.Patrouille:
                    // Logique de patrouille
                    Patrouille();
                    //ResetAttack();
                    break;
                case State.FlyUp:
                    FlyUp();
                    // Logique de vol vers le haut
                    break;
                case State.Dive:
                    Dive();
                    // Logique de plongée
                    break;
                case State.Stunt:
                    Stunt();
                    // Logique de stun
                    break;
                case State.InWater:
                    InWater();
                    // Logique de stun
                    break;
            }
        }

    }

    void InWater()
    {
        rb.gravityScale = 2f;
        rb.linearVelocity = new Vector2(0, 0);
        Invoke("PrendreDesDegats",2f);
    }

    void PrendreDesDegats()
    {
        TakeDamage(1);
    }
    void flip()
    {
        if (direction == 1)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

    void Patrouille()
    {
        speed = patrolSpeed;
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);

        if(transform.position.x > LimitePatrouilleDroite.transform.position.x ||detecterObstacleDroit.isObstacleDetected)
        {
            direction = -1;
        }
        else if(transform.position.x < LimitePatrouilleGauche.transform.position.x || detecterObstacleGauche.isObstacleDetected)
        {
            direction = 1;
        }

        if(transform.position.y < LimitePatrouilleGauche.transform.position.y - 0.4f)
        {
             rb.linearVelocity = new Vector2(rb.linearVelocity.x, speed);
        }
        if(transform.position.y > LimitePatrouilleDroite.transform.position.y + 0.4f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -speed);
        }

       
    }

    //contient du code générer par l'ia
    void FlyUp()
    {
        IsAttacking = true;
        if (player == null)
        {
            currentState = State.Patrouille;
            return;
        }
        

        float velocityX = 0f;
        float velocityY = 0f;

        speed = flyUpSpeed;


        float distanceY = transform.position.y - player.transform.position.y;

        if (distanceY < distanceMinEntreAbeilleEtPlayer)
        {
            velocityY = speed;
        }
        else if (distanceY > distanceMaxEntreAbeilleEtPlayer)
        {
            velocityY = -speed;
        }
        else
        {
            velocityY = 0f;
        }

        float positionCibleX = player.transform.position.x;
        float pointGauche = positionCibleX - 2.5f;
        float pointDroit = positionCibleX + 2.5f;

        float marge = 0.1f;

        switch (flyPhase)
        {
            case FlyPhase.AllerAuCentre:
                if (transform.position.x < positionCibleX - marge)
                {
                    velocityX = speed;
                }
                else if (transform.position.x > positionCibleX + marge)
                {
                    velocityX = -speed;
                }
                else
                {
                    flyPhase = FlyPhase.AllerAGauche;
                }
                break;

            case FlyPhase.AllerAGauche:
                if (transform.position.x > pointGauche + marge)
                {
                    velocityX = -speed;
                }
                else
                {
                    flyPhase = FlyPhase.AllerADroite;
                }
                break;

            case FlyPhase.AllerADroite:
                if (transform.position.x < pointDroit - marge)
                {
                    velocityX = speed;
                }
                else
                {
                    flyPhase = FlyPhase.AllerAGauche;
                }
                break;

            case FlyPhase.Fini:
                //Debug.Log("Fin de la phase de vol vers le haut");
                currentState = State.Dive;
                flyPhase = FlyPhase.AllerAuCentre;
                break;
        }

        rb.linearVelocity = new Vector2(velocityX, velocityY);
        flyUpTimer += Time.deltaTime;
        if (flyUpTimer >= maxFlyUpTime)
        {
            flyUpTimer = 0f;
            flyPhase = FlyPhase.Fini;
        }
    }

    void Dive()
    {
        
        if (targetPlayerPosition == null)
        {
            targetPlayerPosition = player.transform;
            
            directionBee = (targetPlayerPosition.position - transform.position).normalized;
            
            return;
        }
        rb.linearVelocity = directionBee * diveSpeed;

    }

    void Stunt()
    {
        stunTimer += Time.deltaTime;
        rb.linearVelocity = Vector2.zero;
        if (stunTimer >= stunDuration)
        {
            stunTimer = 0f;
            currentState = State.Patrouille;
            IsAttacking = false;
            targetPlayerPosition = null;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && currentState == State.Dive)
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
                //Debug.Log("Player touché par l'abeille");
                EnleverDegat();

            }
        }
        else if (currentState == State.Dive)
        {
            currentState = State.Stunt;
            stunTimer = 0f;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(currentState == State.Dive)
        {
            currentState = State.Patrouille;
            Invoke("ResetAttack",1f);
            stunTimer = 0f;
        }

    }

    void EnleverDegat()
    {
        if (entity != null)
        {
            anim.SetTrigger("Attack");
            entity.TakeDamage(1);
            entity = null;
            currentState = State.Patrouille;
            IsAttacking = false; 
            targetPlayerPosition = null; 
        }
    }

    void ResetAttack()
    {
        IsAttacking = false;
    }
}
