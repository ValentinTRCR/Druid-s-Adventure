using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EcranDeFin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /// <summary>
    /// fonction pour changer de scène
    /// </summary>
    public void continuer()
    {
        SceneManager.LoadScene("MenuChoixNiveau");
    }
}
