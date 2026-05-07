using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Brick : Entity
{
    // Start is called once before
    // the first execution of Update after the MonoBehaviour is created

    /// <summary>
    /// Liste contenant les GameObjects des sprites de la brique.
    /// </summary>
    private List<GameObject> _bricksSprites;

    /// <summary>
    /// Tableau contenant tous les Transform enfants de la brique.
    /// </summary>
    private Transform[] _bricksSpritesTransforms;

    /// <summary>
    /// Initialisation des sprites de la brique.
    /// </summary>
    void Start()
    {
        _bricksSpritesTransforms = gameObject.GetComponentsInChildren<Transform>();
        _bricksSprites = new List<GameObject>();

        foreach (Transform item in _bricksSpritesTransforms)
        {
            _bricksSprites.Add(item.gameObject);

            if(item.gameObject != gameObject) // Vérifie que ce n'est pas le parent
            {
                item.GetComponent<Animator>().enabled = false; // Désactive les animations au départ
            }       
        }
    }

    /// <summary>
    /// Mise à jour appelée à chaque frame.
    /// Vérifie si la brique est détruite.
    /// </summary>
    void Update()
    {
        if(IsDead)
        {
            DestructionBrick();
        }
    }

    /// <summary>
    /// Active les animations de destruction
    /// des différents morceaux de la brique.
    /// </summary>
    void DestructionBrick()
    {
        foreach (var item in _bricksSprites)
        {
            if(item.gameObject != gameObject) // Vérifie que ce n'est pas le parent
            {
                item.GetComponent<Animator>().enabled = true; // Active les animations de destruction
            }
        }

        // Détruit les morceaux après un court délai
        Invoke("DestroyBrick", 0.5f);
    }

    /// <summary>
    /// Détruit tous les sprites de la brique.
    /// </summary>
    void DestroyBrick()
    {
        foreach (var item in _bricksSprites)
        {
            Destroy(item); // Détruit les sprites de la brique
        }
    }
}