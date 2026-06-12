using TMPro;
using UnityEngine;

public class SurvivalTimer : MonoBehaviour
{
    [SerializeField] private float startTime = 300f;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Victory Music")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip victoryMusic;

    private float currentTime;
    private bool timerFinished;

    public float CurrentTime => currentTime;
    public float StartTime => startTime;

    private void Start()
    {
        currentTime = startTime;
        UpdateTimerDisplay();
    }

    private void Update()
    {
        if (timerFinished)
            return;

        if (GameStateManager.Instance.CurrentState != GameState.Playing)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            timerFinished = true;

            Victory();
        }

        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = $"{minutes}:{seconds:00}";
    }

    private void Victory()
    {
        if (musicSource != null &&
            victoryMusic != null)
        {
            musicSource.Stop();
            musicSource.clip = victoryMusic;
            musicSource.Play();
        }

        GameStateManager.Instance.TriggerVictory();
    }
}
