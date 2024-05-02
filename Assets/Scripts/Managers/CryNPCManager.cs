using System.Collections;
using UnityEngine;

/// <summary>
/// Manages the NPC crying sound
/// </summary>
public class CryNPCManager : MonoBehaviour
{
    // Audio source for Crying sound
    public AudioSource Crying;
    // Reference to the SceneController script
    public SceneController SceneController;

    // Flag to track if the NPC is currently Crying
    private bool isCrying = false;
    // Time interval between crying sounds
    private float secondsBetweenCrying = 10f;

    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine for crying loop
        StartCoroutine(CryLoop());
    }

    // Coroutine for continuous crying loop
    IEnumerator CryLoop()
    {
        while (true)
        {
            // Check if the NPC is not already crying and the scene dilemma is not completed
            if (!isCrying && !SceneController.IsSceneDilemaCompleted())
            {
                // Set the NPC to crying state
                isCrying = true;
                // Play the crying sound
                if (Crying != null)
                {
                    Crying.Play();
                    // Wait for the duration of the crying sound plus the specified interval
                    yield return new WaitForSeconds(Crying.clip.length + secondsBetweenCrying);
                }
                // Reset the crying state after the sound ends
                isCrying = false;
            }
            // Yield to the next frame
            yield return null;
        }
    }
}