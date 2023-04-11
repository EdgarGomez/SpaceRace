using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public bool isPlayer1 = true;
    public AudioClip collisionSound;
    public GameObject collisionEffectPrefab;
    public float penalizationTime = 3f;

    private Vector2 initialPosition;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    public float verticalInput;
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
            return;
        }

        if (isPlayer1)
        {
            verticalInput = Input.GetAxis("Vertical1");
        }
        else
        {
            verticalInput = Input.GetAxis("Vertical2");
        }

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
        Destroy(effectInstance, 1.5f);
    }

    IEnumerator PenalizePlayer()
    {
        isPenalized = true;
        GameManager.Instance.DeductPoints(isPlayer1, 1);
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
            PlayCollisionSound();
            SpawnCollisionEffect(collision.contacts[0].point);
            StartCoroutine(PenalizePlayer());
            ResetPosition();
            Destroy(collision.gameObject);
        }
    }
}
