using System.Threading;
using NUnit.Framework;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private GestionPersonnage gp;
    public int currentHealth;


    public float movex;
    public bool Hurt;

    public bool HaveDeadAnimation = false;
    public bool IsDead;

    public bool IsInWater = false;

    //
    float timerInWater = 0f;
    float timerInWaterMax = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        IsDead = false;
        gp = GetComponent<GestionPersonnage>();
        currentHealth = gp.MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Hurt = true;
        if(gp != null)
        {
            gp.MettreAjourVieEtMana(currentHealth);
        }
        
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

    public void DamageInTheWater()
    {
        timerInWater += Time.deltaTime;
        if(timerInWater >= timerInWaterMax)
        {
            TakeDamage(5);
            timerInWater = 0;
        }
    }
}
