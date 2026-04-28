using UnityEngine;

public class Levier : MonoBehaviour
{
    public bool isActivated = false;
    Animator anim;
    public GameObject pont;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        
        anim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActivated)
        {
            anim.enabled = true;
            pont.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
