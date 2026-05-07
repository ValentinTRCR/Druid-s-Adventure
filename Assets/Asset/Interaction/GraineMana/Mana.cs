using UnityEngine;

public class Mana : MonoBehaviour
{
    /// <summary>
    /// script GameManager
    /// </summary>
    GameManager _gm;

    void Start()
    {
        _gm = GetComponentInParent<GameManager>();
    }
    /// <summary>
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
            _gp.ManaRecuperer();
            _gm.AjoutOrbreMana();
            Destroy(gameObject);
        }
    }
}
