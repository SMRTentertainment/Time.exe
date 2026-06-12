using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxSource;

    [Header("Enemy Sounds")]
    [SerializeField] private AudioClip enemyDeathSound;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void PlayEnemyDeath()
    {
        if (enemyDeathSound == null)
            return;

        sfxSource.PlayOneShot(enemyDeathSound);
    }
}