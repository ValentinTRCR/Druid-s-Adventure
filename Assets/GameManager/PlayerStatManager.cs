
using System.IO;

using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    public static PlayerStatManager Instance;

    public int maxHealth;
    public int health;

    public int maxMana;
    public int mana;
    public bool HasCrystal;
    string filePath;


    private void Awake() {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        filePath = Application.persistentDataPath + "/StatData.json";
        Debug.Log(filePath);
        if (System.IO.File.Exists(filePath))
        {
            string donnee = File.ReadAllText(filePath);
            StatData stats = JsonUtility.FromJson<StatData>(donnee);
            health = stats.health;
            maxHealth = stats.healthMax;
            maxMana = stats.manaMax;
            mana = stats.mana;
        }
        else
        {
            health = 100;
            maxHealth = 100;
            maxMana = 60;
            mana = 60;
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

    public void SauvegarderDonnee(int healthMax,int health,int manaMax,int mana)
    {
        StatData stats = new StatData();
        stats.healthMax = healthMax;
        stats.health = health;
        stats.manaMax = manaMax;
        stats.mana = mana;


        string donneeVieEtMana = JsonUtility.ToJson(stats);
        Debug.Log(filePath);
        Debug.Log(donneeVieEtMana);
        System.IO.File.WriteAllText(filePath,donneeVieEtMana);
        Debug.Log("DonnéeSauvegardé");
    }

    [System.Serializable]
    public class StatData
    {
         public int healthMax;
        public int health;
        public int manaMax;
        public int mana;
    }
    
}

