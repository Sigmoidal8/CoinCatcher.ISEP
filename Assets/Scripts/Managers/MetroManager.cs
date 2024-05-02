using Unity.VisualScripting;
using UnityEngine;

public class MetroManager : MonoBehaviour
{
    public GameObject TouchScreen;
    public GameObject Joystick;
    public GameObject DilemaMessagePanel;
    public GameObject HomelessCoins;
    public GameObject HelpPanelButton;
    public GameObject HelpPanel;
    public MetroSceneUIManager UIManager;
    public SceneController sceneController;
    private static int RemoveCoinsFromHomelessOption = 2;

    void Start()
    {
        if (sceneController.IsSceneDilemaCompleted())
        {
            gameObject.SetActive(false);
            TouchScreen.SetActive(true);
            Joystick.SetActive(true);
        }
        if (sceneController.IsSceneResolvedWithValue(RemoveCoinsFromHomelessOption))
        {
            HomelessCoins.SetActive(false);
        }
        if (sceneController.isGameOver)
        {
            HelpPanelButton.SetActive(true);
        }
    }

    public void HideCoinsCollectedFromHomeless()
    {
        HomelessCoins.SetActive(false);
    }

    public void HidePanel()
    {
        sceneController.GetCurrentMoralDilemmaData().timestamps.StopTimer();
        sceneController.SaveGameData();
        TouchScreen.SetActive(true);
        Joystick.SetActive(true);
        DilemaMessagePanel.SetActive(false);
    }

    public void ShowHelpPanel()
    {
        Joystick.SetActive(false);
        TouchScreen.SetActive(false);
        UIManager.FillHelpMessage(CoinManager.instance.CountMissingCoinsFromScene());
        HelpPanel.SetActive(true);
    }

    public void CloseHelpPanel()
    {
        HelpPanel.SetActive(false);
        Joystick.SetActive(true);
        TouchScreen.SetActive(true);
    }
}