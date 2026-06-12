using UnityEngine;

public class EnemyAppearance : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    public void ApplyVisual(EnemyVisualData visualData)
    {
        spriteRenderer.sprite = visualData.defaultSprite;

        animator.runtimeAnimatorController =
                visualData.animatorController;

        animator.Rebind();
        animator.Update(0f);
    }
}

