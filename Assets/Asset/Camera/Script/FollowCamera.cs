using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private GameObject target; 
    public GameObject player;
    GestionPersonnage gestionPersonnage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gestionPersonnage = player.GetComponent<GestionPersonnage>();
    }

    // Update is called once per frame
    void Update()
    {

        if(gestionPersonnage.currentPersonnage == GestionPersonnage.TypePersonnage.Druide)
        {
            target = gestionPersonnage.DruideGo;
        }
        else if(gestionPersonnage.currentPersonnage == GestionPersonnage.TypePersonnage.Bears)
        {
            target = gestionPersonnage.BearGo;
        }
        else if(gestionPersonnage.currentPersonnage == GestionPersonnage.TypePersonnage.Fish)
        {
            target = gestionPersonnage.FishGo;
        }
        else if(gestionPersonnage.currentPersonnage == GestionPersonnage.TypePersonnage.Bird)
        {
            target = gestionPersonnage.BirdGo;
        }

        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, -10);
    }
}
