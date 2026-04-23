using UnityEngine;

public class DetectionSolDroite : MonoBehaviour
{   
    public bool estAuSolDroite;
    public bool bloquer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
           bloquer = true;
        }
        if (collision.gameObject.tag == "Sol")
        {
            estAuSolDroite = true;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
         if(collision.gameObject.tag == "Wall")
        {

            bloquer = true;
        }
        if (collision.gameObject.tag == "Sol")
        {
            estAuSolDroite = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sol")
        {
            estAuSolDroite = false;
        }
        if(collision.gameObject.tag == "Wall")
        {
            bloquer = false;
        }
    }
}
