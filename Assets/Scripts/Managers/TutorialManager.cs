using Unity.VisualScripting;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialMessagePanel;
    public GameObject[] messagePopUps;
    private int messageIndex = 0;
    private static int messagesBeforeCatchingFirstCoin = 3;
    private bool firstCoinCollected = false;
    private bool allCoinsCollected = false;

    void Start()
    {
        SceneController sceneController = FindObjectOfType<SceneController>();
        if (sceneController.IsSceneDilemaCompleted())
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < messagesBeforeCatchingFirstCoin; i++)
        {
            if (i == messageIndex)
            {
                tutorialMessagePanel.SetActive(true);
                messagePopUps[i].SetActive(true);
            }
            else
            {
                messagePopUps[i].SetActive(false);
            }
        }
        if (messageIndex >= messagesBeforeCatchingFirstCoin)
        {
            tutorialMessagePanel.SetActive(false);
            CoinManager coinManager = FindObjectOfType<CoinManager>();
            if (coinManager.IsAnyCoinFromSceneCollected())
            {
                if (!firstCoinCollected)
                {
                    messagePopUps[messagesBeforeCatchingFirstCoin].SetActive(true);
                    tutorialMessagePanel.SetActive(true);
                    if (messageIndex == 4)
                    {
                        messagePopUps[messagesBeforeCatchingFirstCoin].SetActive(false);
                        tutorialMessagePanel.SetActive(false);
                        firstCoinCollected = true;
                    }
                }
            }
            if (coinManager.AreAllCoinsFromSceneCollected())
            {
                if (!allCoinsCollected)
                {
                    messagePopUps[messagesBeforeCatchingFirstCoin + 1].SetActive(true);
                    tutorialMessagePanel.SetActive(true);
                    if (messageIndex == 5)
                    {
                        gameObject.SetActive(false);
                        FindObjectOfType<SceneController>().ResolveDillema(1);
                        FindObjectOfType<ArrowManager>().UpdateArrowVisibility();
                    }
                }
            }
        }
    }

    public void IncreaseIndex()
    {
        messageIndex++;
    }
}
