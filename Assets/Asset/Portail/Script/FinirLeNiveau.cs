using System;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinirLeNiveau : MonoBehaviour
{
    /// <summary>
    /// Référence au cristal porté par le joueur.
    /// </summary>
    Transform _crystal;

    /// <summary>
    /// Vérifie si le joueur possède le cristal nécessaire pour finir le niveau.
    /// </summary>
    public bool canBeTake;

    /// <summary>
    /// Vérifie si le niveau est terminé.
    /// </summary>
    bool _isFinish;

    /// <summary>
    /// Nom de la scène du prochain niveau.
    /// </summary>
    public string NomSceneProchaineNiveau;

    /// <summary>
    /// Initialisation des variables.
    /// </summary>
    void Start()
    {
        _isFinish = false;
        canBeTake = false;
    }

    /// <summary>
    /// Retourne l’état de fin du niveau.
    /// </summary>
    /// <returns>
    /// True si le niveau est terminé, sinon false.
    /// </returns>
    public bool IsFinished()
    {
        return _isFinish;
    }

    // Update is called once per frame

    /// <summary>
    /// Fonction appelée lorsqu’une collision commence.
    /// Vérifie si le joueur possède le cristal pour terminer le niveau.
    /// </summary>
    /// <param name="collision">
    /// Collision détectée.
    /// </param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision détectée avec : " + collision.gameObject);

        // Vérifie si l’objet en collision est le joueur
        if (collision.gameObject.CompareTag("Player"))
        {
            // Parcourt les enfants du joueur pour trouver le cristal
            foreach (Transform child in collision.gameObject.GetComponentsInChildren<Transform>())
            {
                if (child.name == "crystal")
                {
                    _crystal = child;
                    break;
                }
            }

            // Vérifie si le cristal a été trouvé
            if (_crystal != null)
            {
                Debug.Log("crystal trouver");
                canBeTake = true;
            }
            else
            {
                Debug.Log("Crystal non trouvé !");
            }

            // Si le joueur possède le cristal, le niveau est terminé
            if (canBeTake)
            {
                GestionPersonnage gp =
                    collision.gameObject.GetComponentInParent<GestionPersonnage>();

                // Sauvegarde les statistiques du joueur
                PlayerStatManager.Instance.SauvegarderDonnee(
                    gp.MaxHealth,
                    gp.RetournerVie(),
                    gp.MaxMana,
                    gp.RetournerMana()
                );

                _isFinish = true;
            }
        }
    }
}