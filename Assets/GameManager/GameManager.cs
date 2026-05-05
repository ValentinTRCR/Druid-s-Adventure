using System.Linq;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int nbrOrbreManaRecuperer;
    int nbrOrbreVieRecuperer;

    public GameObject portail;
    FinirLeNiveau finirLeNiveau;

    //Canvas
    public GameObject vie;
    public GameObject mana;
    public GameObject image;

    // écran de fin 
    public GameObject imageFin;
    public GameObject TextFin;
    public GameObject btnContinuer;

    public GameObject statVie;
    public GameObject statMana;
    
    TextMeshProUGUI statVieText;
    TextMeshProUGUI statManaText;

    public GameObject player;


    

    void Start()
    {
        finirLeNiveau = portail.GetComponent<FinirLeNiveau>();   
        statVieText = statVie.GetComponent<TextMeshProUGUI>();
        statManaText = statMana.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (finirLeNiveau.IsFinished())
        {
            vie.SetActive(false);
            mana.SetActive(false);
            image.SetActive(false);
            imageFin.SetActive(true);
            TextFin.SetActive(true);
            btnContinuer.SetActive(true);

            statVieText.text = "Nombre d'orbre vie rammasser : "+ nbrOrbreVieRecuperer;
            statManaText.text = "Nombre d'obre mana rammasser : " + nbrOrbreManaRecuperer;

            player.SetActive(false);
        }
    }

    public void AjoutOrbreMana()
    {
        nbrOrbreManaRecuperer++;
    }

    public void AjoutOrbreVie()
    {
        nbrOrbreVieRecuperer++;
    }

    public int OrbreVie()
    {
        return nbrOrbreVieRecuperer;
    }

    public int OrbreMana()
    {
        return nbrOrbreManaRecuperer;
    }
}
