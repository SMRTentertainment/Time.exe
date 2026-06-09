using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;

    [Header("Invulnerability")]
    [SerializeField] private float invulnerabilityTime = 1f;
    [SerializeField] private float damagedAlpha = 0.5f;

    private float currentHealth;
    private bool isInvulnerable;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;
    public bool IsInvulnerable => isInvulnerable;

    private void Awake()
    {
        currentHealth = maxHealth;

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0)
            return;

        if (isInvulnerable)
            return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(InvulnerabilityRoutine());
    }

    public void Heal(float amount)
    {
        if (amount <= 0)
            return;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }

    private IEnumerator InvulnerabilityRoutine()
    {
        isInvulnerable = true;

        if (spriteRenderer != null)
        {
            Color color = originalColor;
            color.a = damagedAlpha;
            spriteRenderer.color = color;
        }

        yield return new WaitForSeconds(invulnerabilityTime);

        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }

        isInvulnerable = false;
    }

    private void Die()
    {
        GameStateManager.Instance.TriggerGameOver();
    }
}
