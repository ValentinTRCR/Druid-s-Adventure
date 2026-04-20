using UnityEngine;

public class DetectionSolDroite : MonoBehaviour
{   
    public bool estAuSolDroite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collision)
    {
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
    }
}
