using UnityEngine;
using UnityEngine.UI;

public class TimeStopEyeUI : MonoBehaviour
{
    [SerializeField] private PlayerTimeStop playerTimeStop;

    [Header("Boss Spawn")]
    [SerializeField] private float bossThreshold = 60f;

    [Header("UI")]
    [SerializeField] private Image eyeImage;

    private Color currentColor;

    private void Awake()
    {
        currentColor = eyeImage.color;
        currentColor.a = 0f;
        eyeImage.color = currentColor;
    }

    private void Update()
    {
        if (!playerTimeStop.IsTimeStopped)
        {
            currentColor.a = Mathf.MoveTowards(
                currentColor.a,
                0f,
                Time.deltaTime * 5f);

            eyeImage.color = currentColor;
            return;
        }

        float visibility =
            Mathf.Clamp01(
                playerTimeStop.TotalTimeUsed /
                bossThreshold);

        currentColor.a = visibility;

        eyeImage.color = currentColor;
    }
}