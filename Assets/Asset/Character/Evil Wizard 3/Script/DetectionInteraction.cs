using UnityEngine;

public class DetectionInteraction : MonoBehaviour
{
    public bool IsCollectable;
    public GameObject ObjectToCollect;

    void Start()
    {
        IsCollectable = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Interact")
        {
            Debug.Log("Object detected");
            ObjectToCollect = collision.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Interact")
        {
            ObjectToCollect = null;
        }
    }
}
