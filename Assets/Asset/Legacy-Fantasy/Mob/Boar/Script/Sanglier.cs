using Unity.VisualScripting;
using UnityEngine;

public class Sanglier : Entity
{
    /// <summary>
    /// États possibles du sanglier.
    /// </summary>
    public enum State
    {
        Patrouille,
        Chase,
        Stunt,
    }

    //variable mouvement

    /// <summary>
    /// Vitesse actuelle du sanglier.
    /// </summary>
    public float speed;

    /// <summary>
    /// Vitesse utilisée lorsque le sanglier poursuit le joueur.
    /// </summary>
    public float chaseSpeed = 4f;

    /// <summary>
    /// Vitesse utilisée lorsque le sanglier patrouille.
    /// </summary>
    public float walkSpeed = 2f;

    /// <summary>
    /// État actuel du sanglier.
    /// </summary>
    public State currentState;

    /// <summary>
    /// Direction actuelle du déplacement.
    /// </summary>
    public float direction;

    /// <summary>
    /// Rigidbody2D utilisé pour gérer les déplacements et la physique.
    /// </summary>
    Rigidbody2D _rb;

    /// <summary>
    /// Animator utilisé pour gérer les animations.
    /// </summary>
    Animator _anim;

    //Script Detection

    /// <summary>
    /// Détecteur du sol et des murs à droite.
    /// </summary>
    private DetectionSolDroite _detectionDroite;

    /// <summary>
    /// Détecteur du sol et des murs à gauche.
    /// </summary>
    private DetectionSolGauche _detectionGauche;

    //SpriteRenderer spriteRenderer;

    /// <summary>
    /// SpriteRenderer utilisé pour retourner le sprite.
    /// </summary>
    SpriteRenderer _sr;

    /// <summary>
    /// GameObject du joueur touché ou détecté.
    /// </summary>
    GameObject _player;

    /// <summary>
    /// Entité du joueur permettant d’infliger des dégâts.
    /// </summary>
    Entity _entity;

    //compteur chase

    /// <summary>
    /// Timer de poursuite du joueur.
    /// </summary>
    float _chaseTime = 0f;

    /// <summary>
    /// Temps maximum pendant lequel le sanglier poursuit le joueur.
    /// </summary>
    float _maxChaseTime = 3f;

    //compteur stunt

    /// <summary>
    /// Timer de l’état étourdi.
    /// </summary>
    float _stuntTime = 0f;

    /// <summary>
    /// Temps maximum pendant lequel le sanglier reste étourdi.
    /// </summary>
    float _maxStuntTime = 3f;

    /// <summary>
    /// Script permettant de détecter le joueur.
    /// </summary>
    DetecterPlayer _detecterPlayer;

    /// <summary>
    /// Initialisation des composants nécessaires.
    /// </summary>
    void Start()
    {
        IsDead = false;
        _rb = GetComponent<Rigidbody2D>();
        currentState = State.Patrouille;
        direction = 1f;
        _detectionDroite = GetComponentInChildren<DetectionSolDroite>();
        _detectionGauche = GetComponentInChildren<DetectionSolGauche>();
        _anim = GetComponentInChildren<Animator>();
        _sr = GetComponentInChildren<SpriteRenderer>();
        _detecterPlayer = GetComponentInChildren<DetecterPlayer>();
        speed = walkSpeed;
    }

    /// <summary>
    /// Mise à jour appelée à chaque frame.
    /// Gère les états, les déplacements et les animations du sanglier.
    /// </summary>
    void Update()
    {
        // Si le joueur est détecté et que le sanglier n’est pas étourdi,
        // il passe en mode poursuite.
        if (_detecterPlayer.chasePlayer && currentState != State.Stunt)
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

    /// <summary>
    /// Gère le déplacement de patrouille du sanglier.
    /// </summary>
    void Patrouille()
    {
        verifierSiIlestAuSolOuEstBloqueParUnMur();
        Flip();
        _rb.linearVelocity = new Vector2(direction * speed, _rb.linearVelocity.y);
    }

    /// <summary>
    /// Vérifie si le sanglier est encore au sol
    /// ou s’il est bloqué par un mur.
    /// </summary>
    void verifierSiIlestAuSolOuEstBloqueParUnMur()
    {
        if (_detectionDroite.estAuSolDroite == true && _detectionGauche.estAuSolGauche == false)
        {

            direction = 1f;
        }
        else if (_detectionDroite.estAuSolDroite == false && _detectionGauche.estAuSolGauche == true)
        {

            direction = -1f;
        }
        if(_detectionDroite.bloquer == true)
        {
            direction = -1f;
        }
        else if(_detectionGauche.bloquer == true)
        {
            direction = 1f;
        }
    }

    /// <summary>
    /// Retourne le sprite selon la direction du sanglier.
    /// </summary>
    void Flip()
    {
        if (direction == 1f)
        {
            _sr.flipX = true;
        }
        else
        {
            _sr.flipX = false;
        }
    }

    /// <summary>
    /// Gère la poursuite du joueur.
    /// </summary>
    void Chase()
    {
        _rb.linearVelocity = new Vector2(direction * speed, _rb.linearVelocity.y);
        Flip();

        if (_chaseTime < _maxChaseTime)
        {
            _chaseTime += Time.deltaTime;
        }
        else
        {
            _detecterPlayer.chasePlayer = false;
            currentState = State.Patrouille;
            speed = walkSpeed;
            _chaseTime = 0f;
        }

    }

    /// <summary>
    /// Gère l’état étourdi du sanglier.
    /// </summary>
    void Stunt()
    {
        _rb.linearVelocity = Vector2.zero;
        _detecterPlayer.chasePlayer = false;
        if (_stuntTime < _maxStuntTime)
        {
            _stuntTime += Time.deltaTime;
        }
        else
        {
            currentState = State.Patrouille;
            _stuntTime = 0f;
        }
    }

    /// <summary>
    /// Gère les animations du sanglier selon son état.
    /// </summary>
    void GererAnimation()
    {
        if (!Hurt)
        {
            if (currentState == State.Patrouille)
            {
                _anim.SetBool("isWalking", true);
                _anim.SetBool("isRunning", false);
            }
            else if (currentState == State.Chase)
            {
                _anim.SetBool("isRunning", true);
                _anim.SetBool("isWalking", false);
            }
            else if (currentState == State.Stunt)
            {
                _anim.SetBool("isWalking", false);
                _anim.SetBool("isRunning", false);
            }
        }
        else
        {
            _anim.SetTrigger("Hurt");
            Hurt = false;
        }
    }

    //fonction pour savoir si il rentre en collision avec le joueur ou un mur
    //si cette un joueur il lui inflige des dégats et le repousse sinon il rentre en stunt

    /// <summary>
    /// Fonction appelée lorsqu’une collision commence.
    /// Permet d’infliger des dégâts au joueur ou
    /// de mettre le sanglier en état étourdi.
    /// </summary>
    /// <param name="collision">
    /// Collision détectée.
    /// </param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision avec: " + collision.gameObject.name + " tag: " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Player" && currentState == State.Chase)
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

    /// <summary>
    /// Inflige des dégâts au joueur détecté
    /// et le repousse avec une force.
    /// </summary>
    public void EnleverDegat()
    {
        if (_entity != null)
        {
            _entity.TakeDamage(1);
            _player.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * 200, 10), ForceMode2D.Impulse);
            _entity = null;
        }
        else
        {
            Debug.Log("Player est null");
        }
    }
}