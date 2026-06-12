using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private InputActionReference moveAction;

    private Rigidbody2D rb;
    private Vector2 movement;

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
    }

    private void Update()
    {
        movement = moveAction.action.ReadValue<Vector2>().normalized;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    public void ApplySpeedBuff(
        float bonusSpeed,
        float duration)
    {
        StartCoroutine(
            SpeedBuffRoutine(
                bonusSpeed,
                duration));
    }
    private IEnumerator SpeedBuffRoutine(
        float bonusSpeed,
        float duration)
    {
        moveSpeed += bonusSpeed;

        yield return new WaitForSeconds(duration);

        moveSpeed -= bonusSpeed;
    }
}
