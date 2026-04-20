using UnityEngine;
using UnityEngine.InputSystem;

public class Water : MonoBehaviour
{
    GameObject player;

    GestionPersonnage gp;

    public GameObject DruideGo;
    public GameObject OursGo;
    public GameObject FishGo;
    public GameObject BirdGo;
    Player DruideCs;

    Bird birdCs;

    Bear bearCs;

    Fish fishCs;

    Entity entite;
    Rigidbody2D rg;
    float movex;
    float movey;

      // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnMove(InputValue inputValue)
    {
        Vector2 d = inputValue.Get<Vector2>();
        movex = d.x;
        movey = d.y;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerStay2D" + collision.gameObject.name);
        player = collision.gameObject;
        if(player.name == "BearsObject")
        {
            bearCs = player.GetComponentInParent<Bear>();
            entite = bearCs;
            entite.IsInWater = true;
        }
        if(player.name == "WizardObject")
        {
            DruideCs = player.GetComponentInParent<Player>();
            entite = DruideCs;
            entite.IsInWater = true;
        }
        if(player.name == "FishObject")
        {
            fishCs = player.GetComponentInParent<Fish>();
            entite = fishCs;
            entite.IsInWater = true;
        }
        if(player.name == "BirdsObject")
        {
            birdCs = player.GetComponentInParent<Bird>();
            entite = birdCs;
            entite.IsInWater = true;
        }


        

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        
        if (entite != null)
        {
            entite.IsInWater = false;
            player = null;
            entite = null;
        }
    }
}
