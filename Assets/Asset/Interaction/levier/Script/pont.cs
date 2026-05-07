using UnityEngine;
using UnityEngine.UIElements;

public class Pont : MonoBehaviour
{
    /// <summary>
    /// Référence au levier contrôlant le pont.
    /// </summary>
    Levier _levier;

    /// <summary>
    /// Collider principal du pont.
    /// </summary>
    BoxCollider2D _boxCollider;

    /// <summary>
    /// Initialisation des composants nécessaires.
    /// </summary>
    void Start()
    {
        _levier = GetComponentInChildren<Levier>();
    }

    /// <summary>
    /// Mise à jour appelée à chaque frame.
    /// Active ou désactive le collider du pont
    /// selon l’état du levier.
    /// </summary>
    void Update()
    {
        // Vérifie si le levier est activé
        if (_levier.isActivated)
        {
            _boxCollider = GetComponent<BoxCollider2D>();

            // Désactive le collider du pont
            _boxCollider.enabled = false;
        }
        else
        {
            _boxCollider = GetComponent<BoxCollider2D>();

            // Active le collider du pont
            _boxCollider.enabled = true;
        }
    }
}