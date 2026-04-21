using UnityEngine;

public class DetectionSolGauche : MonoBehaviour
{
    public bool estAuSolGauche;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D");
        if (collision.gameObject.tag == "Sol")
        {
            estAuSolGauche = true;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sol")
        {
            estAuSolGauche = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("OnTriggerExit2D");
        if (collision.gameObject.tag == "Sol")
        {
            estAuSolGauche = false;
        }
    }
}
