using UnityEngine;

public class ClickHomeless : ClickButton
{
    public GameObject Touchscreen;
    public GameObject Joystick;
    public GameObject Question;
    public SceneController SceneController;

    public override void Action()
    {
        if (!SceneController.IsSceneDilemaCompleted())
        {
            SceneController.GetCurrentMoralDilemmaData().timestamps.StartTimer();
            Touchscreen.SetActive(false);
            Joystick.SetActive(false);
            Question.SetActive(true);
        }

    }
}