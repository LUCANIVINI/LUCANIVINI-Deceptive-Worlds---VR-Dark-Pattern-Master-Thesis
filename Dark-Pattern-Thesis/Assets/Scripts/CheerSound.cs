using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheerSound : MonoBehaviour
{
    public AudioClip[] initialClips = new AudioClip[2];  // Two clips for the Initial option
    public AudioClip[] enterClips = new AudioClip[2];    // Two clips for the Enter option
    public AudioClip[] exitClips = new AudioClip[2];     // Two clips for the Exit option

    public GameObject TriggerObject;  // Reference to the specific object (e.g., the player) to check for
    public GameObject objectToActivate;  // Object to activate when a clip is playing
    public bool activateObjectWhenPlaying = false;  // Checkbox to activate object during clip playback

    public float initialClipDelay = 3f;  // Delay before the Initial clip plays initially (default: 3 seconds)
    public float initialClipInterval = 15f;  // Interval for the Initial clip to repeat if player is not in the zone (default: 15 seconds)
    public float exitClipDelay = 2f;  // Delay before the exit clip plays when the player exits the zone (default: 2 seconds)

    private AudioSource audioSource;  // Audio source component
    private bool playerInZone = false;  // Flag to track if the player is in the zone
    private bool exitClipPlaying = false;  // Flag to track if the exit clip is playing
    private bool initialClipPlaying = false;  // Flag to track if the Initial clip is playing

    private float initialClipTimer = 0f;  // Timer for the Initial clip

    void Start()
    {
        // Ensure there is an AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;  // Prevent the AudioSource from playing on awake

        // Start the Initial clip after the initial delay
        StartCoroutine(PlayInitialClipAfterDelay());
    }

    void Update()
    {
        // Activate or deactivate the specified object if a clip is playing
        if (activateObjectWhenPlaying && objectToActivate != null)
        {
            objectToActivate.SetActive(audioSource.isPlaying);
        }

        // Handle the InitialClip timer if the player is not in the zone, the exit clip is not playing, and the Initial clip is not playing
        if (!playerInZone && !exitClipPlaying && !initialClipPlaying)
        {
            initialClipTimer += Time.deltaTime;
            if (initialClipTimer >= initialClipInterval)
            {
                StartCoroutine(PlayInitialClip());
                initialClipTimer = 0f;  // Reset the timer after playing the Initial clip
            }
        }
    }

    // Play the enter clip when the player enters the zone
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == TriggerObject)
        {
            playerInZone = true;
            initialClipTimer = 0f;  // Stop the InitialClip timer when the player enters
            PlayEnterClip();
        }
    }

    // Play the exit clip when the player exits the zone
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == TriggerObject)
        {
            playerInZone = false;
            StartCoroutine(PlayExitClipWithDelay());
        }
    }

    // Play the Initial clip after a delay when the object is enabled
    IEnumerator PlayInitialClipAfterDelay()
    {
        yield return new WaitForSeconds(initialClipDelay);
        yield return StartCoroutine(PlayInitialClip());
    }

    // Play a randomly selected Initial clip
    IEnumerator PlayInitialClip()
    {
        if (!playerInZone && !exitClipPlaying)
        {
            initialClipPlaying = true;
            audioSource.clip = GetRandomClip(initialClips);
            audioSource.Play();

            // Wait for the Initial clip to finish playing before starting the timer
            yield return new WaitWhile(() => audioSource.isPlaying);

            initialClipPlaying = false;
        }
    }

    // Play a randomly selected Enter clip
    void PlayEnterClip()
    {
        audioSource.clip = GetRandomClip(enterClips);
        audioSource.Play();
    }

    // Play a randomly selected Exit clip after a delay
    IEnumerator PlayExitClipWithDelay()
    {
        yield return new WaitForSeconds(exitClipDelay);
        audioSource.clip = GetRandomClip(exitClips);
        audioSource.Play();
        exitClipPlaying = true;

        // Wait for the exit clip to finish playing before resetting the Initial clip timer
        yield return new WaitWhile(() => audioSource.isPlaying);
        exitClipPlaying = false;
        initialClipTimer = 0f;  // Reset the Initial clip timer when the player exits and the exit clip finishes
    }

    // Get a random clip from the given array of audio clips
    AudioClip GetRandomClip(AudioClip[] clips)
    {
        if (clips.Length == 0)
        {
            return null;
        }
        int randomIndex = Random.Range(0, clips.Length);
        return clips[randomIndex];
    }
}
