using UnityEngine;

public class AnimationState : MonoBehaviour
{
    private Bear bear;

    void Start()
    {
        bear = GetComponentInParent<Bear>();
    }
    void Attack()
    {
        bear.EnleverDegat();
    }

    void Reset()
    {
        bear.ResetAttack();
    }
}
