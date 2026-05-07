
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Nombre d’orbes de mana récupérés pendant le niveau.
    /// </summary>
    int _nbrOrbreManaRecuperer;

    /// <summary>
    /// Nombre d’orbes de vie récupérés pendant le niveau.
    /// </summary>
    int _nbrOrbreVieRecuperer;

    /// <summary>
    /// Portail permettant de terminer le niveau.
    /// </summary>
    public GameObject portail;

    /// <summary>
    /// Référence au script de fin de niveau.
    /// </summary>
    FinirLeNiveau _finirLeNiveau;

    //Canvas

    /// <summary>
    /// Élément UI affichant la vie.
    /// </summary>
    public GameObject vie;

    /// <summary>
    /// Élément UI affichant la mana.
    /// </summary>
    public GameObject mana;

    /// <summary>
    /// Image principale de l’interface.
    /// </summary>
    public GameObject image;

    // écran de fin 

    /// <summary>
    /// Image affichée à la fin du niveau.
    /// </summary>
    public GameObject imageFin;

    /// <summary>
    /// Texte affiché à la fin du niveau.
    /// </summary>
    public GameObject TextFin;

    /// <summary>
    /// Bouton permettant de continuer après la fin du niveau.
    /// </summary>
    public GameObject btnContinuer;

    /// <summary>
    /// Texte affichant le nombre d’orbes de vie récupérés.
    /// </summary>
    public GameObject statVie;

    /// <summary>
    /// Texte affichant le nombre d’orbes de mana récupérés.
    /// </summary>
    public GameObject statMana;
    
    /// <summary>
    /// Composant TextMeshPro du texte des orbes de vie.
    /// </summary>
    TextMeshProUGUI _statVieText;

    /// <summary>
    /// Composant TextMeshPro du texte des orbes de mana.
    /// </summary>
    TextMeshProUGUI _statManaText;

    /// <summary>
    /// GameObject du joueur.
    /// </summary>
    public GameObject player;

    private GestionPersonnage gp;


    

    /// <summary>
    /// Initialisation des composants nécessaires.
    /// </summary>
    void Start()
    {
        _finirLeNiveau = portail.GetComponent<FinirLeNiveau>();   
        _statVieText = statVie.GetComponent<TextMeshProUGUI>();
        _statManaText = statMana.GetComponent<TextMeshProUGUI>();
        gp = player.GetComponent<GestionPersonnage>();
    }

    /// <summary>
    /// Mise à jour appelée à chaque frame.
    /// Vérifie si le niveau est terminé.
    /// </summary>
    void Update()
    {
        // Si le niveau est terminé, affiche l’écran de fin
        if (_finirLeNiveau.IsFinished())
        {
            vie.SetActive(false);
            mana.SetActive(false);
            image.SetActive(false);
            imageFin.SetActive(true);
            TextFin.SetActive(true);
            btnContinuer.SetActive(true);

            _statVieText.text =
                "Nombre d'orbre vie rammasser : " +
                _nbrOrbreVieRecuperer;

            _statManaText.text =
                "Nombre d'obre mana rammasser : " +
                _nbrOrbreManaRecuperer;

            player.SetActive(false);
        }
        if (gp.IsDead())
        {
            vie.SetActive(false);
            mana.SetActive(false);
            image.SetActive(false);
            imageFin.SetActive(true);
            TextFin.SetActive(true);
            btnContinuer.SetActive(true);

            TextFin.GetComponent<TextMeshProUGUI>().text = "";

            _statVieText.text = "Vous êtes mort ";

            _statManaText.text ="";
            Debug.Log("mort");
        }
    }

    /// <summary>
    /// Ajoute une orbe de mana au compteur.
    /// </summary>
    public void AjoutOrbreMana()
    {
        _nbrOrbreManaRecuperer++;
    }

    /// <summary>
    /// Ajoute une orbe de vie au compteur.
    /// </summary>
    public void AjoutOrbreVie()
    {
        _nbrOrbreVieRecuperer++;
    }

    /// <summary>
    /// Retourne le nombre d’orbes de vie récupérés.
    /// </summary>
    /// <returns>
    /// Nombre d’orbes de vie.
    /// </returns>
    public int OrbreVie()
    {
        return _nbrOrbreVieRecuperer;
    }

    /// <summary>
    /// Retourne le nombre d’orbes de mana récupérés.
    /// </summary>
    /// <returns>
    /// Nombre d’orbes de mana.
    /// </returns>
    public int OrbreMana()
    {
        return _nbrOrbreManaRecuperer;
    }
}