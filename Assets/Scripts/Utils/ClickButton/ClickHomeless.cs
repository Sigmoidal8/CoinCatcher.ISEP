using UnityEngine;

/// <summary>
/// This script extends the ClickButton class to handle clicking on a homeless person in the game.
/// </summary>
public class ClickHomeless : ClickButton
{
    public GameObject Touchscreen;
    public GameObject Joystick;
    public GameObject Question;
    public SceneController SceneController;

    // Override the Action method to perform specific actions when the homeless person is clicked
    public override void Action()
    {
        // Check if the scene dilemma is not completed
        if (!SceneController.IsSceneDilemaCompleted())
        {
            // Start the timer for the current moral dilemma data
            SceneController.GetCurrentMoralDilemmaData().Timestamps.StartTimer();
            Touchscreen.SetActive(false);
            Joystick.SetActive(false);
            // Show the question UI element
            Question.SetActive(true);
        }

    }
}