using UnityEngine;

public class TimeLine : MonoBehaviour
{
    public float countdownDuration = 10.0f; // Set the duration of the countdown in seconds
    private float remainingTime;
    private Vector3 initialScale;
    private float initialYPosition;

    void Start()
    {
        remainingTime = countdownDuration;
        initialScale = transform.localScale; // Store the initial scale of the object
        initialYPosition = transform.position.y; // Store the initial Y position of the object
    }

    void Update()
    {
        if (!GameManager.Instance.isGameOver)
        {
            if (GameManager.Instance.gameOverPanel.activeSelf)
            {
                return;
            }

            remainingTime -= Time.deltaTime; // Decrease the remaining time by the elapsed time since the last frame

            if (remainingTime > 0)
            {
                float progress = remainingTime / countdownDuration;
                float newYScale = initialScale.y * progress; // Calculate the new Y scale
                transform.localScale = new Vector3(initialScale.x, newYScale, initialScale.z); // Update only the Y scale

                // Move the object downwards to keep the bottom side static
                float newYPosition = initialYPosition - (initialScale.y - newYScale) / 2;
                transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
            }
            else
            {
                transform.localScale = new Vector3(initialScale.x, 0, initialScale.z); // Set the Y scale to zero when the countdown ends
                GameManager.Instance.EndGame(GameManager.Instance.player1Score > GameManager.Instance.player2Score);
            }
        }
    }
}
