using System.Collections.Generic;
using UnityEngine;

public class TrapSuction : TrapBehaviour
{
    [SerializeField] private float suctionForce = 5f;

    [SerializeField]
    private Collider2D effectCollider;

    private readonly List<EnemyMovement> enemies = new();

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

    private void FixedUpdate()
    {
        if (!active)
            return;

        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            EnemyMovement enemy = enemies[i];

            if (enemy == null)
            {
                enemies.RemoveAt(i);
                continue;
            }

            Vector2 direction =
                ((Vector2)transform.position -
                 (Vector2)enemy.transform.position)
                .normalized;

            enemy.transform.position +=
                (Vector3)(direction *
                          suctionForce *
                          Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!active)
            return;

        EnemyMovement enemy =
            other.GetComponent<EnemyMovement>();

        if (enemy != null &&
            !enemies.Contains(enemy))
        {
            enemies.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        EnemyMovement enemy =
            other.GetComponent<EnemyMovement>();

        if (enemy != null)
        {
            enemies.Remove(enemy);
        }
    }
}
