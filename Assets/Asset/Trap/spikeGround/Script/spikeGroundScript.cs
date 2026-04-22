using UnityEngine;

public class spikeGroundScript : MonoBehaviour
{
    bool playerIn;
    private SpikeAnimationState spikeAnimationState;

    bool hurt = false;

    Entity entity;

    

    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spikeAnimationState = GetComponentInChildren<SpikeAnimationState>();
        
    }

    // Update is called once per frame
   
    public void EnleverDegat()
    {
        if(entity != null)
        {
            entity.TakeDamage(1);
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(50, 5), ForceMode2D.Impulse);
            hurt = true;
        }else
        {
            Debug.Log("Player est null");
        }
        
        
    }

    void Hurt()
    {
        hurt = true;
    }

    public void ResetHurt()
    {
        hurt = false;
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
      
        Debug.Log("OnTriggerEnter2D" + collision.gameObject.name);
        player = collision.gameObject;
        if(player.name == "Ours")
        {
            entity = player.GetComponentInParent<Bear>();
            
        }
        if(player.name == "Druide")
        {
            entity = player.GetComponentInParent<Player>();
           
        }
        if(player.name == "Poisson")
        {
            entity = player.GetComponentInParent<Fish>();
            
        }
        if(player.name == "Oiseau")
        {
            entity = player.GetComponentInParent<Bird>();
            
        }

        if (entity != null && entity.Hurt == false)
        {
            Debug.Log("Player touché par les piques");
            EnleverDegat();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("OnTriggerExit2D" + collision.gameObject.name);
        player = null;
        entity = null;
        
    }
}
