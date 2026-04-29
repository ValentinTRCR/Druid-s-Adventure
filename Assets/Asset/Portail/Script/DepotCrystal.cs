using UnityEngine;

public class DepotCrystal : MonoBehaviour
{
    public bool isActivated = false;
    Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        anim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Transform crystal = transform.Find("crystal");
        if(crystal != null)
        {
            crystal.position = transform.Find("positionCrystal").position;
            anim.enabled = true;
            crystal.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
            isActivated = true;

        }
    }
}
