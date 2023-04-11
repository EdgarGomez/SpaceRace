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
            return;
        }

        if (playerController.verticalInput != 0)
        {
            indicatorObject.SetActive(true);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(movementSound, movementSoundVolume);
            }
        }
        else
        {
            indicatorObject.SetActive(false);
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
