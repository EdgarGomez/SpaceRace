using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // Set the player's movement speed
    public bool isPlayer1 = true; // Set this flag to true for Player 1 and false for Player 2 in the Unity Inspector

    public AudioClip collisionSound;
    public GameObject collisionEffectPrefab; // Drag the new animation prefab in the Inspector

    private Vector2 initialPosition;
    private AudioSource audioSource;

    public float verticalInput;

    public float penalizationTime = 3f;
    private float timeUntilMovementAllowed;

    private SpriteRenderer spriteRenderer;

    public bool isPenalized = false;

    public bool isMovable = true;


    void Start()
    {
        initialPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isPenalized || GameManager.Instance.isGameOver)
        {
            return; // don't allow any movement input while penalized
        }

        // Get input based on whether this is Player 1 or Player 2
        if (isPlayer1)
        {
            verticalInput = Input.GetAxis("Vertical1"); // Set up a new input axis named "Vertical1" for Player 1 (W and S keys)
        }
        else
        {
            verticalInput = Input.GetAxis("Vertical2"); // Set up a new input axis named "Vertical2" for Player 2 (Up and Down arrow keys)
        }

        // Move the player's ship based on the input
        transform.Translate(Vector3.up * verticalInput * speed * Time.deltaTime);
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
    }

    public void PlayCollisionSound()
    {
        audioSource.PlayOneShot(collisionSound);
    }

    public void SpawnCollisionEffect(Vector2 position)
    {
        GameObject effectInstance = Instantiate(collisionEffectPrefab, position + new Vector2(-0.7f, -0.7f), Quaternion.identity);
        // Adjust the duration based on your animation length, e.g., 1.5f for 1.5 seconds
        Destroy(effectInstance, 1.5f);
    }

    public void SetShipDesign(int designIndex)
    {
        // TODO: Implement ship design selection logic
    }

    IEnumerator PenalizePlayer()
    {
        isPenalized = true;
        GameManager.Instance.DeductPoints(isPlayer1, 1); // Deduct 1 point
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(penalizationTime);
        transform.position = initialPosition;
        spriteRenderer.enabled = true;
        isPenalized = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Play the collision sound and spawn the effect
            PlayCollisionSound();
            SpawnCollisionEffect(collision.contacts[0].point);

            // Penalize the player
            StartCoroutine(PenalizePlayer());

            // Reset the player's position
            ResetPosition();

            // Destroy the collided obstacle
            Destroy(collision.gameObject);
        }
    }

}
