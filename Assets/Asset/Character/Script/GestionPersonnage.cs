using TMPro;
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

    //timer
    float timer = 0f;
    float timerMax = 1f;

    public GameObject vie;
    public GameObject mana;    





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPersonnage = TypePersonnage.Druide;
        bear = GetComponent<Bear>();
        Druide = GetComponent<Player>();
        bird = GetComponent<Bird>();
        fish = GetComponent<Fish>();
        animVfx = Vfx.GetComponentInChildren<Animator>();
        currentHealth = PlayerStatManager.Instance.health;
        currentMana = PlayerStatManager.Instance.mana;
        MaxHealth = PlayerStatManager.Instance.maxHealth;
        MaxMana = PlayerStatManager.Instance.maxMana;
        Vfx.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentMana <= 0)
        {
            currentPersonnage = TypePersonnage.Druide;
            currentMana += 1;
            DruideGo.transform.position = new Vector2(positionActuelle.x, positionActuelle.y);

            Vfx.SetActive(true);
            Vfx.transform.position = new Vector2(VfxPositionDruide.transform.position.x, VfxPositionDruide.transform.position.y);
        }
        switch (currentPersonnage)
        {
            case TypePersonnage.Druide:
                //récupérer position actuelle
                positionActuelle = DruideGo.transform.position;
                //StatDeVie

                Druide.currentHealth = currentHealth;

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
        timer += Time.deltaTime;
        if (timer >= timerMax)
        {
            //Debug.Log("une seconde");
            if (currentPersonnage == TypePersonnage.Bears)
            {
                currentMana  -= 6;
            }
            else if (currentPersonnage == TypePersonnage.Fish)
            {
                currentMana -= 8;
            }
            else if (currentPersonnage == TypePersonnage.Bird)
            {
                currentMana -= 8;
            }else if(currentPersonnage == TypePersonnage.Druide && currentMana != MaxMana)
            {
                currentMana += 1;
            }
            timer = 0;
        }
        afficherVieEtMana();
        PlayerStatManager.Instance.SaveStats(currentHealth,currentMana,MaxHealth,MaxMana);
    }

    // sers à mettre a jour depuis la classe entity
    public void MettreAjourVieEtMana(int health)
    {
        currentHealth = health;

        if (currentHealth > MaxHealth)
        {
            int difference = currentHealth - MaxHealth;
            MaxHealth += difference;
        }
        if (currentMana > MaxMana)
        {
            int difference = currentMana - MaxMana;
            MaxMana += difference;
        }
    }

    public void ManaRecuperer()
    {
        currentMana += 20;
    }

    void OnChangeDruide(InputValue inputValue)
    {
        if (inputValue.isPressed && TypePersonnage.Druide != currentPersonnage)
        {

            currentPersonnage = TypePersonnage.Druide;
            DruideGo.transform.position = new Vector2(positionActuelle.x, positionActuelle.y);

            Vfx.SetActive(true);
            Vfx.transform.position = new Vector2(VfxPositionDruide.transform.position.x, VfxPositionDruide.transform.position.y);
        }
    }

    void OnChangeBear(InputValue inputValue)
    {
        if (inputValue.isPressed && TypePersonnage.Bears != currentPersonnage && currentMana >= 6)
        {
            currentPersonnage = TypePersonnage.Bears;
            BearGo.transform.position = new Vector2(positionActuelle.x, positionActuelle.y);

            Vfx.SetActive(true);
            Vfx.transform.position = VfxPositionBear.transform.position;
        }
    }

    void OnChangeFish(InputValue inputValue)
    {
        if (inputValue.isPressed && TypePersonnage.Fish != currentPersonnage && currentMana >= 5)
        {
            currentPersonnage = TypePersonnage.Fish;
            FishGo.transform.position = new Vector2(positionActuelle.x, positionActuelle.y);

            Vfx.SetActive(true);
            Vfx.transform.position = VfxPositionFish.transform.position;
        }
    }

    void OnChangeBird(InputValue inputValue)
    {
        if (inputValue.isPressed && TypePersonnage.Bird != currentPersonnage && currentMana >= 8)
        {
            currentPersonnage = TypePersonnage.Bird;
            BirdGo.transform.position = new Vector2(positionActuelle.x, positionActuelle.y);

            Vfx.SetActive(true);
            Vfx.transform.position = VfxPositionBird.transform.position;
        }
    }

    public void VfxTerminer()
    {
        Vfx.SetActive(false);
    }

    //il sert à afficher la vie pout l'ui
    void afficherVieEtMana()
    {
        vie.GetComponent<TextMeshProUGUI>().text = currentHealth + " / " + MaxHealth;
        mana.GetComponent<TextMeshProUGUI>().text = currentMana + " / " + MaxMana;
    }
}
