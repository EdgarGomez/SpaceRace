using System.Collections;
using UnityEngine;

public class RightSpawner : MonoBehaviour
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
        switch (GameManager.Instance.difficultyLevel)
        {
            case 0:
                spawnInterval = 2f;
                minSpeed = 1f;
                maxSpeed = 2f;
                break;
            case 1:
                spawnInterval = 1.5f;
                minSpeed = 1f;
                maxSpeed = 3f;
                break;
            case 2:
                spawnInterval = 1f;
                minSpeed = 2f;
                maxSpeed = 5f;
                break;
            default:
                spawnInterval = 2f;
                minSpeed = 1f;
                maxSpeed = 2f;
                break;
        }
        spawnerCollider = GetComponent<BoxCollider2D>();
        StartCoroutine(SpawnObjects());

    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (GameManager.Instance.isGameOver || GameManager.Instance.isPaused)
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
                objectToSpawn.GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;

                lastSpawnedObjectY = spawnPosition.y;
            }
        }
    }
}
