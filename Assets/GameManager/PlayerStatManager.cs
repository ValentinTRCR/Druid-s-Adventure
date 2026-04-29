using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    public static PlayerStatManager Instance;

    public int maxHealth = 100;
    public int health = 100;

    public int maxMana = 100;
    public int mana = 100;

    private void Awake() {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveStats(int currentHealth, int currentMana)
    {
        health = currentHealth;
        mana = currentMana;
    }
}
