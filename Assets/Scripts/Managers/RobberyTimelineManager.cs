using UnityEngine;
using UnityEngine.Playables;

public class RobberyTimelineManager : MonoBehaviour
{
    public SceneController sceneController;

    public GameObject HelpPanelButton;
    public GameObject HelpPanel;
    public RobberySceneUIManager UIManager;

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

    [Header("Controls")]
    public PlayableDirector Director;
    public GameObject DilemmaPanel;
    public ArrowManager ArrowManager;
    public GameObject Player;

    public GameObject NPCCop;

    // Start is called before the first frame update
    void Start()
    {
        if (sceneController.IsSceneDilemaCompleted())
        {
            Thief.GetComponent<AnimatorControllerSwitcher>().SwitchController(ThiefController);
            Animator elderAnimator = Elder.GetComponent<Animator>();

            NPCCop.transform.position = new Vector3(25.15706f, -0.8111502f, 7.809397f);
            NPCCop.transform.rotation = Quaternion.Euler(0, 86.987f, 0);

            Touchscreen.SetActive(true);
            Joystick.SetActive(true);
            if (sceneController.IsSceneResolvedWithValue(1))
            {
                Thief.transform.position = new Vector3(7.92f, 0.102f, 11.08f);
                Thief.transform.rotation = Quaternion.Euler(0, 76.693f, 0);
                if (sceneController.IsNextSceneDilemaCompleted())
                {
                    elderAnimator.SetBool("DilemmaResolved", true);

                    switch (sceneController.NextSceneResolvedWithValue())
                    {
                        case 1:
                            BriefcaseThief.SetActive(false);
                            BriefcaseElder.SetActive(true);
                            elderAnimator.SetBool("HasBriefcase", true);
                            Elder.GetComponent<AnimatorControllerSwitcher>().SwitchController(ElderHappyController);
                            Player.transform.position = new Vector3(-230.373f, 32.968f, 7.05f);
                            break;
                        case 2:
                            BriefcaseThief.SetActive(false);
                            BriefcaseElder.SetActive(false);
                            elderAnimator.SetBool("HasBriefcase", false);
                            Elder.GetComponent<AnimatorControllerSwitcher>().SwitchController(ElderAngryController);
                            break;
                        case 3:
                            BriefcaseThief.SetActive(false);
                            BriefcaseElder.SetActive(true);
                            elderAnimator.SetBool("HasBriefcase", true);
                            Elder.GetComponent<AnimatorControllerSwitcher>().SwitchController(ElderHappyController);
                            break;
                        case 4:
                            BriefcaseThief.SetActive(true);
                            BriefcaseElder.SetActive(false);
                            elderAnimator.SetBool("HasBriefcase", false);
                            Elder.GetComponent<AnimatorControllerSwitcher>().SwitchController(ElderAngryController);
                            break;
                    }
                }
            }
            else if (sceneController.IsSceneResolvedWithValue(2))
            {
                Thief.SetActive(false);
                BriefcaseElder.SetActive(false);
                elderAnimator.SetBool("DilemmaResolved", true);
                elderAnimator.SetBool("HasBriefcase", false);
                Elder.GetComponent<AnimatorControllerSwitcher>().SwitchController(ElderSittingController);
            }
            else if (sceneController.IsSceneResolvedWithValue(3))
            {
                Thief.SetActive(false);
                BriefcaseElder.SetActive(false);
                elderAnimator.SetBool("DilemmaResolved", true);
                elderAnimator.SetBool("HasBriefcase", false);
                Elder.GetComponent<AnimatorControllerSwitcher>().SwitchController(ElderSittingController);
            }
            gameObject.SetActive(false);
        }
        if (sceneController.isGameOver)
        {
            HelpPanelButton.SetActive(true);
        }
    }

    public void ShowDilemmaPanel()
    {
        sceneController.GetCurrentMoralDilemmaData().timestamps.StartTimer();
        DilemmaPanel.SetActive(true);
    }

    public void HidePanel(int chosenOption)
    {
        Thief.GetComponent<AnimatorControllerSwitcher>().SwitchController(ThiefController);
        Animator elderAnimator = Elder.GetComponent<Animator>();
        switch (chosenOption)
        {
            case 2:
                Thief.SetActive(false);
                BriefcaseElder.SetActive(false);
                elderAnimator.SetBool("DilemmaResolved", true);
                elderAnimator.SetBool("HasBriefcase", false);
                Elder.GetComponent<AnimatorControllerSwitcher>().SwitchController(ElderSittingController);
                break;
            case 3:
                Thief.SetActive(false);
                BriefcaseElder.SetActive(false);
                elderAnimator.SetBool("DilemmaResolved", true);
                elderAnimator.SetBool("HasBriefcase", false);
                Elder.GetComponent<AnimatorControllerSwitcher>().SwitchController(ElderSittingController);
                break;
        }
        NPCCop.transform.position = new Vector3(25.15706f, -0.8111502f, 7.809397f);
        NPCCop.transform.rotation = Quaternion.Euler(0, 86.987f, 0);
        DilemmaPanel.SetActive(false);
        Touchscreen.SetActive(true);
        Joystick.SetActive(true);
        sceneController.isGameOver = true;
        sceneController.SaveGameData();
        HelpPanelButton.SetActive(true);
    }

    public void FollowThief()
    {
        sceneController.GetCurrentMoralDilemmaData().timestamps.StopTimer();
        sceneController.SaveGameData();
        ArrowManager.GoToNextScene();
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
