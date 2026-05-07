using UnityEngine;

public class DetectionSol : MonoBehaviour
{
    /// <summary>
    /// Vérifie si le joueur touche actuellement le sol.
    /// </summary>
    public bool ToucheLeSol;

    /// <summary>
    /// Fonction appelée automatiquement lorsqu’un collider
    /// entre dans la zone de détection.
    /// </summary>
    /// <param name="collision">
    /// Collider détecté par le trigger.
    /// </param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Vérifie si l’objet touché possède le tag "Sol"
        if (collision.CompareTag("Sol"))
        {
            // Le joueur touche le sol
            ToucheLeSol = true;
        }
    }

    /// <summary>
    /// Fonction appelée automatiquement lorsqu’un collider
    /// quitte la zone de détection.
    /// </summary>
    /// <param name="collision">
    /// Collider qui quitte le trigger.
    /// </param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Vérifie si l’objet quitté possède le tag "Sol"
        if (collision.CompareTag("Sol"))
        {
            // Le joueur ne touche plus le sol
            ToucheLeSol = false;
        }
    }
}