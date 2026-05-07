using UnityEngine;

public class DetectionSolDroite : MonoBehaviour
{
    /// <summary>
    /// Vérifie si un sol est détecté à droite du joueur.
    /// </summary>
    public bool estAuSolDroite;

    /// <summary>
    /// Vérifie si un mur bloque le déplacement.
    /// </summary>
    public bool bloquer;

    /// <summary>
    /// Fonction appelée automatiquement lorsqu’un collider
    /// entre dans la zone de détection.
    /// </summary>
    /// <param name="collision">
    /// Collider détecté par le trigger.
    /// </param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Vérifie si l’objet détecté est un mur
        if(collision.gameObject.tag == "Wall")
        {
            bloquer = true;
        }

        // Vérifie si l’objet détecté est le sol
        if (collision.gameObject.tag == "Sol")
        {
            estAuSolDroite = true;
        }
    }

    /// <summary>
    /// Fonction appelée tant qu’un collider reste
    /// dans la zone de détection.
    /// </summary>
    /// <param name="collision">
    /// Collider détecté par le trigger.
    /// </param>
    void OnTriggerStay2D(Collider2D collision)
    {
        // Vérifie si l’objet détecté est un mur
        if(collision.gameObject.tag == "Wall")
        {
            bloquer = true;
        }

        // Vérifie si l’objet détecté est le sol
        if (collision.gameObject.tag == "Sol")
        {
            estAuSolDroite = true;
        }
    }

    /// <summary>
    /// Fonction appelée lorsqu’un collider
    /// quitte la zone de détection.
    /// </summary>
    /// <param name="collision">
    /// Collider qui quitte le trigger.
    /// </param>
    void OnTriggerExit2D(Collider2D collision)
    {
        // Vérifie si le collider quitté est le sol
        if (collision.gameObject.tag == "Sol")
        {
            estAuSolDroite = false;
        }

        // Vérifie si le collider quitté est un mur
        if(collision.gameObject.tag == "Wall")
        {
            bloquer = false;
        }
    }
}