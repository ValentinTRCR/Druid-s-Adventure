using UnityEngine;

public class DetecterObstacle : MonoBehaviour
{
    /// <summary>
    /// Vérifie si un obstacle est actuellement détecté.
    /// </summary>
    public bool isObstacleDetected;

    /// <summary>
    /// Initialisation des variables.
    /// </summary>
    void Start()
    {
        isObstacleDetected = false;
    }

    /// <summary>
    /// Fonction appelée automatiquement lorsqu’un collider
    /// entre dans la zone de détection.
    /// </summary>
    /// <param name="collision">
    /// Collider détecté par le trigger.
    /// </param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Vérifie que l’objet détecté n’est pas le joueur
        if (!collision.gameObject.CompareTag("Player"))
        {
            // Un obstacle est détecté
            isObstacleDetected = true;
        }
    }

    /// <summary>
    /// Fonction appelée automatiquement lorsqu’un collider
    /// quitte la zone de détection.
    /// </summary>
    /// <param name="collision">
    /// Collider qui quitte le trigger.
    /// </param>
    void OnTriggerExit2D(Collider2D collision)
    {
        // Aucun obstacle détecté
        isObstacleDetected = false;
    }
}