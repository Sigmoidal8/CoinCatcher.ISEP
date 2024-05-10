using UnityEngine;

/// <summary>
/// This script extends the ClickButton class to handle clicking on a watch in the game.
/// </summary>
public class ClickWatch : ClickButton
{
    public GameObject Touchscreen;
    public GameObject Joystick;
    public GameObject Question;
    public SceneController SceneController;
    public HouseManager HouseManager;

    // Override the Action method to perform specific actions when the watch is clicked
    public override void Action()
    {
        Touchscreen.SetActive(false);
        Joystick.SetActive(false);
        Question.SetActive(true);
        SceneController.GetCurrentMoralDilemmaData().Timestamps.StartTimer();
        HouseManager.WatchFound = true;
    }
}
