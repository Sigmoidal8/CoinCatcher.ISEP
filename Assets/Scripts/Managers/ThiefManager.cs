using UnityEngine;

/// <summary>
/// Manages the Thief Scene
/// </summary>
public class ThiefManager : MonoBehaviour
{
    [Header("Controls")]
    public GameObject Touchscreen;
    public GameObject Joystick;

    [Header("Elder")]
    public GameObject BriefcaseElder;
    public GameObject Elder;
    public RuntimeAnimatorController ElderSittingController;
    public RuntimeAnimatorController ElderHappyController;
    public RuntimeAnimatorController ElderAngryController;

    [Header("Thief")]
    public GameObject BriefcaseThief;
    public GameObject Thief;
    public RuntimeAnimatorController ThiefController;

    [Header("Panels")]
    public GameObject DilemmaPanel;
    public GameObject HelpPanel;
    public GameObject HelpPanelButton;

    [Header("Dilemma Buttons & Messages")]
    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;
    public GameObject Button4;
    public GameObject NextButton;
    public GameObject FistMessage;
    public GameObject SecondMessage;

    [Header("Managers & Controllers")]
    public ThiefSceneUIManager UIManager;
    public SceneController SceneController;

    // Start is called before the first frame update
    void Start()
    {
        // Check if the dilemma in the scene is completed or if the previous scene had specific responses
        int previousSceneResponse = SceneController.PreviousSceneResolvedWithValue();
        if (SceneController.IsSceneDilemaCompleted() || previousSceneResponse == 2 || previousSceneResponse == 3)
        {
            DilemmaPanel.SetActive(false);
            Joystick.SetActive(true);
            Touchscreen.SetActive(true);
            // Check the resolution of the current scene and animate the Elder accordingly
            if (SceneController.IsSceneResolvedWithValue(1))
            {
                AnimateElderScene1();
            }
            else if (SceneController.IsSceneResolvedWithValue(2))
            {
                AnimateElderScene2();
            }
            else if (SceneController.IsSceneResolvedWithValue(3))
            {
                AnimateElderScene3();
            }
            else if (SceneController.IsSceneResolvedWithValue(4))
            {
                AnimateElderScene4();
            }
        }
        if (previousSceneResponse == 2 || previousSceneResponse == 3)
        {
            Thief.SetActive(false);
        }
        // Show help panel button if game over
        if (SceneController.IsGameOver)
        {
            HelpPanelButton.SetActive(true);
        }
    }

    // Method called when the "Next" button is pressed to proceed after the first message
    public void PressedNextButton()
    {
        // Hide the first message and show the dilemma buttons and second message
        NextButton.SetActive(false);
        FistMessage.SetActive(false);

        Button1.SetActive(true);
        Button2.SetActive(true);
        Button3.SetActive(true);
        Button4.SetActive(true);
        SecondMessage.SetActive(true);

        // Start timer for the dilemma
        SceneController.GetCurrentMoralDilemmaData().Timestamps.StartTimer();
    }

    // Method called to hide the dilemma panel based on the chosen option
    public void HidePanel(int chosenOption)
    {
        // Stop timer for the dilemma, set game over, and save game data
        SceneController.GetCurrentMoralDilemmaData().Timestamps.StopTimer();
        SceneController.IsGameOver = true;
        SceneController.SaveGameData();
        HelpPanelButton.SetActive(true);

        // Animate Elder and hide the dilemma panel
        switch (chosenOption)
        {
            case 2:
                AnimateElderScene2();
                break;
            case 3:
                AnimateElderScene3();
                break;
            case 4:
                AnimateElderScene4();
                break;
        }
        DilemmaPanel.SetActive(false);
        Touchscreen.SetActive(true);
        Joystick.SetActive(true);
    }

    // Method to animate Elder for Scene resolution 1
    public void AnimateElderScene1()
    {
        Animator elderAnimator = Elder.GetComponent<Animator>();
        BriefcaseThief.SetActive(false);
        BriefcaseElder.SetActive(true);
        elderAnimator.SetBool("HasBriefcase", true);
        Elder.GetComponent<AnimatorControllerSwitcher>().SwitchController(ElderHappyController);
    }

    // Method to animate Elder for Scene resolution 2
    public void AnimateElderScene2()
    {
        Animator elderAnimator = Elder.GetComponent<Animator>();
        BriefcaseThief.SetActive(false);
        BriefcaseElder.SetActive(false);
        elderAnimator.SetBool("HasBriefcase", false);
        Elder.GetComponent<AnimatorControllerSwitcher>().SwitchController(ElderAngryController);
    }

    // Method to animate Elder for Scene resolution 3
    public void AnimateElderScene3()
    {
        Animator elderAnimator = Elder.GetComponent<Animator>();
        BriefcaseThief.SetActive(false);
        BriefcaseElder.SetActive(true);
        elderAnimator.SetBool("HasBriefcase", true);
        Elder.GetComponent<AnimatorControllerSwitcher>().SwitchController(ElderHappyController);
    }

    // Method to animate Elder for Scene resolution 4
    public void AnimateElderScene4()
    {
        Animator elderAnimator = Elder.GetComponent<Animator>();
        BriefcaseThief.SetActive(true);
        BriefcaseElder.SetActive(false);
        elderAnimator.SetBool("HasBriefcase", false);
        Elder.GetComponent<AnimatorControllerSwitcher>().SwitchController(ElderAngryController);
    }

    // Method to show the help panel
    public void ShowHelpPanel()
    {
        Joystick.SetActive(false);
        Touchscreen.SetActive(false);
        UIManager.FillHelpMessage(CoinManager.Instance.CountMissingCoinsFromScene());
        HelpPanel.SetActive(true);
    }

    // Method to close the help panel
    public void CloseHelpPanel()
    {
        HelpPanel.SetActive(false);
        Joystick.SetActive(true);
        Touchscreen.SetActive(true);
    }
}
