using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TMP_Text player1ScoreText;
    public TMP_Text player2ScoreText;
    public GameObject gameOverPanel;
    public TMP_Text gameOverText;

    public int player1Score;
    public int player2Score;

    public bool isGameOver = false;

    public AudioSource backgroundMusic;

    public bool isPaused = false;
    public GameObject pausePanel;
    public GameObject timeLine;

    private bool escPressed = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!escPressed)
            {
                if (!isGameOver)
                {
                    isPaused = !isPaused;
                    Time.timeScale = isPaused ? 0 : 1;
                    pausePanel.SetActive(isPaused);
                    timeLine.GetComponent<SpriteRenderer>().color = isPaused ? new Color32(0, 0, 0, 255) : new Color32(255, 255, 255, 255);
                    escPressed = true;
                }
            }
            else
            {
                escPressed = false;
            }
        }
    }

    public void PauseOn()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        timeLine.GetComponent<SpriteRenderer>().color = isPaused ? new Color32(0, 0, 0, 255) : new Color32(255, 255, 255, 255);
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void AddPoints(bool isPlayer1, int pointsToAdd)
    {
        if (isGameOver) return;

        if (isPlayer1)
        {
            player1Score += pointsToAdd;
            player1ScoreText.text = player1Score.ToString();
        }
        else
        {
            player2Score += pointsToAdd;
            player2ScoreText.text = player2Score.ToString();
        }
    }

    public void DeductPoints(bool isPlayer1, int pointsToDeduct)
    {
        if (isGameOver) return;

        if (isPlayer1)
        {
            player1Score -= pointsToDeduct;
            player1ScoreText.text = player1Score.ToString();
        }
        else
        {
            player2Score -= pointsToDeduct;
            player2ScoreText.text = player2Score.ToString();
        }
    }

    public void EndGame(bool isPlayer1Winner)
    {
        isGameOver = true;

        backgroundMusic.Stop();

        if (isPlayer1Winner)
        {
            gameOverText.text = "Player 1 wins!";
        }
        else
        {
            gameOverText.text = "Player 2 wins!";
        }

        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
