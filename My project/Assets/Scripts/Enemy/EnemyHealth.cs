using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 10f;

    [Header("Drops")]
    [SerializeField, Range(0f, 1f)]
    private float dropChance = 0.1f;
    
    [Header("Audio")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private float deathVolume = 1f;

    [SerializeField]
    private GameObject[] possibleDrops;

    private float currentHealth;

    public EnemyPool Pool { get; set; }

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0)
            return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        if (amount <= 0)
            return;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }

    private void Die()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayEnemyDeath();
        }

        TryDropItem();

        if (Pool != null)
        {
            Pool.ReturnEnemy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void TryDropItem()
    {
        if (possibleDrops == null ||
            possibleDrops.Length == 0)
        {
            return;
        }

        if (Random.value > dropChance)
        {
            return;
        }

        int randomIndex =
            Random.Range(
                0,
                possibleDrops.Length);

        Instantiate(
            possibleDrops[randomIndex],
            transform.position,
            Quaternion.identity);
    }
}
