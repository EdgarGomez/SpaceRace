using System.Collections;
using UnityEngine;

public class LeftSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public float spawnInterval = 2f;
    public float minSpeed = 1f;
    public float maxSpeed = 5f;
    public float minDistanceBetweenObjects = 2f;

    private BoxCollider2D spawnerCollider;
    private float lastSpawnedObjectY;

    void Start()
    {
        spawnerCollider = GetComponent<BoxCollider2D>();
        StartCoroutine(SpawnObjects());
    }

    public void SetDifficulty(int difficulty)
    {
        // Set the difficulty level for this spawner
        // You can use this method to configure the enemy waves based on the difficulty level
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (GameManager.Instance.isGameOver) // Don't spawn objects if game is over
            {
                continue;
            }

            float randomY = Random.Range(spawnerCollider.bounds.min.y, spawnerCollider.bounds.max.y);
            Vector2 spawnPosition = new Vector2(transform.position.x, randomY);

            if (Mathf.Abs(spawnPosition.y - lastSpawnedObjectY) >= minDistanceBetweenObjects)
            {
                int randomIndex = Random.Range(0, objectsToSpawn.Length);
                GameObject objectToSpawn = Instantiate(objectsToSpawn[randomIndex], spawnPosition, objectsToSpawn[randomIndex].transform.rotation);

                float speed = Random.Range(minSpeed, maxSpeed);
                objectToSpawn.GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;

                lastSpawnedObjectY = spawnPosition.y;
            }
        }
    }
}
