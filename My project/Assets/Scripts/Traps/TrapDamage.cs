using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : TrapBehaviour
{
    [SerializeField] private float damagePerSecond = 20f;

    [SerializeField]
    private Collider2D effectCollider;

    private readonly List<EnemyHealth> enemies = new();

    private void Awake()
    {
        if (effectCollider != null)
        {
            effectCollider.enabled = false;
        }
    }

    public override void SetActive(bool value)
    {
        base.SetActive(value);

        if (effectCollider != null)
        {
            effectCollider.enabled = value;
        }

        if (!value)
        {
            enemies.Clear();
        }
    }

    private void Update()
    {
        if (!active)
            return;

        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            EnemyHealth enemy = enemies[i];

            if (enemy == null)
            {
                enemies.RemoveAt(i);
                continue;
            }

            enemy.TakeDamage(
                damagePerSecond * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!active)
            return;

        EnemyHealth enemy =
            other.GetComponent<EnemyHealth>();

        if (enemy != null &&
            !enemies.Contains(enemy))
        {
            enemies.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        EnemyHealth enemy =
            other.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            enemies.Remove(enemy);
        }
    }
}
