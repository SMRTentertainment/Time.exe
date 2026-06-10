using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTimeStop : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference timeStopAction;

    [Header("Time Stop")]
    [SerializeField] private float maxCharge = 5f;

    [SerializeField] private float drainRate = 1f;

    [SerializeField] private float rechargeRate = 0.5f;

    private float currentCharge;

    private bool isTimeStopped;

    private float totalTimeUsed;

    public bool IsTimeStopped => isTimeStopped;

    public float CurrentCharge => currentCharge;

    public float MaxCharge => maxCharge;

    public float TotalTimeUsed => totalTimeUsed;

    private void Awake()
    {
        currentCharge = maxCharge;
    }

    private void OnEnable()
    {
        timeStopAction.action.Enable();

        timeStopAction.action.started += OnTimeStopStarted;
        timeStopAction.action.canceled += OnTimeStopCanceled;
    }

    private void OnDisable()
    {
        timeStopAction.action.started -= OnTimeStopStarted;
        timeStopAction.action.canceled -= OnTimeStopCanceled;

        timeStopAction.action.Disable();
    }

    private void Update()
    {
        if (isTimeStopped)
        {
            float delta = Time.deltaTime;

            currentCharge -= drainRate * delta;

            totalTimeUsed += delta;

            if (currentCharge <= 0f)
            {
                currentCharge = 0f;

                StopTime();
            }
        }
        else
        {
            currentCharge += rechargeRate * Time.deltaTime;

            currentCharge =
                Mathf.Clamp(currentCharge, 0f, maxCharge);
        }
    }

    private void OnTimeStopStarted(InputAction.CallbackContext context)
    {
        if (currentCharge <= 0f)
            return;

        StartTime();
    }

    private void OnTimeStopCanceled(InputAction.CallbackContext context)
    {
        StopTime();
    }

    private void StartTime()
    {
        if (isTimeStopped)
            return;

        isTimeStopped = true;

        EnemyMovement[] enemies =
            FindObjectsByType<EnemyMovement>(
                FindObjectsSortMode.None);

        foreach (EnemyMovement enemy in enemies)
        {
            enemy.SetMoveSpeed(0f);
            enemy.SetCollidersEnabled(false);
        }
    }

    private void StopTime()
    {
        if (!isTimeStopped)
            return;

        isTimeStopped = false;

        EnemyMovement[] enemies =
            FindObjectsByType<EnemyMovement>(
                FindObjectsSortMode.None);

        foreach (EnemyMovement enemy in enemies)
        {
            enemy.RestoreOriginalSpeed();
            enemy.SetCollidersEnabled(true);
        }
    }
}
