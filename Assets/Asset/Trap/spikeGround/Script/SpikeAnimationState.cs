using UnityEngine;
using System.Collections;

public class SpikeAnimationState : MonoBehaviour
{
    public bool attack = false;

    Animator animator;

    spikeGroundScript spikeGroundScript;

    BoxCollider2D boxCollider2D;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(SpikeLoop());
        spikeGroundScript = GetComponentInParent<spikeGroundScript>();
        boxCollider2D = GetComponentInParent<BoxCollider2D>();
    }

    IEnumerator SpikeLoop()
    {
        while (true)
        {
            // lance l'animation
            animator.SetTrigger("Attack");

            // attend 2 secondes avant la prochaine attaque
            yield return new WaitForSeconds(2f);
        }
    }

    public void AttaqueTrue()
    {
        boxCollider2D.enabled = true;

    }

    public void AttaqueFalse()
    {
        animator.enabled = false; // stop Animator temporairement
        boxCollider2D.enabled = false;
        attack = false;
        animator.enabled = true; // remet Animator
    }

    public void ResetHurt()
    {
        spikeGroundScript.ResetHurt();
    }
}
