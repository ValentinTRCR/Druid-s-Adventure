using System.Threading;
using NUnit.Framework;
using UnityEngine;

public class Entity : MonoBehaviour
{
    /// <summary>
    /// Référence au script GestionPersonnage.
    /// Permet notamment de récupérer la vie maximale
    /// et de mettre à jour l’interface.
    /// </summary>
    private GestionPersonnage _gp;

    /// <summary>
    /// Vie actuelle de l’entité.
    /// </summary>
    public int currentHealth;

    /// <summary>
    /// Valeur du déplacement horizontal.
    /// </summary>
    public float movex;

    /// <summary>
    /// Vérifie si l’entité vient de recevoir des dégâts.
    /// </summary>
    public bool Hurt;

    /// <summary>
    /// Indique si l’entité possède une animation de mort.
    /// </summary>
    public bool HaveDeadAnimation = false;

    /// <summary>
    /// Vérifie si l’entité est morte.
    /// </summary>
    public bool IsDead;

    /// <summary>
    /// Vérifie si l’entité est actuellement dans l’eau.
    /// </summary>
    public bool IsInWater = false;

    /// <summary>
    /// Timer utilisé pour appliquer des dégâts dans l’eau.
    /// </summary>
    float _timerInWater = 0f;

    /// <summary>
    /// Temps maximum avant d’infliger des dégâts dans l’eau.
    /// </summary>
    float _timerInWaterMax = 0.5f;

    /// <summary>
    /// Initialisation de l’entité.
    /// </summary>
    public void Start()
    {
        IsDead = false;

        _gp = GetComponent<GestionPersonnage>();

        currentHealth = _gp.MaxHealth;
    }

    /// <summary>
    /// Inflige des dégâts à l’entité.
    /// </summary>
    /// <param name="damage">
    /// Nombre de points de vie à retirer.
    /// </param>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Hurt = true;

        // Met à jour l’interface de vie et de mana
        if(_gp != null)
        {
            _gp.MettreAjourVieEtMana(currentHealth);
        }

        // Vérifie si l’entité n’a plus de vie
        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    /// <summary>
    /// Gère la mort de l’entité.
    /// </summary>
    private void Dead()
    {
        // Si aucune animation de mort n’est prévue,
        // l’objet est détruit après un court délai
        if (!HaveDeadAnimation)
        {
            Invoke("LancerAnimation", 0.5f);
        }

        IsDead = true;
    }

    /// <summary>
    /// Détruit le GameObject de l’entité.
    /// </summary>
    private void LancerAnimation()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Mise à jour appelée à chaque frame.
    /// </summary>
    void Update()
    {

    }

    /// <summary>
    /// Inflige régulièrement des dégâts
    /// lorsque l’entité reste dans l’eau.
    /// </summary>
    public void DamageInTheWater()
    {
        _timerInWater += Time.deltaTime;

        // Vérifie si le temps avant dégâts est atteint
        if(_timerInWater >= _timerInWaterMax)
        {
            TakeDamage(5);

            _timerInWater = 0;
        }
    }
}