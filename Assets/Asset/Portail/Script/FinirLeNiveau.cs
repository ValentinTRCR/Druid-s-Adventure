using System;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinirLeNiveau : MonoBehaviour
{
    Transform crystal;
    public bool canBeTake;

    bool isFinish;

    public string NomSceneProchaineNiveau;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isFinish = false;
        canBeTake = false;
    }

    public bool IsFinished()
    {
        return isFinish;
    }

    // Update is called once per frame

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision détectée avec : " + collision.gameObject);
        if (collision.gameObject.CompareTag("Player"))
        {

            foreach (Transform child in collision.gameObject.GetComponentsInChildren<Transform>())
            {

                if (child.name == "crystal")
                {
                    crystal = child;
                    break;
                }
            }

            if (crystal != null)
            {
                Debug.Log("crystal trouver");
                canBeTake = true;
            }
            else
            {
                Debug.Log("Crystal non trouvé !");
            }



            if (canBeTake)
            {
                GestionPersonnage gp = collision.gameObject.GetComponentInParent<GestionPersonnage>();
                PlayerStatManager.Instance.SauvegarderDonnee(gp.MaxHealth, gp.RetournerVie(), gp.MaxMana, gp.RetournerMana());
                isFinish = true;
            }


        }
    }
}
