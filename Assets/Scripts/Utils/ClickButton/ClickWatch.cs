using UnityEngine;

public class ClickWatch : ClickButton
{
    public GameObject Touchscreen;
    public GameObject Joystick;
    public GameObject Question;
    public SceneController SceneController;

    public override void Action()
    {
        Touchscreen.SetActive(false);
        Joystick.SetActive(false);
        Question.SetActive(true);
        SceneController.GetCurrentMoralDilemmaData().timestamps.StartTimer();
    }
}
