using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

public class GestionPersonnage : MonoBehaviour
{
    public enum TypePersonnage
    {
        Druide,
        Bears,
        Fish,
        Bird
    }

    public TypePersonnage currentPersonnage;

    public GameObject BearGo;
    public GameObject DruideGo;
    public GameObject FishGo;
    public GameObject BirdGo;

    public GameObject VfxPositionBear;
    public GameObject VfxPositionDruide;
    public GameObject VfxPositionFish;
    public GameObject VfxPositionBird;



    public GameObject Vfx;

    private Bear bear;
    private Player Druide;

    private Bird bird;

    Vector2 positionActuelle;

    Animator animVfx;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPersonnage = TypePersonnage.Bird;
        bear = GetComponent<Bear>();
        Druide = GetComponent<Player>();
        bird = GetComponent<Bird>();
        animVfx = Vfx.GetComponentInChildren<Animator>();
        Vfx.SetActive(false);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentPersonnage)
        {
            case TypePersonnage.Druide:
                //récupérer position actuelle
                positionActuelle = DruideGo.transform.position;
                //script
                Druide.enabled = true;
                bear.enabled = false;
                bird.enabled = false;
                //gameObject
                DruideGo.SetActive(true);
                BearGo.SetActive(false);
                FishGo.SetActive(false);
                BirdGo.SetActive(false);

                break;
            case TypePersonnage.Bears:
                //récupérer position actuelle
                positionActuelle = BearGo.transform.position;
                //script
                Druide.enabled = false;
                bear.enabled = true;
                bird.enabled = false;
                //gameObject
                BearGo.SetActive(true);
                DruideGo.SetActive(false);
                FishGo.SetActive(false);
                BirdGo.SetActive(false);


                // Handle Bears specific logic
                break;
            case TypePersonnage.Fish:
                //récupérer position actuelle
                positionActuelle = FishGo.transform.position;
                // script
                Druide.enabled = false;
                bear.enabled = false;
                bird.enabled = false;
                // GameObject
                FishGo.SetActive(true);
                BearGo.SetActive(false);
                BirdGo.SetActive(false);
                DruideGo.SetActive(false);
                // Handle Fish specific logic
                break;
            case TypePersonnage.Bird:
                //récupérer position actuelle
                positionActuelle = BirdGo.transform.position;
                //script
                bird.enabled = true;
                Druide.enabled = false;
                bear.enabled = false;
                //GameObject
                BirdGo.SetActive(true);
                FishGo.SetActive(false);
                BearGo.SetActive(false);
                DruideGo.SetActive(false);
                // Handle Bird specific logic
                break;
        }
    }

    void OnChangeDruide(InputValue inputValue)
    {
        if (inputValue.isPressed && TypePersonnage.Druide != currentPersonnage)
        {
            currentPersonnage = TypePersonnage.Druide;
            DruideGo.transform.position = new Vector2(positionActuelle.x,positionActuelle.y);
            
            Vfx.SetActive(true);
            Vfx.transform.position = new Vector2(VfxPositionDruide.transform.position.x, VfxPositionDruide.transform.position.y);
        }
    }

    void OnChangeBear(InputValue inputValue)
    {
         if (inputValue.isPressed && TypePersonnage.Bears != currentPersonnage)
        {
            currentPersonnage = TypePersonnage.Bears;
            BearGo.transform.position =  new Vector2(positionActuelle.x,positionActuelle.y);
            
            Vfx.SetActive(true);
            Vfx.transform.position = VfxPositionBear.transform.position;
        }
    }

    void OnChangeFish(InputValue inputValue)
    {
         if (inputValue.isPressed && TypePersonnage.Fish != currentPersonnage)
        {
            currentPersonnage = TypePersonnage.Fish;
            FishGo.transform.position =  new Vector2(positionActuelle.x,positionActuelle.y);
            
            Vfx.SetActive(true);
            Vfx.transform.position = VfxPositionFish.transform.position;
        }
    }

    void OnChangeBird(InputValue inputValue)
    {
         if (inputValue.isPressed && TypePersonnage.Bird != currentPersonnage)
        {
            currentPersonnage = TypePersonnage.Bird;
            BirdGo.transform.position =  new Vector2(positionActuelle.x,positionActuelle.y);
           
            Vfx.SetActive(true);
            Vfx.transform.position = VfxPositionBird.transform.position;
        }
    }

    public void VfxTerminer()
    {
        Vfx.SetActive(false);
    }
}
