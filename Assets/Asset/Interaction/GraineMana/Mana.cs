using UnityEngine;

public class Mana : MonoBehaviour
{
    GameManager gm;

    void Start()
    {
        gm = GetComponentInParent<GameManager>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GestionPersonnage gp = collision.gameObject.GetComponentInParent<GestionPersonnage>();
            gp.ManaRecuperer();
            gm.AjoutOrbreMana();
            Destroy(gameObject);
        }
    }
}
