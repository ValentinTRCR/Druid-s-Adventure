using UnityEngine;
using UnityEngine.InputSystem;

public class Bird : Entity
{
    /// <summary>
    /// GameObject principal de l’oiseau.
    /// </summary>
    public GameObject bird;

    /// <summary>
    /// Rigidbody2D utilisé pour gérer les déplacements et la physique.
    /// </summary>
    private Rigidbody2D _rb;

    /// <summary>
    /// Animator utilisé pour gérer les animations.
    /// </summary>
    Animator _anim;

    /// <summary>
    /// Valeur du déplacement vertical récupérée via l’Input System.
    /// </summary>
    float _movey;

    /// <summary>
    /// Vitesse de déplacement de l’oiseau.
    /// </summary>
    public float speed = 3;

    /// <summary>
    /// Vérifie si l’oiseau est en train de voler.
    /// </summary>
    bool _IsFlying;

    /// <summary>
    /// Script permettant de détecter si l’oiseau touche le sol.
    /// </summary>
    DetectionSol _detectionSol;

    /// <summary>
    /// SpriteRenderer utilisé pour retourner le sprite.
    /// </summary>
    SpriteRenderer _sr;

    /// <summary>
    /// Initialisation des composants nécessaires.
    /// </summary>
    void Start()
    {
        _rb = bird.GetComponent<Rigidbody2D>();

        _anim = bird.GetComponentInChildren<Animator>();

        _detectionSol =
            bird.GetComponentInChildren<DetectionSol>();

        _sr = bird.GetComponentInChildren<SpriteRenderer>();

        base.Start();
    }

    /// <summary>
    /// Mise à jour appelée à chaque frame.
    /// Gère les déplacements et les animations.
    /// </summary>
    void Update()
    {
        // Vérifie si l’oiseau est dans l’eau
        if (IsInWater)
        {
            _rb.gravityScale = 1f;

            // Stop le déplacement
            _rb.linearVelocity = new Vector2(0, 0);

            // Inflige des dégâts dans l’eau
            DamageInTheWater();
        }
        else
        {
            // Déplacement horizontal et vertical
            _rb.linearVelocity =
                new Vector2(movex * speed, _movey * speed);
        }

        // Mise à jour des animations
        GererAnimation();
    }

    /// <summary>
    /// Fonction appelée automatiquement lors du déplacement.
    /// </summary>
    /// <param name="inputValue">
    /// Valeur envoyée par le système d’Input.
    /// </param>
    void OnMove(InputValue inputValue)
    {
        Vector2 d = inputValue.Get<Vector2>();

        movex = d.x;

        _movey = d.y;
    }

    /// <summary>
    /// Gère les animations de l’oiseau
    /// selon son état et ses déplacements.
    /// </summary>
    void GererAnimation()
    {
        // Vérifie si l’oiseau n’est pas blessé
        if (!Hurt)
        {
            // Vérifie si l’oiseau vole
            if (_movey > 0.1 || !_detectionSol.ToucheLeSol)
            {
                _IsFlying = true;
            }

            // Vérifie si l’oiseau touche le sol
            if (_detectionSol.ToucheLeSol)
            {
                _IsFlying = false;
            }

            // Animation de marche
            _anim.SetBool(
                "IsWalking",
                (movex > 0.1 || movex < -0.1) &&
                _IsFlying == false &&
                !IsInWater
            );

            // Animation de vol
            _anim.SetBool(
                "IsFlying",
                _IsFlying && !IsInWater
            );

            // Orientation du sprite vers la droite
            if (movex > 0.1f)
            {
                _sr.flipX = true;
            }

            // Orientation du sprite vers la gauche
            else if (movex < -0.1f)
            {
                _sr.flipX = false;
            }
        }
        else
        {
            // Déclenche l’animation de dégâts
            _anim.SetTrigger("Hurt");

            // Réinitialise l’état de blessure
            Hurt = false;
        }
    }
}