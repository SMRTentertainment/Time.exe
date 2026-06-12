using System.Collections;
using UnityEngine;

public class TrapBase : MonoBehaviour, IInteractable
{
    [Header("Timing")]
    [SerializeField] private float activeDuration = 3f;
    [SerializeField] private float cooldown = 5f;

    [Header("Visual")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Color readyColor = Color.white;
    [SerializeField] private Color activeColor = Color.green;
    [SerializeField] private Color cooldownColor = Color.gray;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip activationSound;

    private bool isActive;
    private bool isCoolingDown;

    private TrapBehaviour[] behaviours;

    private void Awake()
    {
        behaviours =
            GetComponentsInChildren<TrapBehaviour>(true);

        if (spriteRenderer != null)
        {
            spriteRenderer.color = readyColor;
        }
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

        if (spriteRenderer != null)
        {
            spriteRenderer.color = activeColor;
        }

        if (audioSource != null &&
            activationSound != null)
        {
            audioSource.PlayOneShot(activationSound);
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

        isActive = false;
        isCoolingDown = true;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = cooldownColor;
        }

        yield return new WaitForSeconds(cooldown);

        isCoolingDown = false;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = readyColor;
        }
    }
}