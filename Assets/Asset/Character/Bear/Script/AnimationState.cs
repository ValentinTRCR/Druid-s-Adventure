using UnityEngine;

public class AnimationState : MonoBehaviour
{
    private Bear _bear;

    void Start()
    {
        _bear = GetComponentInParent<Bear>();
    }
    void Attack()
    {
        _bear.EnleverDegat();
    }

    void Reset()
    {
        _bear.ResetAttack();
    }
}
