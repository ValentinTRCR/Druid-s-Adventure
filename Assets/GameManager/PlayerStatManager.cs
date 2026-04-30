using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    public static PlayerStatManager Instance;

    public int maxHealth = 100;
    public int health = 100;

    public int maxMana = 200;
    public int mana = 200;
    public bool HasCrystal;


    private void Awake() {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveStats(int currentHealth, int currentMana,int maxHealth,int manaMax)
    {
        health = currentHealth;
        mana = currentMana;
        this.maxHealth = maxHealth;
        maxMana = manaMax;
    }
}
