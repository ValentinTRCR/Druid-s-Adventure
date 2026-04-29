using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinirLeNiveau : MonoBehaviour
{
    Transform crystal;
    public bool canBeTake = false;

    public string NomSceneProchaineNiveau;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision détectée avec : " + collision.gameObject);
        if (collision.gameObject.CompareTag("Player"))
        {
            
            foreach (Transform child in collision.gameObject.GetComponentsInChildren<Transform>())
            {
                
                if (child.name == "crystal")
                {
                    crystal = child;
                    break;
                }
            }

            if (crystal != null)
            {
                
                canBeTake = true;
            }else
            {
                Debug.Log("Crystal non trouvé !");
            }

            if (canBeTake)
            {
                SceneManager.LoadScene(NomSceneProchaineNiveau);
            }
        }
    }
}
