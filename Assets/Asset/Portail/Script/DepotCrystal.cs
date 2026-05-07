using UnityEngine;

public class DepotCrystal : MonoBehaviour
{
    /// <summary>
    /// Vérifie si le dépôt de cristal est activé.
    /// </summary>
    public bool isActivated = false;

    /// <summary>
    /// Animator utilisé pour jouer l’animation du dépôt.
    /// </summary>
    Animator _anim;

    /// <summary>
    /// Initialisation des composants nécessaires.
    /// </summary>
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();

        // Désactive l’animation au démarrage
        _anim.enabled = false;
    }

    /// <summary>
    /// Mise à jour appelée à chaque frame.
    /// Vérifie si un cristal est présent dans le dépôt.
    /// </summary>
    void Update()
    {
        // Recherche le cristal dans les enfants du dépôt
        Transform crystal = transform.Find("crystal");

        // Vérifie si un cristal est présent
        if(crystal != null)
        {
            // Replace le cristal à la position prévue
            crystal.position =
                transform.Find("positionCrystal").position;

            // Active l’animation du dépôt
            _anim.enabled = true;

            // Active le rendu visuel du cristal
            crystal
                .gameObject
                .GetComponentInChildren<SpriteRenderer>()
                .enabled = true;

            // Active le dépôt
            isActivated = true;
        }
    }
}