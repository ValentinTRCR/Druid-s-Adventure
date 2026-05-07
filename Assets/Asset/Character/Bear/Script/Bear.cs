using UnityEngine;
using UnityEngine.InputSystem;

public class Bear : Entity
{
    /// <summary>
    /// GameObject principal de l’ours.
    /// </summary>
    public GameObject bear;

    /// <summary>
    /// Direction horizontale du déplacement.
    /// </summary>
    float _directionX;

    /// <summary>
    /// Rigidbody2D utilisé pour gérer les déplacements et la physique.
    /// </summary>
    Rigidbody2D _rb;

    /// <summary>
    /// Vitesse de déplacement de l’ours.
    /// </summary>
    float _speed = 3f;

    /// <summary>
    /// Animator utilisé pour gérer les animations.
    /// </summary>
    Animator _animator;

    /// <summary>
    /// Détection du sol à droite du joueur.
    /// </summary>
    DetectionSolDroite _detectionSolDroite;

    /// <summary>
    /// Détection du sol à gauche du joueur.
    /// </summary>
    DetectionSolGauche _detectionSolGauche;

    /// <summary>
    /// Ennemi actuellement détecté pouvant recevoir des dégâts.
    /// </summary>
    Entity _entityEnnemy;

    /// <summary>
    /// Vérifie si l’ours est actuellement en train d’attaquer.
    /// </summary>
    bool _isAttacking = false;

    /// <summary>
    /// Script permettant de détecter si une attaque peut toucher un ennemi.
    /// </summary>
    CanAttack _canAttack;

    /// <summary>
    /// Initialisation des composants nécessaires.
    /// </summary>
    void Start()
    {
        base.Start();

        _rb = bear.GetComponent<Rigidbody2D>();

        _animator = bear.GetComponentInChildren<Animator>();

        _detectionSolDroite =
            bear.GetComponentInChildren<DetectionSolDroite>();

        _detectionSolGauche =
            bear.GetComponentInChildren<DetectionSolGauche>();

        _canAttack = bear.GetComponent<CanAttack>();
    }

    /// <summary>
    /// Fonction appelée à intervalle fixe.
    /// Gère les déplacements physiques de l’ours.
    /// </summary>
    void FixedUpdate()
    {
        float directionX = movex;

        // Bloque le déplacement vers la droite
        // si aucun sol n’est détecté
        if (movex > 0 &&
            !_detectionSolDroite.estAuSolDroite)
        {
            directionX = 0;
        }

        // Bloque le déplacement vers la gauche
        // si aucun sol n’est détecté
        if (movex < 0 &&
            !_detectionSolGauche.estAuSolGauche)
        {
            directionX = 0;
        }

        // Vérifie si l’ours est dans l’eau
        if (IsInWater)
        {
            _rb.gravityScale = 2f;

            // Stop le déplacement
            _rb.linearVelocity = new Vector2(0, 0);

            // Inflige des dégâts dans l’eau
            DamageInTheWater();
        }
        else
        {
            // Gravité normale
            _rb.gravityScale = 1f;

            // Déplacement horizontal
            _rb.linearVelocity =
                new Vector2(
                    directionX * _speed,
                    _rb.linearVelocity.y
                );
        }
    }

    /// <summary>
    /// Mise à jour appelée à chaque frame.
    /// </summary>
    void Update()
    {
        GererAnimation();
    }

    /// <summary>
    /// Fonction appelée automatiquement lors du déplacement.
    /// </summary>
    /// <param name="value">
    /// Valeur envoyée par le système d’Input.
    /// </param>
    void OnMove(InputValue value)
    {
        movex = value.Get<Vector2>().x;
    }

    /// <summary>
    /// Gère les animations de l’ours.
    /// </summary>
    void GererAnimation()
    {
        // Vérifie si l’ours n’est pas blessé
        if (!Hurt)
        {
            // Orientation du sprite vers la droite
            if (movex > 0)
            {
                GetComponentInChildren<SpriteRenderer>().flipX = false;
            }

            // Orientation du sprite vers la gauche
            else if (movex < 0)
            {
                GetComponentInChildren<SpriteRenderer>().flipX = true;
            }

            // Animation de marche
            _animator.SetBool("IsWalking", movex != 0);
        }
        else
        {
            // Déclenche l’animation de dégâts
            _animator.SetTrigger("Hurt");

            // Réinitialise l’état de blessure
            Hurt = false;
        }
    }

    /// <summary>
    /// Fonction appelée lors de l’attaque.
    /// </summary>
    /// <param name="value">
    /// Valeur envoyée par le système d’Input.
    /// </param>
    void OnAttack(InputValue value)
    {
        // Vérifie si le joueur attaque
        // et qu’aucune attaque n’est déjà en cours
        if (value.isPressed && _isAttacking == false)
        {
            _isAttacking = true;

            // Déclenche l’animation d’attaque
            _animator.SetTrigger("Attack");

            // Réinitialise l’attaque après 0.5 secondes
            Invoke("ResetAttack", 0.5f);
        }
    }

    /// <summary>
    /// Inflige des dégâts à l’ennemi détecté.
    /// Fonction appelée via un Animation Event.
    /// </summary>
    public void EnleverDegat()
    {
        _entityEnnemy = _canAttack.entityEnnemy;

        // Vérifie si un ennemi est détecté
        if (_entityEnnemy != null)
        {
            // Inflige des dégâts
            _entityEnnemy.TakeDamage(1);

            _entityEnnemy = null;
        }
    }

    /// <summary>
    /// Réinitialise l’état d’attaque.
    /// </summary>
    public void ResetAttack()
    {
        _isAttacking = false;
    }
}