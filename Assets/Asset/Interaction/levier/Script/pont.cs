using UnityEngine;
using UnityEngine.UIElements;

public class Pont : MonoBehaviour
{
    Levier levier;
    BoxCollider2D boxCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levier = GetComponentInChildren<Levier>();
    }

    // Update is called once per frame
    void Update()
    {
        if (levier.isActivated)
        {
            boxCollider = GetComponent<BoxCollider2D>();
            boxCollider.enabled = false;
        }else
        {
            boxCollider = GetComponent<BoxCollider2D>();
            boxCollider.enabled = true;
        }

    }
}
