using UnityEngine;

public class PlayerMovementIndicator : MonoBehaviour
{
    public GameObject indicatorObject;
    public AudioClip movementSound;
    public float movementSoundVolume = 0.5f;

    private PlayerController playerController;
    private AudioSource audioSource;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerController.isPenalized || GameManager.Instance.isGameOver)
        {
            indicatorObject.SetActive(false);
            return; // don't show indicator or play sound while penalized or game over
        }

        // Show indicator when the player is moving
        if (playerController.verticalInput != 0)
        {
            indicatorObject.SetActive(true);
            // Play movement sound
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(movementSound, movementSoundVolume);
            }
        }
        else
        {
            indicatorObject.SetActive(false);
            // Stop movement sound
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
