using System.Collections;
using UnityEngine;

/// <summary>
/// Manages the House scene
/// </summary>
public class HouseManager : MonoBehaviour
{
    [Header("Controls")]
    public GameObject TouchScreen;
    public GameObject Joystick;
    [Header("Panels")]
    public GameObject InfoCanvas;
    public GameObject DilemaMessagePanel;
    public GameObject HelpPanel;
    [Header("Watch")]
    public GameObject Watch;
    [Header("Watch Dilemma Buttons")]
    public GameObject Option1Button;
    public GameObject Option2Button;
    public GameObject CloseMessageButton;
    [Header("Help Panel Button")]
    public GameObject HelpPanelButton;
    [Header("Managers & Controllers")]
    public HouseSceneUIManager UIManager;
    public SceneController SceneController;
    public float DelayInSeconds = 30f;
    public bool WatchFound = false;

    // Value indicating to ignore the option chosen in a dilemma
    private static int IgnoreOptionDilemaValue = 3;

    // Start is called before the first frame update
    void Start()
    {
        // Check if the dilemma in the scene is completed
        if (SceneController.IsSceneDilemaCompleted())
        {
            // Disable the house manager and related UI elements
            gameObject.SetActive(false);
            Watch.SetActive(false);
            TouchScreen.SetActive(true);
            Joystick.SetActive(true);
            InfoCanvas.SetActive(false);
        }
        else
        {
            // Start the timer for the current moral dilemma
            MoralDilemmaData moralDilemmaData = SceneController.GetCurrentMoralDilemmaData();
            moralDilemmaData.Timestamps.StartTimer();
        }
        // Show help panel button if game over
        if (SceneController.IsGameOver)
        {
            HelpPanelButton.SetActive(true);
        }
    }

    // Method called when option one button is clicked
    public void ClickButtonOne()
    {
        Joystick.SetActive(true);
        TouchScreen.SetActive(true);
        InfoCanvas.SetActive(false);
        StartCoroutine(WaitToFindWatch());
    }

    // Method called when option two button is clicked
    public void ClickButtonTwo()
    {
        // Stop timer for current moral dilemma
        MoralDilemmaData moralDilemmaData = SceneController.GetCurrentMoralDilemmaData();
        moralDilemmaData.Timestamps.StopTimer();

        Joystick.SetActive(true);
        TouchScreen.SetActive(true);
        InfoCanvas.SetActive(false);
        Watch.SetActive(false);

        // Resolve the dilemma with the ignored option value
        SceneController.ResolveDillema(IgnoreOptionDilemaValue);

        // Update arrow visibility after resolving the dilemma
        ArrowManager arrowManager = GameObject.Find(Constants.ArrowManagerComponent).GetComponent<ArrowManager>();
        arrowManager.UpdateArrowVisibility();
    }

    // Method called when the dilemma is resolved
    public void DilemaResolved()
    {
        // Stop timer for current moral dilemma
        SceneController.GetCurrentMoralDilemmaData().Timestamps.StopTimer();
        // Save game data
        SceneController.SaveGameData();
        // Update UI closing message
        UIManager.UpdateClosingMessage();
        // Deactivate option buttons and activate close message button
        Option1Button.SetActive(false);
        Option2Button.SetActive(false);
        CloseMessageButton.SetActive(true);
    }

    // Method to hide the dilemma message panel
    public void HidePanel()
    {
        // If the dilemma end time is not recorded, stop timer and save game data
        if (SceneController.GetCurrentMoralDilemmaData().Timestamps.EndTime == 0)
        {
            SceneController.GetCurrentMoralDilemmaData().Timestamps.StopTimer();
            SceneController.SaveGameData();
        }
        Watch.SetActive(false);
        TouchScreen.SetActive(true);
        Joystick.SetActive(true);
        DilemaMessagePanel.SetActive(false);
    }

    // Method to show the help panel
    public void ShowHelpPanel()
    {
        // Deactivate joystick and touchscreen, fill help message, and activate help panel
        Joystick.SetActive(false);
        TouchScreen.SetActive(false);
        UIManager.FillHelpMessage(CoinManager.Instance.CountMissingCoinsFromScene());
        HelpPanel.SetActive(true);
    }

    // Method to show the help panel
    public void ShowWatchHelpPanel()
    {
        // Deactivate joystick and touchscreen, fill help message, and activate help panel
        TouchScreen.GetComponent<FixedTouchField>().TouchDist = new Vector2() { x = 0, y = 0 };
        Joystick.GetComponent<Joystick>().OnPointerUp(null);
        Joystick.SetActive(false);
        TouchScreen.SetActive(false);
        UIManager.FillInfoPanelWIthWatchMessage();
        HelpPanel.SetActive(true);
    }

    // Method to close the help panel
    public void CloseHelpPanel()
    {
        HelpPanel.SetActive(false);
        Joystick.SetActive(true);
        TouchScreen.SetActive(true);
        Joystick.GetComponent<FixedJoystick>().DeadZone = 0;
    }

    private IEnumerator WaitToFindWatch()
    {
        yield return new WaitForSeconds(DelayInSeconds);

        // Check if the watch was found
        if (!WatchFound)
        {
            this.ShowWatchHelpPanel();
        }
    }
}