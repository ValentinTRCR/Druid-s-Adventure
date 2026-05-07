using UnityEngine;

public class DetectionInteraction : MonoBehaviour
{
    /// <summary>
    /// Vérifie si un objet interactif est détecté.
    /// </summary>
    public bool IsCollectable;

    /// <summary>
    /// Objet interactif actuellement détecté par le joueur.
    /// </summary>
    public GameObject ObjectToCollect;

    /// <summary>
    /// Initialisation des variables.
    /// </summary>
    void Start()
    {
        IsCollectable = false;
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
        // Vérifie si l’objet possède le tag "Interact"
        if(collision.gameObject.tag == "Interact")
        {
            Debug.Log(
                "Object " +
                collision.gameObject.name +
                " detected"
            );

            // Sauvegarde l’objet détecté
            ObjectToCollect = collision.gameObject;
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
        // Vérifie si l’objet quitté possède le tag "Interact"
        if(collision.gameObject.tag == "Interact")
        {
            // Réinitialise l’objet interactif détecté
            ObjectToCollect = null;
        }
    }
}