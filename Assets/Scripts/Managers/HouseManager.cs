using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    public GameObject TouchScreen;
    public GameObject Joystick;
    public GameObject InfoCanvas;
    public GameObject DilemaMessagePanel;
    public GameObject Watch;
    public GameObject Option1Button;
    public GameObject Option2Button;
    public GameObject CloseMessageButton;
    public GameObject HelpPanelButton;
    public GameObject HelpPanel;
    public HouseSceneUIManager UIManager;
    public SceneController sceneController;
    private static int IgnoreOptionDilemaValue = 2;

    void Start()
    {
        if (sceneController.IsSceneDilemaCompleted())
        {
            gameObject.SetActive(false);
            Watch.SetActive(false);
            TouchScreen.SetActive(true);
            Joystick.SetActive(true);
            InfoCanvas.SetActive(false);
        }
        else
        {
            MoralDilemmaData moralDilemmaData = sceneController.GetCurrentMoralDilemmaData();
            moralDilemmaData.timestamps.StartTimer();
        }
        if(sceneController.isGameOver){
            HelpPanelButton.SetActive(true);
        }
    }

    public void ClickButtonOne()
    {
        Joystick.SetActive(true);
        TouchScreen.SetActive(true);
        InfoCanvas.SetActive(false);
    }

    public void ClickButtonTwo()
    {
        MoralDilemmaData moralDilemmaData = sceneController.GetCurrentMoralDilemmaData();
        moralDilemmaData.timestamps.StopTimer();
        Joystick.SetActive(true);
        TouchScreen.SetActive(true);
        InfoCanvas.SetActive(false);
        Watch.SetActive(false);

        sceneController.ResolveDillema(IgnoreOptionDilemaValue);

        ArrowManager arrowManager = GameObject.Find(Constants.ArrowManagerComponent).GetComponent<ArrowManager>();
        arrowManager.UpdateArrowVisibility();
    }

    public void DilemaResolved()
    {
        sceneController.GetCurrentMoralDilemmaData().timestamps.StopTimer();
        sceneController.SaveGameData();
        UIManager.UpdateClosingMessage();
        Option1Button.SetActive(false);
        Option2Button.SetActive(false);
        CloseMessageButton.SetActive(true);
    }

    public void HidePanel()
    {
        if (sceneController.GetCurrentMoralDilemmaData().timestamps.endTime == 0)
        {
            sceneController.GetCurrentMoralDilemmaData().timestamps.StopTimer();
            sceneController.SaveGameData();
        }
        Watch.SetActive(false);
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