using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAccueil : MonoBehaviour
{

   public GameObject vie;
   public GameObject mana;

   void Start()
    {
        TextMeshProUGUI txt = vie.GetComponent<TextMeshProUGUI>();
        txt.text = "Vie max = " + PlayerStatManager.Instance.maxHealth;
        txt = mana.GetComponent<TextMeshProUGUI>();
        txt.text = "Mana max = " + PlayerStatManager.Instance.maxMana;

    }
    public void Jouer()
    {
        SceneManager.LoadScene("MenuChoixNiveau");
    }

    public void Quitter()
    {
        Application.Quit();
    }
    // Update is called once per frame
   
}
