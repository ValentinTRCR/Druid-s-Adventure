using UnityEngine;

public class Levier : MonoBehaviour
{
    /// <summary>
    /// Vérifie si le levier a été activé.
    /// </summary>
    public bool isActivated = false;

    /// <summary>
    /// Animator utilisé pour jouer l’animation du levier.
    /// </summary>
    Animator _anim;

    /// <summary>
    /// Pont contrôlé par le levier.
    /// </summary>
    public GameObject pont;

    /// <summary>
    /// Initialisation des composants nécessaires.
    /// </summary>
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();

        // Désactive l’Animator au démarrage
        _anim.enabled = false;
    }

    /// <summary>
    /// Mise à jour appelée à chaque frame.
    /// Vérifie si le levier a été activé.
    /// </summary>
    void Update()
    {
        // Vérifie si le levier est activé
        if(isActivated)
        {
            // Active l’animation du levier
            _anim.enabled = true;
        }
    }
}