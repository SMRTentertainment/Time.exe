using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Transform player;

    private float originalSpeed;

    private bool isFrozen;

    private PlayerTimeStop playerTimeStop;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        originalSpeed = moveSpeed;
    }

    private void Start()
    {
        PlayerMovement playerMovement =
            FindFirstObjectByType<PlayerMovement>();

        if (playerMovement != null)
        {
            player = playerMovement.transform;
        }

        playerTimeStop =
            FindFirstObjectByType<PlayerTimeStop>();
    }

    private void Update()
    {
        if (playerTimeStop == null)
            return;

        bool shouldBeFrozen =
            playerTimeStop.IsTimeStopped;

        if (shouldBeFrozen && !isFrozen)
        {
            Freeze();
        }
        else if (!shouldBeFrozen && isFrozen)
        {
            Unfreeze();
        }
    }

    private void FixedUpdate()
    {
        if (player == null)
            return;

        if (moveSpeed <= 0f)
            return;

        Vector2 direction =
            ((Vector2)player.position - rb.position)
            .normalized;

        rb.MovePosition(
            rb.position +
            direction * moveSpeed * Time.fixedDeltaTime);
    }

    private void Freeze()
    {
        isFrozen = true;

        moveSpeed = 0f;

        SetCollidersEnabled(false);
    }

    private void Unfreeze()
    {
        isFrozen = false;

        moveSpeed = originalSpeed;

        SetCollidersEnabled(true);
    }

    public void SetMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void RestoreOriginalSpeed()
    {
        moveSpeed = originalSpeed;
    }

    public void SetCollidersEnabled(bool enabled)
    {
        Collider2D[] colliders =
            GetComponentsInChildren<Collider2D>();

        foreach (Collider2D collider in colliders)
        {
            collider.enabled = enabled;
        }
    }
}