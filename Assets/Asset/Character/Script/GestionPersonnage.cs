using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using Unity.Collections.LowLevel.Unsafe;
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
    public int MaxHealth = 100;
    private int currentHealth;
    public int MaxMana = 100;
    private int currentMana;

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

    private Fish fish;

    Vector2 positionActuelle;

    Animator animVfx;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPersonnage = TypePersonnage.Bird;
        bear = GetComponent<Bear>();
        Druide = GetComponent<Player>();
        bird = GetComponent<Bird>();
        fish = GetComponent<Fish>();
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
                //StatDeVie

                Druide.currentHealth = currentHealth;

                Druide.currentMana = currentMana;
                //script
                Druide.enabled = true;
                bear.enabled = false;
                bird.enabled = false;
                fish.enabled = false;
                //gameObject
                DruideGo.SetActive(true);
                BearGo.SetActive(false);
                FishGo.SetActive(false);
                BirdGo.SetActive(false);

                break;
            case TypePersonnage.Bears:
                //récupérer position actuelle
                positionActuelle = BearGo.transform.position;
                //StatDeVie

                bear.currentHealth = currentHealth;

                bear.currentMana = currentMana;
                //script
                Druide.enabled = false;
                bear.enabled = true;
                bird.enabled = false;
                fish.enabled = false;
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
                //Stat de vie et de mana
                fish.currentHealth = currentHealth;
                fish.currentMana = currentMana;
                // script
                Druide.enabled = false;
                bear.enabled = false;
                bird.enabled = false;
                fish.enabled = true;
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
                //Stat de vie et de mana

                bird.currentHealth = currentHealth;

                bird.currentMana = currentMana;
                //script
                bird.enabled = true;
                Druide.enabled = false;
                bear.enabled = false;
                fish.enabled = false;
                //GameObject
                BirdGo.SetActive(true);
                FishGo.SetActive(false);
                BearGo.SetActive(false);
                DruideGo.SetActive(false);
                // Handle Bird specific logic
                break;
        }
    }

    public void MettreAjourVieEtMana(int health,int mana)
    {
        currentHealth = health;
        currentMana = mana;

        if(currentHealth > MaxHealth)
        {
            int difference = currentHealth - MaxHealth;
            MaxHealth += difference;
        }
        if(currentMana > MaxMana)
        {
            int difference = currentMana - MaxMana;
            MaxMana += difference;
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
