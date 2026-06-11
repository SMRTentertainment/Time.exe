using UnityEngine;

public class TrapAnimator : TrapBehaviour
{
    [SerializeField] private Animator animator;

    public override void SetActive(bool value)
    {
        base.SetActive(value);

        if (animator == null)
            return;

        animator.SetBool("Active", value);
    }
}
