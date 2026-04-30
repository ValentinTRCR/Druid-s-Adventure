using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Brick : Entity
{
    // Start is called once before
    // the first execution of Update after the MonoBehaviour is created

    private List<GameObject> bricksSprites;
    private Transform[] bricksSpritesTransforms;
    void Start()
    {
        bricksSpritesTransforms = gameObject.GetComponentsInChildren<Transform>();
        bricksSprites = new List<GameObject>();
        foreach (var item in bricksSpritesTransforms)
        {
            
            bricksSprites.Add(item.gameObject);
            if(item.gameObject != gameObject) // Vérifie que ce n'est pas le parent
            {
                item.GetComponent<Animator>().enabled = false; // Désactive les animations au départ
            }
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(IsDead)
        {
            DestructionBrick();
        }
    }

    void DestructionBrick()
    {
        foreach (var item in bricksSprites)
        {

            if(item.gameObject != gameObject) // Vérifie que ce n'est pas le parent
            {
                item.GetComponent<Animator>().enabled = true; // Désactive les animations au départ
            } // Active les animations
             // Appelle la fonction de destruction après un délai
        }
        Invoke("DestroyBrick", 0.5f);

    }

    void DestroyBrick()
    {
        foreach (var item in bricksSprites)
        {
            Destroy(item); // Détruit les sprites de la brique
        }
       
    }
}
