using UnityEngine;

public class DetecterEnnemiSol : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /// <summary>
    /// Le GameObject du joueur
    /// </summary>
    public GameObject player;
    /// <summary>
    /// bool pour savoir si le player est dans la zone
    /// </summary>
    bool playerIn;
   
    /// <summary>
    /// Fonction appelée tant qu’un collider entre dans le trigger
    /// </summary>
    /// <param name="collision">Quand le collider détecte une collision</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            playerIn = true;
        }
        
    }
    /// <summary>
    /// Fonction appelée tant qu’un collider reste
    /// dans la zone de détection.
    /// </summary>
    /// <param name="collision">Quand le collider détecte une collision</param>
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            playerIn = true;
        }
    }

    /// <summary>
    /// Fonction appelée quand un collider sort
    /// de la zone de détection.
    /// </summary>
    /// <param name="collision">Quand le collider détecte plus la collision</param>
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = null;
            playerIn = false;
        }
    }
}
