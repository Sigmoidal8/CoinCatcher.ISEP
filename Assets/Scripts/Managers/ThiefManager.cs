using UnityEngine;

public class ThiefManager : MonoBehaviour
{
    public GameObject HelpPanelButton;
    public GameObject HelpPanel;
    public ThiefSceneUIManager UIManager;
    public SceneController sceneController;

    [Header("TouchControls")]
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

    [Header("Panel")]
    public GameObject DilemmaPanel;

    [Header("Buttons")]
    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;
    public GameObject Button4;
    public GameObject NextButton;
    public GameObject FistMessage;
    public GameObject SecondMessage;

    // Start is called before the first frame update
    void Start()
    {
        int previousSceneResponse = sceneController.PreviousSceneResolvedWithValue();
        if (sceneController.IsSceneDilemaCompleted() || previousSceneResponse == 2 || previousSceneResponse == 3)
        {
            DilemmaPanel.SetActive(false);
            Joystick.SetActive(true);
            Touchscreen.SetActive(true);
            if (sceneController.IsSceneResolvedWithValue(1))
            {
                AnimateElderScene1();
            }
            else if (sceneController.IsSceneResolvedWithValue(2))
            {
                AnimateElderScene2();
            }
            else if (sceneController.IsSceneResolvedWithValue(3))
            {
                AnimateElderScene3();
            }
            else if (sceneController.IsSceneResolvedWithValue(4))
            {
                AnimateElderScene4();
            }
        }
        if (previousSceneResponse == 2 || previousSceneResponse == 3)
        {
            Thief.SetActive(false);
        }
        if (sceneController.isGameOver)
        {
            HelpPanelButton.SetActive(true);
        }
    }

    public void PressedNextButton()
    {
        NextButton.SetActive(false);
        FistMessage.SetActive(false);

        Button1.SetActive(true);
        Button2.SetActive(true);
        Button3.SetActive(true);
        Button4.SetActive(true);
        SecondMessage.SetActive(true);
        sceneController.GetCurrentMoralDilemmaData().timestamps.StartTimer();
    }

    public void HidePanel(int chosenOption)
    {
        sceneController.GetCurrentMoralDilemmaData().timestamps.StopTimer();
        sceneController.isGameOver = true;
        sceneController.SaveGameData();
        HelpPanelButton.SetActive(true);
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

    public void AnimateElderScene1()
    {
        Animator elderAnimator = Elder.GetComponent<Animator>();
        BriefcaseThief.SetActive(false);
        BriefcaseElder.SetActive(true);
        elderAnimator.SetBool("HasBriefcase", true);
        Elder.GetComponent<AnimatorControllerSwitcher>().SwitchController(ElderHappyController);
    }

    public void AnimateElderScene2()
    {
        Animator elderAnimator = Elder.GetComponent<Animator>();
        BriefcaseThief.SetActive(false);
        BriefcaseElder.SetActive(false);
        elderAnimator.SetBool("HasBriefcase", false);
        Elder.GetComponent<AnimatorControllerSwitcher>().SwitchController(ElderAngryController);
    }

    public void AnimateElderScene3()
    {
        Animator elderAnimator = Elder.GetComponent<Animator>();
        BriefcaseThief.SetActive(false);
        BriefcaseElder.SetActive(true);
        elderAnimator.SetBool("HasBriefcase", true);
        Elder.GetComponent<AnimatorControllerSwitcher>().SwitchController(ElderHappyController);
    }

    public void AnimateElderScene4()
    {
        Animator elderAnimator = Elder.GetComponent<Animator>();
        BriefcaseThief.SetActive(true);
        BriefcaseElder.SetActive(false);
        elderAnimator.SetBool("HasBriefcase", false);
        Elder.GetComponent<AnimatorControllerSwitcher>().SwitchController(ElderAngryController);
    }

    public void ShowHelpPanel()
    {
        Joystick.SetActive(false);
        Touchscreen.SetActive(false);
        UIManager.FillHelpMessage(CoinManager.instance.CountMissingCoinsFromScene());
        HelpPanel.SetActive(true);
    }

    public void CloseHelpPanel()
    {
        HelpPanel.SetActive(false);
        Joystick.SetActive(true);
        Touchscreen.SetActive(true);
    }
}
