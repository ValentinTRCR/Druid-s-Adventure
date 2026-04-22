using UnityEngine;

public class DetecterEnnemiSol : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player;
    public GameObject sol;
    bool playerIn;

    bool GroundIn;
   

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            playerIn = true;
        }
         if (collision.gameObject.CompareTag("Sol"))
        {
            sol = collision.gameObject;
            GroundIn = true;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
         if (collision.gameObject.CompareTag("Sol"))
        {
            sol = collision.gameObject;
            GroundIn = true;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            playerIn = true;
        }
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = null;
            playerIn = false;
        }
        if (collision.gameObject.CompareTag("Sol"))
        {
            sol = null;
            GroundIn = false;
        }
    }
}
