using UnityEngine;

public class AnimationPlaySound : MonoBehaviour
{
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void PlaySound()
    {
        audioSource.Play();
    }
}
