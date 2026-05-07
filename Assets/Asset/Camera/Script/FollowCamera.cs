//Valentin
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    /// <summary>
    /// Cible actuellement suivie par la caméra.
    /// </summary>
    private GameObject _target;

    /// <summary>
    /// GameObject contenant le script GestionPersonnage.
    /// </summary>
    public GameObject player;

    /// <summary>
    /// Référence au script GestionPersonnage.
    /// </summary>
    private GestionPersonnage _gestionPersonnage;

    /// <summary>
    /// Décalage appliqué sur l’axe Z de la caméra.
    /// </summary>
    public int offsetZ = 0;

    /// <summary>
    /// Initialisation des composants nécessaires.
    /// </summary>
    void Start()
    {
        _gestionPersonnage =
            player.GetComponent<GestionPersonnage>();
    }

    /// <summary>
    /// Mise à jour appelée à chaque frame.
    /// Permet de suivre la forme actuellement contrôlée.
    /// </summary>
    void Update()
    {
        // Vérifie si le joueur contrôle le druide
        if(_gestionPersonnage.currentPersonnage ==
           GestionPersonnage.TypePersonnage.Druide)
        {
            _target = _gestionPersonnage.DruideGo;
        }

        // Vérifie si le joueur contrôle l’ours
        else if(_gestionPersonnage.currentPersonnage ==
                GestionPersonnage.TypePersonnage.Bears)
        {
            _target = _gestionPersonnage.BearGo;
        }

        // Vérifie si le joueur contrôle le poisson
        else if(_gestionPersonnage.currentPersonnage ==
                GestionPersonnage.TypePersonnage.Fish)
        {
            _target = _gestionPersonnage.FishGo;
        }

        // Vérifie si le joueur contrôle l’oiseau
        else if(_gestionPersonnage.currentPersonnage ==
                GestionPersonnage.TypePersonnage.Bird)
        {
            _target = _gestionPersonnage.BirdGo;
        }

        // Vérifie si une cible existe
        if(_target == null)
        {
            return;
        }

        // Déplace la caméra sur la position de la cible
        transform.position =
            new Vector3(
                _target.transform.position.x,
                _target.transform.position.y,
                offsetZ
            );
    }
}