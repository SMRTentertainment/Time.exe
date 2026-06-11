using System.Collections;
using UnityEngine;

public class TrapBase : MonoBehaviour, IInteractable
{
    [Header("Timing")]
    [SerializeField] private float activeDuration = 3f;
    [SerializeField] private float cooldown = 5f;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private bool isActive;
    private bool isCoolingDown;

    private TrapBehaviour[] behaviours;

    private void Awake()
    {
        behaviours =
            GetComponentsInChildren<TrapBehaviour>(true);
    }

    public void Interact()
    {
        if (isActive || isCoolingDown)
            return;

        StartCoroutine(ActivationRoutine());
    }

    private IEnumerator ActivationRoutine()
    {
        isActive = true;

        if (animator != null)
        {
            animator.SetBool("Active", true);
        }

        foreach (var behaviour in behaviours)
        {
            behaviour.SetActive(true);
        }

        yield return new WaitForSeconds(activeDuration);

        foreach (var behaviour in behaviours)
        {
            behaviour.SetActive(false);
        }

        if (animator != null)
        {
            animator.SetBool("Active", false);
        }

        isActive = false;
        isCoolingDown = true;

        yield return new WaitForSeconds(cooldown);

        isCoolingDown = false;
    }
}