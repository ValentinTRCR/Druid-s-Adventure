using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuChoixNiveau : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void NiveauUn()
    {
        SceneManager.LoadScene("NiveauUn");
    }

    public void NiveauDeux()
    {
        SceneManager.LoadScene("NiveauDeux");
    }

    public void NiveauTrois()
    {
        SceneManager.LoadScene("NiveauTrois");
    }

    public void Retour()
    {
        SceneManager.LoadScene("MenuAccueil");
    }
}
