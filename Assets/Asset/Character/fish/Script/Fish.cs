using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fish : Entity
{
    /// <summary>
    /// GameObject principal du poisson.
    /// </summary>
    public GameObject fish;

    /// <summary>
    /// Rigidbody2D utilisé pour gérer les déplacements et la physique.
    /// </summary>
    Rigidbody2D _rb;

    /// <summary>
    /// Animator utilisé pour gérer les animations.
    /// </summary>
    Animator _anim;

    /// <summary>
    /// Valeur du déplacement vertical récupérée via l’Input System.
    /// </summary>
    float _movey;

    /// <summary>
    /// Vitesse maximale du poisson.
    /// </summary>
    float _speed = 7f;

    /// <summary>
    /// Vitesse d’accélération du poisson.
    /// </summary>
    float _acceleration = 4f;

    /// <summary>
    /// Vitesse actuelle utilisée pour lisser le déplacement.
    /// </summary>
    Vector2 _velocityCourante;

    /// <summary>
    /// Vitesse de rotation du poisson.
    /// </summary>
    float _rotationSpeed = 200f;

    /// <summary>
    /// SpriteRenderer utilisé pour retourner le sprite.
    /// </summary>
    SpriteRenderer _spriteRenderer;

    /// <summary>
    /// Initialisation des composants nécessaires.
    /// </summary>
    void Start()
    {
        _rb = fish.GetComponent<Rigidbody2D>();

        _anim = fish.GetComponentInChildren<Animator>();

        _spriteRenderer =
            fish.GetComponentInChildren<SpriteRenderer>();

        base.Start();
    }

    /// <summary>
    /// Mise à jour appelée à chaque frame.
    /// Gère les déplacements et les animations.
    /// </summary>
    void Update()
    {
        // Vérifie si le poisson est dans l’eau
        if(IsInWater)
        {
            // Désactive la gravité
            _rb.gravityScale = 0f;

            // Calcul de la vitesse souhaitée
            Vector2 VitesseVoulue =
                new Vector2(
                    movex * _speed,
                    _movey * _speed
                );

            // Lissage du déplacement
            _velocityCourante =
                Vector2.Lerp(
                    _velocityCourante,
                    VitesseVoulue,
                    _acceleration * Time.deltaTime
                );

            _rb.linearVelocity = _velocityCourante;

            // Vérifie si le poisson est en mouvement
            if(Mathf.Abs(movex) > 0.1 ||
               Mathf.Abs(_movey) > 0.1)
            {
                // Calcul de l’angle visé
                float angleVise =
                    Mathf.Atan2(_movey, movex) *
                    Mathf.Rad2Deg;

                // Angle actuel du poisson
                float angleActuel =
                    fish.transform.rotation.eulerAngles.z;

                // Rotation progressive
                float nouvelAngle =
                    Mathf.MoveTowardsAngle(
                        angleActuel,
                        angleVise,
                        _rotationSpeed * Time.deltaTime
                    );

                // Application de la rotation
                fish.transform.rotation =
                    Quaternion.Euler(0, 0, nouvelAngle);
            }
        }
        else
        {
            // Réactive la gravité hors de l’eau
            _rb.gravityScale = 1f;

            // Stop le déplacement horizontal
            _rb.linearVelocity =
                new Vector2(0, _rb.linearVelocityY);
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
    /// Gère les animations et l’orientation du sprite.
    /// </summary>
    void GererAnimation()
    {
        // Vérifie si le poisson est orienté vers le bas
        if(fish.transform.rotation.eulerAngles.z > 90 &&
           fish.transform.rotation.eulerAngles.z < 270)
        {
            _spriteRenderer.flipY = true;
        }
        else
        {
            _spriteRenderer.flipY = false;
        }

        // Animation de nage
        //anim.SetBool(
        //    "IsSwimming",
        //    (movex > 0.1 ||
        //    movex < -0.1 ||
        //    _movey > 0.1 ||
        //    _movey < -0.1) &&
        //    IsInWater
        //);
    }
}