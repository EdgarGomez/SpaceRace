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

    public int player1Ship = 0;
    public int player2Ship = 0;
    public int difficultyLevel = 0;
    public int playTime = 0;
    public float playTimeValue = 15.0f;


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
        SetGameConfig();
    }

    void SetGameConfig()
    {
        player1Ship = PlayerPrefs.HasKey("Player1Ship") ? PlayerPrefs.GetInt("Player1Ship") : 0;
        player2Ship = PlayerPrefs.HasKey("Player2Ship") ? PlayerPrefs.GetInt("Player2Ship") : 0;
        difficultyLevel = PlayerPrefs.HasKey("DifficultyLevel") ? PlayerPrefs.GetInt("DifficultyLevel") : 0;
        playTime = PlayerPrefs.HasKey("PlayTime") ? PlayerPrefs.GetInt("PlayTime") : 0;

        switch (playTime)
        {
            case 0:
                playTimeValue = 15.0f;
                break;
            case 1:
                playTimeValue = 30.0f;
                break;
            case 2:
                playTimeValue = 45.0f;
                break;
            case 3:
                playTimeValue = 60.0f;
                break;
            default:
                playTimeValue = 15.0f;
                break;
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
