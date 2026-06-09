using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float damage = 10f;

    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerHealth player =
            collision.gameObject.GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
