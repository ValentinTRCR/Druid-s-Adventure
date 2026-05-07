using UnityEngine;

public class Abeille : Entity
{
    /// <summary>
    /// États possibles de l’abeille.
    /// </summary>
    public enum State
    {
        Patrouille,
        FlyUp,
        Dive,
        Stunt,
        InWater,
    }

    /// <summary>
    /// Phases utilisées pendant le déplacement aérien avant l’attaque.
    /// </summary>
    enum FlyPhase
    {
        AllerAuCentre,
        AllerAGauche,
        AllerADroite,
        Fini
    }

    /// <summary>
    /// Phase actuelle du déplacement aérien.
    /// </summary>
    private FlyPhase _flyPhase = FlyPhase.AllerAuCentre;

    /// <summary>
    /// État actuel de l’abeille.
    /// </summary>
    public State currentState;

    /// <summary>
    /// Rigidbody2D utilisé pour gérer les déplacements.
    /// </summary>
    private Rigidbody2D _rb;

    /// <summary>
    /// Animator utilisé pour gérer les animations.
    /// </summary>
    private Animator _anim;

    /// <summary>
    /// SpriteRenderer utilisé pour retourner le sprite.
    /// </summary>
    private SpriteRenderer _sr;

    /// <summary>
    /// Vitesse actuelle de l’abeille.
    /// </summary>
    private float _speed;

    /// <summary>
    /// Vitesse utilisée pendant la patrouille.
    /// </summary>
    private float _patrolSpeed = 2f;

    /// <summary>
    /// Vitesse utilisée pendant la montée avant l’attaque.
    /// </summary>
    private float _flyUpSpeed = 4f;

    /// <summary>
    /// Vitesse utilisée pendant la plongée.
    /// </summary>
    private float _diveSpeed = 10f;

    /// <summary>
    /// Durée pendant laquelle l’abeille est étourdie.
    /// </summary>
    private float _stunDuration = 4f;

    /// <summary>
    /// Timer de l’état étourdi.
    /// </summary>
    private float _stunTimer = 0f;

    /// <summary>
    /// Direction de déplacement de l’abeille.
    /// </summary>
    private float _direction = 1;

    /// <summary>
    /// Détecteur d’obstacle à droite.
    /// </summary>
    public GameObject DetecteurDroit;

    /// <summary>
    /// Détecteur d’obstacle à gauche.
    /// </summary>
    public GameObject DetecteurGauche;

    /// <summary>
    /// Détecteur du joueur et du sol.
    /// </summary>
    public GameObject DetecteurPlayerEtSol;

    /// <summary>
    /// Script du détecteur d’obstacle à droite.
    /// </summary>
    DetecterObstacle _detecterObstacleDroit;

    /// <summary>
    /// Script du détecteur d’obstacle à gauche.
    /// </summary>
    DetecterObstacle _detecterObstacleGauche;

    /// <summary>
    /// Script permettant de détecter le joueur.
    /// </summary>
    DetecterEnnemiSol _detecterEnnemiSol;

    /// <summary>
    /// Référence au sol détecté.
    /// </summary>
    GameObject _Sol;

    /// <summary>
    /// Référence au joueur détecté.
    /// </summary>
    GameObject _player;

    /// <summary>
    /// Distance entre l’abeille et le sol.
    /// </summary>
    float _distanceEntreAbeilleEtSol;

    /// <summary>
    /// Distance maximale entre l’abeille et le sol.
    /// </summary>
    float _distanceMaxEntreAbeilleEtSol = 5.5f;

    /// <summary>
    /// Distance minimale entre l’abeille et le sol.
    /// </summary>
    float _distanceMinEntreAbeilleEtSol = 4.5f;

    /// <summary>
    /// Distance actuelle entre l’abeille et le sol.
    /// </summary>
    float _distanceActuelleEntreAbeilleEtSol;

    //FlyUp

    //Calcul Distance Y

    /// <summary>
    /// Distance actuelle entre l’abeille et le joueur.
    /// </summary>
    float _distanceActuelleEntreAbeilleEtPlayer;

    /// <summary>
    /// Distance maximale entre l’abeille et le joueur.
    /// </summary>
    float _distanceMaxEntreAbeilleEtPlayer = 5.5f;

    /// <summary>
    /// Distance minimale entre l’abeille et le joueur.
    /// </summary>
    float _distanceMinEntreAbeilleEtPlayer = 4.5f;

    //Calcul Distance X    

    /// <summary>
    /// Vérifie si l’abeille est actuellement en train d’attaquer.
    /// </summary>
    public bool IsAttacking = false;

    /// <summary>
    /// Timer de la phase FlyUp.
    /// </summary>
    float _flyUpTimer = 0f;

    /// <summary>
    /// Temps maximum de la phase FlyUp.
    /// </summary>
    float _maxFlyUpTime = 3.5f;

    //position du joueur au moment du dive;

    /// <summary>
    /// Position du joueur ciblée au moment de la plongée.
    /// </summary>
    Transform _targetPlayerPosition;

    /// <summary>
    /// Entité touchée par l’abeille.
    /// </summary>
    Entity _entity;

    /// <summary>
    /// Direction de déplacement de l’abeille pendant la plongée.
    /// </summary>
    Vector2 _directionBee;

    /// <summary>
    /// Limite droite de la zone de patrouille.
    /// </summary>
    public GameObject LimitePatrouilleDroite;

    /// <summary>
    /// Limite gauche de la zone de patrouille.
    /// </summary>
    public GameObject LimitePatrouilleGauche;




    /// <summary>
    /// Initialisation des composants nécessaires.
    /// </summary>
    void Start()
    {
        IsDead = false;
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
        _sr = GetComponentInChildren<SpriteRenderer>();
        _detecterObstacleDroit = DetecteurDroit.GetComponent<DetecterObstacle>();
        _detecterObstacleGauche = DetecteurGauche.GetComponent<DetecterObstacle>();
        currentState = State.Patrouille;
        _detecterEnnemiSol = DetecteurPlayerEtSol.GetComponent<DetecterEnnemiSol>();
        _speed = _patrolSpeed;
    }

    /// <summary>
    /// Mise à jour appelée à chaque frame.
    /// Gère l’état actuel de l’abeille.
    /// </summary>
    void Update()
    {
        //Debug.Log(currentState);
        if (Hurt == true)
        {
            _anim.SetTrigger("Hurt");
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
            if (_detecterEnnemiSol.player != null && !IsAttacking)
            {
                _player = _detecterEnnemiSol.player;
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

    /// <summary>
    /// Gère le comportement de l’abeille lorsqu’elle est dans l’eau.
    /// </summary>
    void InWater()
    {
        _rb.gravityScale = 2f;
        _rb.linearVelocity = new Vector2(0, 0);
        Invoke("PrendreDesDegats",2f);
    }

    /// <summary>
    /// Inflige des dégâts à l’abeille.
    /// </summary>
    void PrendreDesDegats()
    {
        TakeDamage(1);
    }

    /// <summary>
    /// Retourne le sprite selon la direction de déplacement.
    /// </summary>
    void flip()
    {
        if (_direction == 1)
        {
            _sr.flipX = true;
        }
        else
        {
            _sr.flipX = false;
        }
    }

    /// <summary>
    /// Gère le déplacement de patrouille de l’abeille.
    /// </summary>
    void Patrouille()
    {
        _speed = _patrolSpeed;
        _rb.linearVelocity = new Vector2(_direction * _speed, _rb.linearVelocity.y);

        if(transform.position.x > LimitePatrouilleDroite.transform.position.x ||_detecterObstacleDroit.isObstacleDetected)
        {
            _direction = -1;
        }
        else if(transform.position.x < LimitePatrouilleGauche.transform.position.x || _detecterObstacleGauche.isObstacleDetected)
        {
            _direction = 1;
        }

        if(transform.position.y < LimitePatrouilleGauche.transform.position.y - 0.4f)
        {
             _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _speed);
        }
        if(transform.position.y > LimitePatrouilleDroite.transform.position.y + 0.4f)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, -_speed);
        }

       
    }

    //contient du code générer par l'ia
    /// <summary>
    /// Gère la phase où l’abeille monte et se place autour du joueur avant de plonger.
    /// </summary>
    void FlyUp()
    {
        IsAttacking = true;
        if (_player == null)
        {
            currentState = State.Patrouille;
            return;
        }
        

        float velocityX = 0f;
        float velocityY = 0f;

        _speed = _flyUpSpeed;


        float distanceY = transform.position.y - _player.transform.position.y;

        if (distanceY < _distanceMinEntreAbeilleEtPlayer)
        {
            velocityY = _speed;
        }
        else if (distanceY > _distanceMaxEntreAbeilleEtPlayer)
        {
            velocityY = -_speed;
        }
        else
        {
            velocityY = 0f;
        }

        float positionCibleX = _player.transform.position.x;
        float pointGauche = positionCibleX - 2.5f;
        float pointDroit = positionCibleX + 2.5f;

        float marge = 0.1f;

        switch (_flyPhase)
        {
            case FlyPhase.AllerAuCentre:
                if (transform.position.x < positionCibleX - marge)
                {
                    velocityX = _speed;
                }
                else if (transform.position.x > positionCibleX + marge)
                {
                    velocityX = -_speed;
                }
                else
                {
                    _flyPhase = FlyPhase.AllerAGauche;
                }
                break;

            case FlyPhase.AllerAGauche:
                if (transform.position.x > pointGauche + marge)
                {
                    velocityX = -_speed;
                }
                else
                {
                    _flyPhase = FlyPhase.AllerADroite;
                }
                break;

            case FlyPhase.AllerADroite:
                if (transform.position.x < pointDroit - marge)
                {
                    velocityX = _speed;
                }
                else
                {
                    _flyPhase = FlyPhase.AllerAGauche;
                }
                break;

            case FlyPhase.Fini:
                //Debug.Log("Fin de la phase de vol vers le haut");
                currentState = State.Dive;
                _flyPhase = FlyPhase.AllerAuCentre;
                break;
        }

        _rb.linearVelocity = new Vector2(velocityX, velocityY);
        _flyUpTimer += Time.deltaTime;
        if (_flyUpTimer >= _maxFlyUpTime)
        {
            _flyUpTimer = 0f;
            _flyPhase = FlyPhase.Fini;
        }
    }

    /// <summary>
    /// Gère la plongée de l’abeille vers la position du joueur.
    /// </summary>
    void Dive()
    {
        
        if (_targetPlayerPosition == null)
        {
            _targetPlayerPosition = _player.transform;
            
            _directionBee = (_targetPlayerPosition.position - transform.position).normalized;
            
            return;
        }
        _rb.linearVelocity = _directionBee * _diveSpeed;

    }

    /// <summary>
    /// Gère l’état étourdi de l’abeille après une collision.
    /// </summary>
    void Stunt()
    {
        _stunTimer += Time.deltaTime;
        _rb.linearVelocity = Vector2.zero;
        if (_stunTimer >= _stunDuration)
        {
            _stunTimer = 0f;
            currentState = State.Patrouille;
            IsAttacking = false;
            _targetPlayerPosition = null;
        }
    }


    /// <summary>
    /// Fonction appelée lorsqu’une collision commence.
    /// Sert à gérer les dégâts au joueur ou le stun de l’abeille.
    /// </summary>
    /// <param name="collision">
    /// Collision détectée.
    /// </param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && currentState == State.Dive)
        {
            _player = collision.gameObject;
            if (_player.name == "Ours")
            {
                _entity = _player.GetComponentInParent<Bear>();

            }
            if (_player.name == "Druide")
            {
                _entity = _player.GetComponentInParent<Player>();

            }
            if (_player.name == "Poisson")
            {
                _entity = _player.GetComponentInParent<Fish>();

            }
            if (_player.name == "Oiseau")
            {
                _entity = _player.GetComponentInParent<Bird>();

            }

            if (_entity != null && _entity.Hurt == false)
            {
                //Debug.Log("Player touché par l'abeille");
                EnleverDegat();

            }
        }
        else if (currentState == State.Dive)
        {
            currentState = State.Stunt;
            _stunTimer = 0f;
        }
    }

    /// <summary>
    /// Fonction appelée lorsqu’une collision continue.
    /// Réinitialise l’attaque si l’abeille reste bloquée pendant une plongée.
    /// </summary>
    /// <param name="collision">
    /// Collision détectée.
    /// </param>
    void OnCollisionStay2D(Collision2D collision)
    {
        if(currentState == State.Dive)
        {
            currentState = State.Patrouille;
            Invoke("ResetAttack",1f);
            _stunTimer = 0f;
        }

    }

    /// <summary>
    /// Inflige des dégâts au joueur détecté.
    /// </summary>
    void EnleverDegat()
    {
        if (_entity != null)
        {
            _anim.SetTrigger("Attack");
            _entity.TakeDamage(1);
            _entity = null;
            currentState = State.Patrouille;
            IsAttacking = false; 
            _targetPlayerPosition = null; 
        }
    }

    /// <summary>
    /// Réinitialise l’état d’attaque de l’abeille.
    /// </summary>
    void ResetAttack()
    {
        IsAttacking = false;
    }
}