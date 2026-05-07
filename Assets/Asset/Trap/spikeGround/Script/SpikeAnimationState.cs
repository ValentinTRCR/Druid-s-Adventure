using UnityEngine;
using System.Collections;

public class SpikeAnimationState : MonoBehaviour
{
    /// <summary>
    /// Vérifie si les pics sont actuellement en attaque.
    /// </summary>
    public bool attack = false;

    /// <summary>
    /// Animator utilisé pour gérer les animations des pics.
    /// </summary>
    Animator _animator;

    /// <summary>
    /// Référence au script principal des pics.
    /// </summary>
    spikeGroundScript _spikeGroundScript;

    /// <summary>
    /// Collider utilisé pour infliger des dégâts.
    /// </summary>
    BoxCollider2D _boxCollider2D;

    /// <summary>
    /// Initialisation des composants nécessaires.
    /// </summary>
    void Start()
    {
        _animator = GetComponent<Animator>();

        // Lance la boucle d’attaque des pics
        StartCoroutine(SpikeLoop());

        _spikeGroundScript =
            GetComponentInParent<spikeGroundScript>();

        _boxCollider2D =
            GetComponentInParent<BoxCollider2D>();
    }

    /// <summary>
    /// Coroutine permettant de répéter l’attaque des pics.
    /// </summary>
    IEnumerator SpikeLoop()
    {
        while (true)
        {
            // Lance l’animation d’attaque
            _animator.SetTrigger("Attack");

            // Attend 2 secondes avant la prochaine attaque
            yield return new WaitForSeconds(2f);
        }
    }

    /// <summary>
    /// Active le collider des pics.
    /// Fonction appelée via un Animation Event.
    /// </summary>
    public void AttaqueTrue()
    {
        _boxCollider2D.enabled = true;
    }

    /// <summary>
    /// Désactive le collider des pics
    /// et réinitialise l’animation.
    /// Fonction appelée via un Animation Event.
    /// </summary>
    public void AttaqueFalse()
    {
        // Stop temporairement l’Animator
        _animator.enabled = false;

        // Désactive le collider
        _boxCollider2D.enabled = false;

        attack = false;

        // Réactive l’Animator
        _animator.enabled = true;
    }

    /// <summary>
    /// Réinitialise l’état de dégâts des pics.
    /// Fonction appelée via un Animation Event.
    /// </summary>
    public void ResetHurt()
    {
        _spikeGroundScript.ResetHurt();
    }
}