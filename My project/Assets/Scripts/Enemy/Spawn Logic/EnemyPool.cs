using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int initialSize = 50;

    private readonly Queue<GameObject> pool = new();

    private void Awake()
    {
        for (int i = 0; i < initialSize; i++)
        {
            CreateEnemy();
        }
    }

    private void CreateEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab);

        enemy.SetActive(false);

        EnemyHealth health =
            enemy.GetComponent<EnemyHealth>();

        if (health != null)
        {
            health.Pool = this;
        }

        pool.Enqueue(enemy);
    }

    public GameObject GetEnemy()
    {
        if (pool.Count == 0)
        {
            CreateEnemy();
        }

        GameObject enemy = pool.Dequeue();

        EnemyHealth health =
            enemy.GetComponent<EnemyHealth>();

        if (health != null)
        {
            health.Pool = this;
        }

        enemy.SetActive(true);

        return enemy;
    }

    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);

        pool.Enqueue(enemy);
    }
}
