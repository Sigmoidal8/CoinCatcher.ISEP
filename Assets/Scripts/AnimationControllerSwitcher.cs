using UnityEngine;
using UnityEngine.Playables;

// This class is responsible for switching the Animator Controller of a game object.
public class AnimatorControllerSwitcher : MonoBehaviour
{
    public Animator Animator;
    public RuntimeAnimatorController NewController;

    void Start()
    {
        // Get the Timeline's playable director
        PlayableDirector director = GetComponent<PlayableDirector>();

        // Check if the director is valid
        if (director != null)
        {
            // Register a callback for when the Timeline ends
            director.stopped += OnTimelineEnd;
        }
    }

    // Method to switch the Animator Controller
    public void SwitchController(RuntimeAnimatorController animatorController)
    {
        Animator.runtimeAnimatorController = animatorController;
    }

    // Callback method called when the Timeline ends
    public void OnTimelineEnd(PlayableDirector director)
    {
        // Check if the provided director is the one we are interested in
        if (director == GetComponent<PlayableDirector>())
        {
            // Call the method to switch the Animator Controller
            SwitchController(NewController);
        }
    }
}
