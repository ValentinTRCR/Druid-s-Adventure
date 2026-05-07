using Unity.VisualScripting;
using UnityEngine;

public class Vie : MonoBehaviour
{   
    /// <summary>
    /// script GameManager
    /// </summary>
    GameManager _gm;

    void Start()
    {
        _gm = GetComponentInParent<GameManager>();
    }
    
    /// Fonction appelée lorsqu’un objet qui le collider
    /// </summary>
    /// <param name="collision">
    /// Collider de l’objet qui quitte le trigger.
    /// </param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GestionPersonnage _gp = collision.gameObject.GetComponentInParent<GestionPersonnage>();
            _gp.VieRecuperer();
            _gm.AjoutOrbreVie();
            Destroy(gameObject);
        }
    }
}
