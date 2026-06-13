using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    [Header("UI")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject gameplayPanel;

    public GameState CurrentState { get; private set; }
        = GameState.Playing;

    private void Awake()
    {
        if (Instance != null &&
            Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        victoryPanel.SetActive(false);
        gameplayPanel.SetActive(true);

        Time.timeScale = 1f;
        CurrentState = GameState.Playing;
    }

    public void TogglePause()
    {
        if (CurrentState == GameState.GameOver ||
            CurrentState == GameState.Victory)
        {
            return;
        }

        if (CurrentState == GameState.Paused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        CurrentState = GameState.Paused;

        gameplayPanel.SetActive(false);

        pausePanel.SetActive(true);
        gameOverPanel.SetActive(false);
        victoryPanel.SetActive(false);

        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        CurrentState = GameState.Playing;

        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        victoryPanel.SetActive(false);

        gameplayPanel.SetActive(true);

        Time.timeScale = 1f;
    }

    public void TriggerGameOver()
    {
        if (CurrentState == GameState.GameOver)
            return;

        CurrentState = GameState.GameOver;

        gameplayPanel.SetActive(false);

        pausePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        victoryPanel.SetActive(false);

        Time.timeScale = 0f;
    }

    public void TriggerVictory()
    {
        if (CurrentState == GameState.Victory)
            return;

        CurrentState = GameState.Victory;

        gameplayPanel.SetActive(false);

        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        victoryPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void Retry()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }
}