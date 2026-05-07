using UnityEngine;
using UnityEngine.InputSystem;

public class Water : MonoBehaviour
{
    /// <summary>
    /// GameObject actuellement détecté dans l’eau.
    /// </summary>
    GameObject _Entity;

    /// <summary>
    /// Référence au script GestionPersonnage.
    /// </summary>
    GestionPersonnage _gp;

    /// <summary>
    /// GameObject du druide.
    /// </summary>
    public GameObject DruideGo;

    /// <summary>
    /// GameObject de l’ours.
    /// </summary>
    public GameObject OursGo;

    /// <summary>
    /// GameObject du poisson.
    /// </summary>
    public GameObject FishGo;

    /// <summary>
    /// GameObject de l’oiseau.
    /// </summary>
    public GameObject BirdGo;

    /// <summary>
    /// Référence au script Player du druide.
    /// </summary>
    Player _DruideCs;

    /// <summary>
    /// Référence au script Bird.
    /// </summary>
    Bird _birdCs;

    /// <summary>
    /// Référence au script Bear.
    /// </summary>
    Bear _bearCs;

    /// <summary>
    /// Référence au script Fish.
    /// </summary>
    Fish _fishCs;

    /// <summary>
    /// Entité actuellement dans l’eau.
    /// </summary>
    Entity _entite;

    /// <summary>
    /// Rigidbody2D de l’entité détectée.
    /// </summary>
    Rigidbody2D _rg;

    /// <summary>
    /// Valeur du déplacement horizontal.
    /// </summary>
    float _movex;

    /// <summary>
    /// Valeur du déplacement vertical.
    /// </summary>
    float _movey;

      // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    /// <summary>
    /// Récupère les valeurs de déplacement envoyées par l’Input System.
    /// </summary>
    /// <param name="inputValue">
    /// Valeur envoyée par le système d’Input.
    /// </param>
    void OnMove(InputValue inputValue)
    {
        Vector2 d = inputValue.Get<Vector2>();
        _movex = d.x;
        _movey = d.y;
    }

    /// <summary>
    /// Fonction appelée lorsqu’un objet entre dans l’eau.
    /// </summary>
    /// <param name="collision">
    /// Collider de l’objet qui entre dans le trigger.
    /// </param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerStay2D" + collision.gameObject.name);
        _Entity = collision.gameObject;

        // Vérifie si l’objet détecté est l’ours
        if(_Entity.name == "Ours")
        {
            _bearCs = _Entity.GetComponentInParent<Bear>();
            _entite = _bearCs;
            _entite.IsInWater = true;
        }

        // Vérifie si l’objet détecté est le druide
        if(_Entity.name == "Druide")
        {
            _DruideCs = _Entity.GetComponentInParent<Player>();
            _entite = _DruideCs;
            _entite.IsInWater = true;
        }

        // Vérifie si l’objet détecté est le poisson
        if(_Entity.name == "Poisson")
        {
            _fishCs = _Entity.GetComponentInParent<Fish>();
            _entite = _fishCs;
            _entite.IsInWater = true;
        }

        // Vérifie si l’objet détecté est l’oiseau
        if(_Entity.name == "Oiseau")
        {
            _birdCs = _Entity.GetComponentInParent<Bird>();
            _entite = _birdCs;
            _entite.IsInWater = true;
        }

        // Vérifie si l’objet détecté est l’abeille
        if(_Entity.name == "Abeille")
        {
            _entite = _Entity.GetComponent<Entity>();
            _entite.IsInWater = true;
        }
    }

    /// <summary>
    /// Fonction appelée tant qu’un objet reste dans l’eau.
    /// </summary>
    /// <param name="collision">
    /// Collider de l’objet présent dans le trigger.
    /// </param>
    void OnTriggerStay2D(Collider2D collision)
    {
        _Entity = collision.gameObject;

        // Vérifie si l’objet détecté est l’ours
        if(_Entity.name == "Ours")
        {
            _bearCs = _Entity.GetComponentInParent<Bear>();
            _entite = _bearCs;
            _entite.IsInWater = true;
        }

        // Vérifie si l’objet détecté est le druide
        if(_Entity.name == "Druide")
        {
            _DruideCs = _Entity.GetComponentInParent<Player>();
            _entite = _DruideCs;
            _entite.IsInWater = true;
        }

        // Vérifie si l’objet détecté est le poisson
        if(_Entity.name == "Poisson")
        {
            _fishCs = _Entity.GetComponentInParent<Fish>();
            _entite = _fishCs;
            _entite.IsInWater = true;
        }

        // Vérifie si l’objet détecté est l’oiseau
        if(_Entity.name == "Oiseau")
        {
            _birdCs = _Entity.GetComponentInParent<Bird>();
            _entite = _birdCs;
            _entite.IsInWater = true;
        }
    }

    /// <summary>
    /// Fonction appelée lorsqu’un objet quitte l’eau.
    /// </summary>
    /// <param name="collision">
    /// Collider de l’objet qui quitte le trigger.
    /// </param>
    void OnTriggerExit2D(Collider2D collision)
    {
        // Vérifie si une entité était détectée
        if (_entite != null)
        {
            _entite.IsInWater = false;
            _Entity = null;
            _entite = null;
        }
    }
}