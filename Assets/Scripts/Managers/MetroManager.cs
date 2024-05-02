using UnityEngine;

/// <summary>
/// Manages the Metro scene
/// </summary>
public class MetroManager : MonoBehaviour
{
    [Header("Controls")]
    public GameObject TouchScreen;
    public GameObject Joystick;

    [Header("Panels")]
    public GameObject DilemaMessagePanel;
    public GameObject HelpPanel;

    [Header("Help Panel Button")]
    public GameObject HelpPanelButton;

    [Header("Managers & Controllers")]
    public MetroSceneUIManager UIManager;
    public SceneController SceneController;

    [Header("Homeless Coinst")]
    public GameObject HomelessCoins;

    // Value indicating to remove coins from homeless option
    private static int RemoveCoinsFromHomelessOption = 2;

    // Start is called before the first frame update
    void Start()
    {
        // Check if the dilemma in the scene is completed
        if (SceneController.IsSceneDilemaCompleted())
        {
            gameObject.SetActive(false);
            TouchScreen.SetActive(true);
            Joystick.SetActive(true);
        }
        // Check if the scene is resolved with the option to remove coins from homeless
        if (SceneController.IsSceneResolvedWithValue(RemoveCoinsFromHomelessOption))
        {
            HomelessCoins.SetActive(false);
        }
        // Show help panel button if game over
        if (SceneController.IsGameOver)
        {
            HelpPanelButton.SetActive(true);
        }
    }

    // Method to hide coins collected from homeless
    public void HideCoinsCollectedFromHomeless()
    {
        HomelessCoins.SetActive(false);
    }

    // Method to hide the dilemma message panel
    public void HidePanel()
    {
        // Stop timer for current moral dilemma and save game data
        SceneController.GetCurrentMoralDilemmaData().Timestamps.StopTimer();
        SceneController.SaveGameData();

        TouchScreen.SetActive(true);
        Joystick.SetActive(true);
        DilemaMessagePanel.SetActive(false);
    }

    // Method to show the help panel
    public void ShowHelpPanel()
    {
        Joystick.SetActive(false);
        TouchScreen.SetActive(false);
        UIManager.FillHelpMessage(CoinManager.Instance.CountMissingCoinsFromScene());
        HelpPanel.SetActive(true);
    }

    // Method to close the help panel
    public void CloseHelpPanel()
    {
        HelpPanel.SetActive(false);
        Joystick.SetActive(true);
        TouchScreen.SetActive(true);
    }
}