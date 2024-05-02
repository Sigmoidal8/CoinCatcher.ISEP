using UnityEngine;

/// <summary>
/// Manages the Tutorial Scene
/// </summary>
public class TutorialManager : MonoBehaviour
{
    public GameObject TutorialMessagePanel;
    public GameObject[] MessagePopUps;
    // Index to track the current message being displayed
    private int MessageIndex = 0;
    // Number of messages before catching the first coin
    private static int MessagesBeforeCatchingFirstCoin = 3;
    // Flag indicating if the first coin has been collected
    private bool FirstCoinCollected = false;
    // Flag indicating if all coins have been collected
    private bool AllCoinsCollected = false;

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
        // Iterate through the messages before catching the first coin
        for (int i = 0; i < MessagesBeforeCatchingFirstCoin; i++)
        {
            if (i == MessageIndex)
            {
                // Display the current message pop-up
                TutorialMessagePanel.SetActive(true);
                MessagePopUps[i].SetActive(true);
            }
            else
            {
                // Hide other message pop-ups
                MessagePopUps[i].SetActive(false);
            }
        }
        // Check if all messages before catching the first coin have been displayed
        if (MessageIndex >= MessagesBeforeCatchingFirstCoin)
        {
            // Hide the tutorial message panel
            TutorialMessagePanel.SetActive(false);
            CoinManager coinManager = FindObjectOfType<CoinManager>();
            // Check if any coin from the scene is collected
            if (coinManager.IsAnyCoinFromSceneCollected())
            {
                if (!FirstCoinCollected)
                {
                    // Display the message pop-up for catching the first coin
                    MessagePopUps[MessagesBeforeCatchingFirstCoin].SetActive(true);
                    TutorialMessagePanel.SetActive(true);
                    // Check if the last message before catching the first coin is displayed
                    if (MessageIndex == 4)
                    {
                        MessagePopUps[MessagesBeforeCatchingFirstCoin].SetActive(false);
                        TutorialMessagePanel.SetActive(false);
                        FirstCoinCollected = true;
                    }
                }
            }
            // Check if all coins from the scene are collected
            if (coinManager.AreAllCoinsFromSceneCollected())
            {
                if (!AllCoinsCollected)
                {
                    // Display the message pop-up for collecting all coins
                    MessagePopUps[MessagesBeforeCatchingFirstCoin + 1].SetActive(true);
                    TutorialMessagePanel.SetActive(true);

                    // Check if the last message for collecting all coins is displayed
                    if (MessageIndex == 5)
                    {
                        gameObject.SetActive(false);
                        FindObjectOfType<SceneController>().ResolveDillema(1);
                        FindObjectOfType<ArrowManager>().UpdateArrowVisibility();
                    }
                }
            }
        }
    }

    // Method to increase the message index
    public void IncreaseIndex()
    {
        MessageIndex++;
    }
}
