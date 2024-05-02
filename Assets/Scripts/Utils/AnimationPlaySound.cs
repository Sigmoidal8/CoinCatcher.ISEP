using UnityEngine;

// This script plays an audio clip when triggered by an animation event.
public class AnimationPlaySound : MonoBehaviour
{
    AudioSource AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        AudioSource = GetComponent<AudioSource>();
    }

    // Method to play the assigned audio clip
    private void PlaySound()
    {
        // Play the audio clip associated with the AudioSource component
        AudioSource.Play();
    }
}
