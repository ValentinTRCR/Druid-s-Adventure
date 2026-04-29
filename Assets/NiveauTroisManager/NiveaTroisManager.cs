using UnityEngine;

public class NiveaTroisManager : MonoBehaviour
{
    public GameObject Levier;
    Levier levier;
    public GameObject DepotCrystal;
    DepotCrystal depotCrystal;

    public GameObject portail;
    FinirLeNiveau finirLeNiveau;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levier = Levier.GetComponent<Levier>();
        depotCrystal = DepotCrystal.GetComponentInChildren<DepotCrystal>();
        finirLeNiveau = portail.GetComponent<FinirLeNiveau>();
    }

    // Update is called once per frame
    void Update()
    {
        if(levier.isActivated && depotCrystal.isActivated)
        {
            finirLeNiveau.canBeTake = true;
        }
    }
}
