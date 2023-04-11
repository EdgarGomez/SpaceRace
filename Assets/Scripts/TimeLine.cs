using UnityEngine;

public class TimeLine : MonoBehaviour
{
    public float countdownDuration = 10.0f;
    private float remainingTime;
    private Vector3 initialScale;
    private float initialYPosition;

    void Start()
    {
        remainingTime = countdownDuration;
        initialScale = transform.localScale;
        initialYPosition = transform.position.y;
    }

    void Update()
    {
        if (!GameManager.Instance.isGameOver || GameManager.Instance.isPaused)
        {
            if (GameManager.Instance.gameOverPanel.activeSelf)
            {
                return;
            }

            remainingTime -= Time.deltaTime;

            if (remainingTime > 0)
            {
                float progress = remainingTime / countdownDuration;
                float newYScale = initialScale.y * progress;
                transform.localScale = new Vector3(initialScale.x, newYScale, initialScale.z);

                float newYPosition = initialYPosition - (initialScale.y - newYScale) / 2;
                transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
            }
            else
            {
                transform.localScale = new Vector3(initialScale.x, 0, initialScale.z);
                GameManager.Instance.EndGame(GameManager.Instance.player1Score > GameManager.Instance.player2Score);
            }
        }
    }
}
