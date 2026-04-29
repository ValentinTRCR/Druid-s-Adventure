using NUnit.Framework;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private GestionPersonnage gp;
    public int currentHealth;

    public int currentMana;

    public float movex;
    public bool Hurt;

    public bool HaveDeadAnimation = false;
    public bool IsDead;

    public bool IsInWater = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        currentHealth = gp.MaxHealth;
        currentMana = gp.MaxMana;
        IsDead = false;
        gp = GetComponent<GestionPersonnage>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Hurt = true;
        gp.MettreAjourVieEtMana(currentHealth,currentMana);
        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
       
        if (!HaveDeadAnimation)
        {
           Invoke("LancerAnimation", 0.5f);
        }
        IsDead = true;
        
    }

    private void LancerAnimation()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
