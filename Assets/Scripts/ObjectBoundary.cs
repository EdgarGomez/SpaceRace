using UnityEngine;

public class ObjectBoundary : MonoBehaviour
{
    private Transform oppositeSpawner;

    private void Update()
    {
        if (oppositeSpawner != null && transform.position.x < oppositeSpawner.position.x)
        {
            Destroy(gameObject);
        }
    }

    public void SetOppositeSpawner(Transform oppositeSpawnerTransform)
    {
        oppositeSpawner = oppositeSpawnerTransform;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.ResetPosition();
            playerController.PlayCollisionSound();
            playerController.SpawnCollisionEffect(collision.GetContact(0).point);

            Destroy(gameObject);
        }
    }
}
