using UnityEngine;

public class StateVfx : MonoBehaviour
{
    public GameObject Player;
    GestionPersonnage gestionPersonnage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       gestionPersonnage = Player.GetComponent<GestionPersonnage>();
    }

    // Update is called once per frame
    void fini()
    {
        gestionPersonnage.VfxTerminer();
    }
}
