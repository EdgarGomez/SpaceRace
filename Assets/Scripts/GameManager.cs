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
