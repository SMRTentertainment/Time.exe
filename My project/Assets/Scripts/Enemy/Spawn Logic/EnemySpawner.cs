using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyPool pool;
    [SerializeField] private PlayerTimeStop playerTimeStop;
    [SerializeField] private SurvivalTimer survivalTimer;

    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Boss")]
    [SerializeField] private GameObject bossPrefab;

    [Header("Music")]
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private AudioClip normalMusic;

    [SerializeField] private AudioClip bossMusic;

    [Header("Spawn Rate")]
    [SerializeField] private float baseSpawnInterval = 3f;
    [SerializeField] private float minimumSpawnInterval = 0.25f;

    [Header("Difficulty")]
    [SerializeField] private float intervalReductionPerMinute = 0.2f;

    [Header("Time Stop")]
    [SerializeField] private float bossThreshold = 60f;
    [SerializeField] private float timeStopPenaltyMultiplier = 0.01f;

    [Header("Enemy Visuals")]
    [SerializeField] private EnemyVisualData[] enemyVisuals;

    private float spawnTimer;
    private float currentSpawnInterval;

    private bool bossSpawned;
    private bool bossEligible;

    private void Start()
    {
        currentSpawnInterval = baseSpawnInterval;

        if (musicSource != null &&
            normalMusic != null)
        {
            musicSource.clip = normalMusic;
            musicSource.Play();
        }
    }

    private void Update()
    {
        if (GameStateManager.Instance.CurrentState != GameState.Playing)
            return;

        UpdateSpawnRate();

        CheckBossSpawn();

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= currentSpawnInterval)
        {
            spawnTimer = 0f;

            SpawnEnemy();
        }
    }

    private void UpdateSpawnRate()
    {
        float elapsedTime =
            Time.timeSinceLevelLoad;

        float minuteBonus =
            Mathf.Floor(elapsedTime / 60f)
            * intervalReductionPerMinute;

        float timeStopBonus =
            playerTimeStop.TotalTimeUsed
            * timeStopPenaltyMultiplier;

        currentSpawnInterval =
            baseSpawnInterval
            - minuteBonus
            - timeStopBonus;

        currentSpawnInterval =
            Mathf.Max(
                currentSpawnInterval,
                minimumSpawnInterval);
    }

    private void CheckBossSpawn()
    {
        if (bossSpawned)
            return;

        if (playerTimeStop.TotalTimeUsed >= bossThreshold)
        {
            bossEligible = true;
        }

        if (!bossEligible)
            return;

        if (survivalTimer.CurrentTime <= 30f)
        {
            SpawnBoss();
        }
    }

    private void SpawnEnemy()
    {
        if (spawnPoints.Length == 0)
            return;

        int spawnIndex =
            Random.Range(0, spawnPoints.Length);

        GameObject enemy =
            pool.GetEnemy();

        enemy.transform.position =
            spawnPoints[spawnIndex].position;

        if (enemyVisuals.Length > 0)
        {
            int visualIndex =
                Random.Range(
                    0,
                    enemyVisuals.Length);

            EnemyAppearance appearance =
                enemy.GetComponent<EnemyAppearance>();

            if (appearance != null)
            {
                appearance.ApplyVisual(
                    enemyVisuals[visualIndex]);
            }
        }
    }

    private void SpawnBoss()
    {
        bossSpawned = true;

        int index =
            Random.Range(0, spawnPoints.Length);

        Instantiate(
            bossPrefab,
            spawnPoints[index].position,
            Quaternion.identity);

        if (musicSource != null &&
            bossMusic != null)
        {
            musicSource.Stop();
            musicSource.clip = bossMusic;
            musicSource.Play();
        }

        Debug.Log("Boss Spawned");
    }
}
