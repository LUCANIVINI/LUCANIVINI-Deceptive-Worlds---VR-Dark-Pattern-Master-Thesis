using UnityEngine;

public class TickingClock : MonoBehaviour
{
    public AudioClip tickingSound; // Sound clip to play
    private AudioSource audioSource; // AudioSource component

    private void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Assign the sound clip to the AudioSource
        audioSource.clip = tickingSound;

        // Play the sound in a loop
        audioSource.loop = true;
        audioSource.Play();

        // Start increasing the pitch of the sound
        StartCoroutine(IncreasePitch());
    }

    private System.Collections.IEnumerator IncreasePitch()
    {
        float currentPitch = audioSource.pitch;
        while (true)
        {
            // Increase pitch by 1% every second
            currentPitch += currentPitch * 0.03f;

            // Set the new pitch
            audioSource.pitch = currentPitch;

            // Wait for 1 second
            yield return new WaitForSeconds(1f);
        }
    }
}
