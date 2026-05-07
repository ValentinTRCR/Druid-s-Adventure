using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    /// <summary>
    /// GameObject principal du druide contrôlé par le joueur.
    /// </summary>
    public GameObject Druide;

    /// <summary>
    /// Rigidbody2D utilisé pour gérer les déplacements et la physique.
    /// </summary>
    Rigidbody2D _rb;

    /// <summary>
    /// Script permettant de détecter si le joueur touche le sol.
    /// </summary>
    DetectionSol _detectionSol;

    /// <summary>
    /// Vitesse de déplacement horizontale du joueur.
    /// </summary>
    float _speed = 5f;

    /// <summary>
    /// Force appliquée lors du saut.
    /// </summary>
    float _jumpForce = 6f;

    /// <summary>
    /// Vérifie si le joueur a sauté.
    /// </summary>
    bool _jump = true;

    /// <summary>
    /// Valeur du déplacement vertical récupérée via l’Input System.
    /// </summary>
    private float _movey;

    /// <summary>
    /// Vérifie si le joueur est en train de sauter.
    /// </summary>
    bool _isJumping;

    /// <summary>
    /// Vérifie si le joueur est en train de tomber.
    /// </summary>
    bool _isFalling;

    /// <summary>
    /// Vérifie si le joueur est en train de marcher.
    /// </summary>
    bool _isWalking;

    /// <summary>
    /// Collider principal du joueur.
    /// </summary>
    CapsuleCollider2D _capsuleCollider2D;

    /// <summary>
    /// Offset du collider lorsque le joueur regarde à droite.
    /// </summary>
    float _offsetxRight;

    /// <summary>
    /// Offset du collider lorsque le joueur regarde à gauche.
    /// </summary>
    float _offsetxLeft = 0.06f;

    /// <summary>
    /// Animator utilisé pour gérer les animations du joueur.
    /// </summary>
    Animator _anim;

    /// <summary>
    /// Script permettant de détecter les objets interactifs proches du joueur.
    /// </summary>
    DetectionInteraction _detectionInteraction;

    /// <summary>
    /// Préfab du cristal récupérable.
    /// </summary>
    public GameObject prefabCrystal;


    /// <summary>
    /// Initialisation des composants nécessaires au joueur.
    /// </summary>
    void Start()
    {
        _rb = Druide.GetComponent<Rigidbody2D>();

        _detectionSol = Druide.GetComponentInChildren<DetectionSol>();

        _capsuleCollider2D = Druide.GetComponent<CapsuleCollider2D>();

        // Sauvegarde de l’offset du collider
        _offsetxRight = _capsuleCollider2D.offset.x;

        _anim = Druide.GetComponentInChildren<Animator>();

        _detectionInteraction =
            Druide.GetComponentInChildren<DetectionInteraction>();

        // Vérifie si le joueur possède déjà le cristal
        if(PlayerStatManager.Instance.HasCrystal)
        {
            GameObject instanceCrystal = Instantiate(prefabCrystal);

            DeplacerCrystal(instanceCrystal);

            instanceCrystal.name = "crystal";
        }

        base.Start();
    }

    /// <summary>
    /// Mise à jour appelée à chaque frame.
    /// Gère les déplacements et les animations.
    /// </summary>
    void Update()
    {
        // Vérifie si le joueur est dans l’eau
        if (IsInWater)
        {
            _rb.gravityScale = 2f;

            // Stop le déplacement du joueur
            _rb.linearVelocity = new Vector2(0, 0);

            // Inflige des dégâts dans l’eau
            DamageInTheWater();
        }
        else
        {
            // Gravité normale
            _rb.gravityScale = 1f;

            // Déplacement horizontal du joueur
            _rb.linearVelocity =
                new Vector2(movex * _speed, _rb.linearVelocity.y);
        }

        // Mise à jour des animations
        gererAnimation();
    }

    /// <summary>
    /// Fonction appelée automatiquement lors du déplacement du joueur.
    /// </summary>
    /// <param name="value">
    /// Valeur envoyée par le système d’Input.
    /// </param>
    void OnMove(InputValue value)
    {
        Vector2 d = value.Get<Vector2>();

        movex = d.x;
        _movey = d.y;
    }

    /// <summary>
    /// Fonction appelée lorsque le joueur saute.
    /// </summary>
    /// <param name="value">
    /// Valeur envoyée par le système d’Input.
    /// </param>
    void OnJump(InputValue value)
    {
        // Vérifie si le joueur appuie sur la touche
        // et qu’il touche le sol
        if (value.isPressed && _detectionSol.ToucheLeSol)
        {
            // Applique une impulsion vers le haut
            _rb.AddForce(
                new Vector2(0, _jumpForce),
                ForceMode2D.Impulse
            );

            _jump = true;
        }
    }

    /// <summary>
    /// Fonction appelée lors d’une interaction avec un objet.
    /// Permet de récupérer un cristal,
    /// activer un levier ou déposer le cristal.
    /// </summary>
    /// <param name="value">
    /// Valeur envoyée par le système d’Input.
    /// </param>
    void OnInteract(InputValue value)
    {
        if(value.isPressed)
        {
            // Objet détecté par le joueur
            GameObject objectToCollect =
                _detectionInteraction.ObjectToCollect;

            // Vérifie si l’objet est un cristal
            if(objectToCollect != null &&
               objectToCollect.name == "crystal")
            {
                DeplacerCrystal(objectToCollect);

                PlayerStatManager.Instance.HasCrystal = true;
            }

            // Vérifie si l’objet est un levier
            else if(objectToCollect.name == "Levier" &&
                    objectToCollect.CompareTag("Interact"))
            {
                objectToCollect
                    .GetComponent<Levier>()
                    .isActivated = true;
            }

            // Vérifie si le joueur dépose le cristal
            else if(objectToCollect.name =="depotCrystal" &&
                    objectToCollect.CompareTag("Interact"))
            {
                Transform crystal =
                    Druide.transform.Find("crystal");

                if(crystal != null)
                {
                    PlayerStatManager.Instance.HasCrystal = false;

                    // Attache le cristal au dépôt
                    crystal.SetParent(objectToCollect.transform);
                }
                else
                {
                    Debug.Log("No crystal to deposit");
                }
            }
        }
    }

    /// <summary>
    /// Déplace le cristal sur le joueur
    /// et désactive ses composants visuels.
    /// </summary>
    /// <param name="objectToCollect">
    /// Cristal à déplacer.
    /// </param>
    void DeplacerCrystal(GameObject objectToCollect)
    {
        // Attache le cristal au joueur
        objectToCollect.transform.SetParent(Druide.transform);

        // Désactive le rendu du cristal
        objectToCollect
            .GetComponentInChildren<SpriteRenderer>()
            .enabled = false;

        // Désactive son collider
        objectToCollect
            .GetComponent<Collider2D>()
            .enabled = false;

        // Désactive la lumière du cristal
        objectToCollect
            .transform
            .Find("Spot Light 2D")
            .gameObject
            .SetActive(false);
    }

    /// <summary>
    /// Gère les animations du joueur
    /// selon ses déplacements et son état.
    /// </summary>
    public void gererAnimation()
    {
        // Vérifie si le joueur n’est pas blessé
        if (!Hurt)
        {
            // Direction vers la droite
            if (movex > 0)
            {
                GetComponentInChildren<SpriteRenderer>().flipX = false;

                _capsuleCollider2D.offset =
                    new Vector2(
                        _offsetxRight,
                        _capsuleCollider2D.offset.y
                    );
            }

            // Direction vers la gauche
            else if (movex < 0)
            {
                GetComponentInChildren<SpriteRenderer>().flipX = true;

                _capsuleCollider2D.offset =
                    new Vector2(
                        _offsetxLeft,
                        _capsuleCollider2D.offset.y
                    );
            }

            // Détection des états du joueur
            _isJumping = _rb.linearVelocity.y > 0.1f;

            _isFalling = _rb.linearVelocity.y < -0.1f;

            _isWalking = movex != 0;

            // Mise à jour des paramètres Animator
            _anim.SetBool("IsJumping", _isJumping);

            _anim.SetBool("IsFalling", _isFalling);

            _anim.SetBool(
                "IsWalking",
                _isWalking && !_isJumping && !_isFalling
            );
        }
        else
        {
            // Déclenche l’animation de dégâts
            _anim.SetTrigger("Hurt");

            Hurt = false;
        }
    }
}