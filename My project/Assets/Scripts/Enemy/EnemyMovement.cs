using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Transform player;

    public float MoveSpeed => moveSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        PlayerMovement playerMovement = FindFirstObjectByType<PlayerMovement>();

        if (playerMovement != null)
        {
            player = playerMovement.transform;
        }
    }

    private void FixedUpdate()
    {
        if (player == null)
            return;

        Vector2 direction =
            ((Vector2)player.position - rb.position).normalized;

        rb.MovePosition(
            rb.position +
            direction * moveSpeed * Time.fixedDeltaTime
        );
    }

    public void SetMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }
    public void SetCollidersEnabled(bool enabled)
    {
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();

        foreach (Collider2D collider in colliders)
        {
            collider.enabled = enabled;
        }
    }
}