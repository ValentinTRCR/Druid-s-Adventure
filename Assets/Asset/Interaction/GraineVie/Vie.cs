using Unity.VisualScripting;
using UnityEngine;

public class Vie : MonoBehaviour
{
    GameManager gm;

    void Start()
    {
        gm = GetComponentInParent<GameManager>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GestionPersonnage gp = collision.gameObject.GetComponentInParent<GestionPersonnage>();
            gp.VieRecuperer();
            gm.AjoutOrbreVie();
            Destroy(gameObject);
        }
    }
}
