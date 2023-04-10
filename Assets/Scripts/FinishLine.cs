using UnityEngine;
using TMPro;

public class FinishLine : MonoBehaviour
{
    private Vector3 player1InitialPosition = new Vector3(-2f, -4.1f, 0f);
    private Vector3 player2InitialPosition = new Vector3(2f, -4.1f, 0f);

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            GameManager.Instance.AddPoints(true, 1);
            other.transform.position = player1InitialPosition;
        }
        else if (other.CompareTag("Player2"))
        {
            GameManager.Instance.AddPoints(false, 1);
            other.transform.position = player2InitialPosition;
        }
    }
}
