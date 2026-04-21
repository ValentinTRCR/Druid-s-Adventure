using UnityEngine;

public class CanAttack : MonoBehaviour
{
    public bool canAttack = false;
    public Entity entityEnnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Ennemy")
        {
            canAttack = true;
            entityEnnemy = collision.gameObject.GetComponentInParent<Entity>();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ennemy")
        {
            
            canAttack = false;
            entityEnnemy = null;
        }
    }
}
