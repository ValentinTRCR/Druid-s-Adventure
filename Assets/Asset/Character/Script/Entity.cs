using UnityEngine;

public class Entity : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public bool Hurt;

    public bool IsDead;

    public bool IsInWater = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        currentHealth = maxHealth;
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Hurt = true;

        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        IsDead = true;
        Invoke("LancerAnimation", 0.5f);
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
