using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Manages the Robbery Scene and Timeline
/// </summary>
public class RobberyTimelineManager : MonoBehaviour
{
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
    public GameObject Touchscreen;
    public GameObject Joystick;
    public PlayableDirector Director;

    [Header("Panels")]
    public GameObject DilemmaPanel;
    public GameObject HelpPanelButton;
    public GameObject HelpPanel;

    [Header("Managers & Controllers")]
    public SceneController SceneController;
    public RobberySceneUIManager UIManager;
    public ArrowManager ArrowManager;

    [Header("Others")]
    public GameObject Player;
    public GameObject NPCCop;

    // Start is called before the first frame update
    void Start()
    {
        // Check if the dilemma in the scene is completed
        if (SceneController.IsSceneDilemaCompleted())
        {
            // Set up scene based on completed dilemma
            SetupScene();
        }
        // Show help panel button if game over
        if (SceneController.IsGameOver)
        {
            HelpPanelButton.SetActive(true);
        }
    }

    // Method to set up the scene based on completed dilemma
    private void SetupScene()
    {
        Thief.GetComponent<AnimatorControllerSwitcher>().SwitchController(ThiefController);
        // Get animator for elder character
        Animator elderAnimator = Elder.GetComponent<Animator>();

        NPCCop.transform.position = new Vector3(25.15706f, -0.8111502f, 7.809397f);
        NPCCop.transform.rotation = Quaternion.Euler(0, 86.987f, 0);

        Touchscreen.SetActive(true);
        Joystick.SetActive(true);

        // Set up scene based on resolved dilemma option
        if (SceneController.IsSceneResolvedWithValue(1))
        {
            // Set up scene for resolved option 1
            SetupOption1(elderAnimator);
        }
        else if (SceneController.IsSceneResolvedWithValue(2) || SceneController.IsSceneResolvedWithValue(3))
        {
            // Set up scene for resolved option 2 or 3
            SetupOption2Or3(elderAnimator);
        }

        // Disable the robbery timeline manager
        gameObject.SetActive(false);
    }

    // Method to set up scene for resolved option 1
    private void SetupOption1(Animator elderAnimator)
    {
        // Set thief's position and rotation
        Thief.transform.position = new Vector3(7.92f, 0.102f, 11.08f);
        Thief.transform.rotation = Quaternion.Euler(0, 76.693f, 0);

        // Check if next scene's dilemma is completed
        if (SceneController.IsNextSceneDilemaCompleted())
        {
            // Set elder's animator and position based on next scene's resolution
            SetupElderForNextScene(elderAnimator);
        }
        else
        {
            Elder.GetComponent<AnimatorControllerSwitcher>().SwitchController(ElderSittingController);
        }
    }

    // Method to set up scene for resolved option 2 or 3
    private void SetupOption2Or3(Animator elderAnimator)
    {
        // Disable thief and elder's briefcase
        Thief.SetActive(false);
        BriefcaseElder.SetActive(false);

        // Set elder's animator and position
        elderAnimator.SetBool("DilemmaResolved", true);
        elderAnimator.SetBool("HasBriefcase", false);
        Elder.GetComponent<AnimatorControllerSwitcher>().SwitchController(ElderSittingController);
    }

    // Method to set up elder for next scene based on resolution
    private void SetupElderForNextScene(Animator elderAnimator)
    {
        switch (SceneController.NextSceneResolvedWithValue())
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

    // Method to show dilemma panel
    public void ShowDilemmaPanel()
    {
        SceneController.GetCurrentMoralDilemmaData().Timestamps.StartTimer();
        DilemmaPanel.SetActive(true);
    }

    // Method to hide panel based on chosen option
    public void HidePanel(int chosenOption)
    {
        SceneController.GetCurrentMoralDilemmaData().Timestamps.StopTimer();
        // Set game over and save game data
        SceneController.IsGameOver = true;
        SceneController.SaveGameData();

        // Set up scene based on chosen option
        SetupSceneForChosenOption(chosenOption);

        // Reset NPC cop position and rotation
        NPCCop.transform.position = new Vector3(25.15706f, -0.8111502f, 7.809397f);
        NPCCop.transform.rotation = Quaternion.Euler(0, 86.987f, 0);

        // Hide dilemma panel and enable touch controls
        DilemmaPanel.SetActive(false);
        Touchscreen.SetActive(true);
        Joystick.SetActive(true);

        // Show help panel button
        HelpPanelButton.SetActive(true);
    }

    // Method to set up scene based on chosen option
    private void SetupSceneForChosenOption(int chosenOption)
    {
        // Get animator for elder character
        Animator elderAnimator = Elder.GetComponent<Animator>();

        // Set up scene based on chosen option
        switch (chosenOption)
        {
            case 2:
            case 3:
                // Set up scene for options 2 or 3
                SetupOption2Or3(elderAnimator);
                break;
        }
    }

    // Method to make player follow thief
    public void FollowThief()
    {
        // Stop timer and save game data
        SceneController.GetCurrentMoralDilemmaData().Timestamps.StopTimer();
        SceneController.SaveGameData();
        // Go to next scene
        ArrowManager.GoToNextScene();
    }

    // Method to show help panel
    public void ShowHelpPanel()
    {
        Joystick.SetActive(false);
        Touchscreen.SetActive(false);
        UIManager.FillHelpMessage(CoinManager.Instance.CountMissingCoinsFromScene());
        HelpPanel.SetActive(true);
    }

    // Method to close help panel
    public void CloseHelpPanel()
    {
        HelpPanel.SetActive(false);
        Joystick.SetActive(true);
        Touchscreen.SetActive(true);
    }
}
