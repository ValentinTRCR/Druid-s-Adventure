using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GestionPersonnage : MonoBehaviour
{
    /// <summary>
    /// Liste des différentes formes disponibles pour le personnage.
    /// </summary>
    public enum TypePersonnage
    {
        Druide,
        Bears,
        Fish,
        Bird
    }

    /// <summary>
    /// Vie maximale du personnage.
    /// </summary>
    public int MaxHealth = 100;

    /// <summary>
    /// Vie actuelle du personnage.
    /// </summary>
    private int _currentHealth;

    /// <summary>
    /// Mana maximale du personnage.
    /// </summary>
    public int MaxMana = 100;

    /// <summary>
    /// Mana actuelle du personnage.
    /// </summary>
    private int _currentMana;

    /// <summary>
    /// Forme actuellement utilisée par le joueur.
    /// </summary>
    public TypePersonnage currentPersonnage;

    /// <summary>
    /// GameObject de la forme ours.
    /// </summary>
    public GameObject BearGo;

    /// <summary>
    /// GameObject de la forme druide.
    /// </summary>
    public GameObject DruideGo;

    /// <summary>
    /// GameObject de la forme poisson.
    /// </summary>
    public GameObject FishGo;

    /// <summary>
    /// GameObject de la forme oiseau.
    /// </summary>
    public GameObject BirdGo;

    /// <summary>
    /// Position du VFX pour la transformation en ours.
    /// </summary>
    public GameObject VfxPositionBear;

    /// <summary>
    /// Position du VFX pour la transformation en druide.
    /// </summary>
    public GameObject VfxPositionDruide;

    /// <summary>
    /// Position du VFX pour la transformation en poisson.
    /// </summary>
    public GameObject VfxPositionFish;

    /// <summary>
    /// Position du VFX pour la transformation en oiseau.
    /// </summary>
    public GameObject VfxPositionBird;

    /// <summary>
    /// Effet visuel joué lors d’une transformation.
    /// </summary>
    public GameObject Vfx;

    /// <summary>
    /// Référence au script de l’ours.
    /// </summary>
    private Bear _bear;

    /// <summary>
    /// Référence au script du druide.
    /// </summary>
    private Player _Druide;

    /// <summary>
    /// Référence au script de l’oiseau.
    /// </summary>
    private Bird _bird;

    /// <summary>
    /// Référence au script du poisson.
    /// </summary>
    private Fish _fish;

    /// <summary>
    /// Position actuelle du personnage avant une transformation.
    /// </summary>
    Vector2 _positionActuelle;

    /// <summary>
    /// Animator du VFX de transformation.
    /// </summary>
    Animator _animVfx;

    //timer

    /// <summary>
    /// Timer utilisé pour gérer la consommation ou la récupération de mana.
    /// </summary>
    float _timer = 0f;

    /// <summary>
    /// Temps maximum avant de modifier la mana.
    /// </summary>
    float _timerMax = 1f;

    /// <summary>
    /// Texte affichant la vie dans l’interface.
    /// </summary>
    public GameObject vie;

    /// <summary>
    /// Texte affichant la mana dans l’interface.
    /// </summary>
    public GameObject mana;  

    private bool _isDead;  





    /// <summary>
    /// Initialisation des formes, des statistiques et du VFX.
    /// </summary>
    void Start()
    {
        currentPersonnage = TypePersonnage.Druide;
        _bear = GetComponent<Bear>();
        _Druide = GetComponent<Player>();
        _bird = GetComponent<Bird>();
        _fish = GetComponent<Fish>();
        _animVfx = Vfx.GetComponentInChildren<Animator>();
        _currentHealth = PlayerStatManager.Instance.health;
        _currentMana = PlayerStatManager.Instance.mana;
        MaxHealth = PlayerStatManager.Instance.maxHealth;
        MaxMana = PlayerStatManager.Instance.maxMana;
        if(_currentHealth == 0)
        {
            _currentHealth = MaxHealth;
        }
        Vfx.SetActive(false);
    }

    /// <summary>
    /// Mise à jour appelée à chaque frame.
    /// Gère les transformations, la mana, l’interface et la sauvegarde des statistiques.
    /// </summary>
    void Update()
    {
        if(_currentHealth <= 0)
        {
            _isDead = true;
        }
        // Si la mana est vide, le joueur retourne automatiquement en druide
        if(_currentMana <= 0)
        {
            currentPersonnage = TypePersonnage.Druide;
            _currentMana += 1;
            DruideGo.transform.position = new Vector2(_positionActuelle.x, _positionActuelle.y);

            Vfx.SetActive(true);
            Vfx.transform.position = new Vector2(VfxPositionDruide.transform.position.x, VfxPositionDruide.transform.position.y);
        }

        switch (currentPersonnage)
        {
            case TypePersonnage.Druide:
                //récupérer position actuelle
                _positionActuelle = DruideGo.transform.position;
                //StatDeVie

                _Druide.currentHealth = _currentHealth;

                //script
                _Druide.enabled = true;
                _bear.enabled = false;
                _bird.enabled = false;
                _fish.enabled = false;
                //gameObject
                DruideGo.SetActive(true);
                BearGo.SetActive(false);
                FishGo.SetActive(false);
                BirdGo.SetActive(false);

                break;
            case TypePersonnage.Bears:
                //récupérer position actuelle
                _positionActuelle = BearGo.transform.position;
                //StatDeVie

                _bear.currentHealth = _currentHealth;

                //script
                _Druide.enabled = false;
                _bear.enabled = true;
                _bird.enabled = false;
                _fish.enabled = false;
                //gameObject

                BearGo.SetActive(true);
                DruideGo.SetActive(false);
                FishGo.SetActive(false);
                BirdGo.SetActive(false);


                // Handle Bears specific logic
                break;
            case TypePersonnage.Fish:
                //récupérer position actuelle
                _positionActuelle = FishGo.transform.position;
                //Stat de vie et de mana
                _fish.currentHealth = _currentHealth;
                // script
                _Druide.enabled = false;
                _bear.enabled = false;
                _bird.enabled = false;
                _fish.enabled = true;
                // GameObject
                FishGo.SetActive(true);
                BearGo.SetActive(false);
                BirdGo.SetActive(false);
                DruideGo.SetActive(false);
                // Handle Fish specific logic
                break;
            case TypePersonnage.Bird:
                //récupérer position actuelle
                _positionActuelle = BirdGo.transform.position;
                //Stat de vie et de mana

                _bird.currentHealth = _currentHealth;

                //script
                _bird.enabled = true;
                _Druide.enabled = false;
                _bear.enabled = false;
                _fish.enabled = false;
                //GameObject
                BirdGo.SetActive(true);
                FishGo.SetActive(false);
                BearGo.SetActive(false);
                DruideGo.SetActive(false);
                // Handle Bird specific logic
                break;
        }

        _timer += Time.deltaTime;
        if (_timer >= _timerMax)
        {
            //Debug.Log("une seconde");

            // Consommation de mana en forme ours
            if (currentPersonnage == TypePersonnage.Bears)
            {
                _currentMana  -= 6;
            }

            // Consommation de mana en forme poisson
            else if (currentPersonnage == TypePersonnage.Fish)
            {
                _currentMana -= 8;
            }

            // Consommation de mana en forme oiseau
            else if (currentPersonnage == TypePersonnage.Bird)
            {
                _currentMana -= 8;
            }

            // Régénération de mana en forme druide
            else if(currentPersonnage == TypePersonnage.Druide && _currentMana < MaxMana)
            {
                _currentMana += 10;
                if(_currentMana >= MaxMana)
                {
                    _currentMana = MaxMana;
                }
            }

            _timer = 0;
        }

        afficherVieEtMana();

        // Sauvegarde les statistiques actuelles
        PlayerStatManager.Instance.SaveStats(_currentHealth,_currentMana,MaxHealth,MaxMana);
    }

    // sers à mettre a jour depuis la classe entity
    /// <summary>
    /// Met à jour la vie actuelle depuis la classe Entity.
    /// </summary>
    /// <param name="health">
    /// Nouvelle valeur de vie actuelle.
    /// </param>
    public void MettreAjourVieEtMana(int health)
    {
        _currentHealth = health;
    }

    /// <summary>
    /// Récupère de la mana.
    /// Si la mana dépasse le maximum, le maximum est augmenté.
    /// </summary>
    public void ManaRecuperer()
    {
        MaxMana += 20;
        if (_currentMana > MaxMana)
        {
            int difference = _currentMana - MaxMana;
            MaxMana += difference;
        }
    }

    /// <summary>
    /// Récupère de la vie.
    /// Si la vie dépasse le maximum, le maximum est augmenté.
    /// </summary>
    public void VieRecuperer()
    {
        _currentHealth += 20;
        if (_currentHealth > MaxHealth)
        {
            int difference = _currentHealth - MaxHealth;
            MaxHealth += difference;
        }
    }

    /// <summary>
    /// Transforme le personnage en druide.
    /// </summary>
    /// <param name="inputValue">
    /// Valeur envoyée par le système d’Input.
    /// </param>
    void OnChangeDruide(InputValue inputValue)
    {
        if (inputValue.isPressed && TypePersonnage.Druide != currentPersonnage)
        {

            currentPersonnage = TypePersonnage.Druide;
            DruideGo.transform.position = new Vector2(_positionActuelle.x, _positionActuelle.y);

            Vfx.SetActive(true);
            Vfx.transform.position = new Vector2(VfxPositionDruide.transform.position.x, VfxPositionDruide.transform.position.y);
        }
    }

    /// <summary>
    /// Transforme le personnage en ours si la mana est suffisante.
    /// </summary>
    /// <param name="inputValue">
    /// Valeur envoyée par le système d’Input.
    /// </param>
    void OnChangeBear(InputValue inputValue)
    {
        if (inputValue.isPressed && TypePersonnage.Bears != currentPersonnage && _currentMana >= 6)
        {
            currentPersonnage = TypePersonnage.Bears;
            BearGo.transform.position = new Vector2(_positionActuelle.x, _positionActuelle.y);

            Vfx.SetActive(true);
            Vfx.transform.position = VfxPositionBear.transform.position;
        }
    }

    /// <summary>
    /// Transforme le personnage en poisson si la mana est suffisante.
    /// </summary>
    /// <param name="inputValue">
    /// Valeur envoyée par le système d’Input.
    /// </param>
    void OnChangeFish(InputValue inputValue)
    {
        if (inputValue.isPressed && TypePersonnage.Fish != currentPersonnage && _currentMana >= 5)
        {
            currentPersonnage = TypePersonnage.Fish;
            FishGo.transform.position = new Vector2(_positionActuelle.x, _positionActuelle.y);

            Vfx.SetActive(true);
            Vfx.transform.position = VfxPositionFish.transform.position;
        }
    }

    /// <summary>
    /// Transforme le personnage en oiseau si la mana est suffisante.
    /// </summary>
    /// <param name="inputValue">
    /// Valeur envoyée par le système d’Input.
    /// </param>
    void OnChangeBird(InputValue inputValue)
    {
        if (inputValue.isPressed && TypePersonnage.Bird != currentPersonnage && _currentMana >= 8)
        {
            currentPersonnage = TypePersonnage.Bird;
            BirdGo.transform.position = new Vector2(_positionActuelle.x, _positionActuelle.y);

            Vfx.SetActive(true);
            Vfx.transform.position = VfxPositionBird.transform.position;
        }
    }

    /// <summary>
    /// Désactive le VFX lorsque son animation est terminée.
    /// </summary>
    public void VfxTerminer()
    {
        Vfx.SetActive(false);
    }

    //il sert à afficher la vie pout l'ui
    /// <summary>
    /// Affiche la vie et la mana dans l’interface utilisateur.
    /// </summary>
    void afficherVieEtMana()
    {
        vie.GetComponent<TextMeshProUGUI>().text = _currentHealth + " / " + MaxHealth;
        mana.GetComponent<TextMeshProUGUI>().text = _currentMana + " / " + MaxMana;
    }

    /// <summary>
    /// Retourne la vie actuelle du personnage.
    /// </summary>
    /// <returns>
    /// Vie actuelle.
    /// </returns>
    public int RetournerVie()
    {
        return _currentHealth;
    }

    /// <summary>
    /// Retourne la mana actuelle du personnage.
    /// </summary>
    /// <returns>
    /// Mana actuelle.
    /// </returns>
    public int RetournerMana()
    {
        return _currentMana;
    }

    public bool IsDead()
    {
        return _isDead;
    }

    
}