using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Controls the state and data of moral dilemmas and scenes
[System.Serializable]
public class SceneController : MonoBehaviour
{
    // Array to store the state of moral dilemmas for each scene
    public MoralDilemmaData[] MoralDilemmaStates;

    // Current morality value
    public double MoralityValue;

    // Total coins collected
    public int TotalCoins;

    // List of MoralityValuesPerScene objects
    [SerializeField]
    public List<MoralityValuesPerScene> MoralityValuesPerSceneList;

    public TimeMeasurement GameTime;

    public bool IsGameOver;

    // Reference to the text displaying the amount of coins
    private TextMeshProUGUI CoinAmount;


    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Initialize game data when the scene loads
        InitializeGameData();
    }

    // Initialize game data
    void InitializeGameData()
    {
        // Initialize moral dilemma states for each scene
        MoralDilemmaStates = new MoralDilemmaData[SceneManager.sceneCountInBuildSettings];
        for (int i = 0; i < MoralDilemmaStates.Length; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);
            MoralDilemmaStates[i] = new MoralDilemmaData(false, -1, sceneName, 0, "", 0, new TimeMeasurement()); // Not completed, no answer chosen
        }
        // Find the text displaying the amount of coins
        GameObject CoinAmountObject = GameObject.FindGameObjectsWithTag(Constants.CoinAmountTag).FirstOrDefault();
        if (CoinAmountObject)
        {
            CoinAmount = CoinAmountObject.GetComponent<TextMeshProUGUI>();
            if (CoinAmount.text == "0")
            {
                CoinAmount.text = GetStringForCoinAmount(0);
            }
        }
        if (GameTime == null)
        {
            GameTime = new TimeMeasurement();
            GameTime.StartTimer();
        }
        LoadGameData();
    }

    // Save game data
    public void SaveGameData()
    {
        // Convert the game data to JSON and save it using PlayerPrefs
        string json = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(Constants.GameDataPlayerPrefs, json);
        PlayerPrefs.Save();
    }

    // Load game data
    public void LoadGameData()
    {
        // Load the JSON data and convert it back to fill coin amount text
        if (PlayerPrefs.HasKey(Constants.GameDataPlayerPrefs))
        {
            string json = PlayerPrefs.GetString(Constants.GameDataPlayerPrefs);
            JsonUtility.FromJsonOverwrite(json, this);
            if (CoinAmount)
            {
                CoinAmount.text = GetStringForCoinAmount(TotalCoins);
            }
        }
    }

    // Resolve a dilemma by updating its state and the morality value
    public void ResolveDillema(int chosenValue)
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Update the chosen answer index in moral dilemma data for the current scene
        MoralDilemmaStates[currentSceneIndex].MoralDilemmaChosenOption = chosenValue;
        MoralDilemmaStates[currentSceneIndex].Completed = true;
        MoralDilemmaStates[currentSceneIndex].DecisionValue = MoralityValuesPerSceneList[currentSceneIndex - 1].MoralityValues[chosenValue - 1];
        MoralDilemmaStates[currentSceneIndex].MoralDilemmaChosenOptionDescription = MoralityValuesPerSceneList[currentSceneIndex - 1].MoralityDescriptions[chosenValue - 1];
        // Update morality value based on chosen dilemma option
        MoralityValue += MoralityValuesPerSceneList[currentSceneIndex - 1].MoralityValues[chosenValue - 1]; // Assuming moralityValues array holds values for each option
        // Save game data
        SaveGameData();
    }

    // Update the total coins collected
    public void CoinCollected(int coinAmountToAdd)
    {
        // Increment total coins collected
        TotalCoins += coinAmountToAdd;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        MoralDilemmaStates[currentSceneIndex].CoinsCollected = MoralDilemmaStates[currentSceneIndex].CoinsCollected + 1;
        // Update the displayed coin amount
        CoinAmount.text = GetStringForCoinAmount(TotalCoins);
        // Trigger coin counter animation
        CoinCounterAnimation coinCounterAnimation = GameObject.Find(Constants.CoinAnimationComponent).GetComponent<CoinCounterAnimation>();
        coinCounterAnimation.PlayAnimation(coinAmountToAdd, true);
        // Save game data
        SaveGameData();
        if (TotalCoins >= Constants.CoinObjective)
        {
            SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
        }
    }

    // Update the total coins collected
    public void CoinGiven(int coinAmountToSubtract)
    {
        // Increment total coins collected
        TotalCoins -= coinAmountToSubtract;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        MoralDilemmaStates[currentSceneIndex].CoinsCollected = MoralDilemmaStates[currentSceneIndex].CoinsCollected + 1;
        // Update the displayed coin amount
        CoinAmount.text = GetStringForCoinAmount(TotalCoins);
        // Trigger coin counter animation
        CoinCounterAnimation coinCounterAnimation = GameObject.Find(Constants.CoinAnimationComponent).GetComponent<CoinCounterAnimation>();
        coinCounterAnimation.PlayAnimation(coinAmountToSubtract, false);
        // Save game data
        SaveGameData();
        if (TotalCoins >= Constants.CoinObjective)
        {
            SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
        }
    }

    // Check if the moral dilemma in the current scene is completed
    public bool IsSceneDilemaCompleted()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        return MoralDilemmaStates[currentSceneIndex].Completed;
    }

    // Check if the moral dilemma in the current scene is resolved with a specific option
    public bool IsSceneResolvedWithValue(int optionSelected)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int chosenOption = MoralDilemmaStates[currentSceneIndex].MoralDilemmaChosenOption;
        return optionSelected == chosenOption;
    }

    // Check if the moral dilemma in the next scene is completed
    public bool IsNextSceneDilemaCompleted()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        return MoralDilemmaStates[currentSceneIndex + 1].Completed;
    }

    // Get the resolution value of the moral dilemma in the next scene
    public int NextSceneResolvedWithValue()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int chosenOption = MoralDilemmaStates[currentSceneIndex + 1].MoralDilemmaChosenOption;
        return chosenOption;
    }

    // Get the resolution value of the moral dilemma in the previous scene
    public int PreviousSceneResolvedWithValue()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int chosenOption = MoralDilemmaStates[currentSceneIndex - 1].MoralDilemmaChosenOption;
        return chosenOption;
    }

    // Get the current moral dilemma data for the active scene
    public MoralDilemmaData GetCurrentMoralDilemmaData()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        return MoralDilemmaStates[currentSceneIndex];
    }

    // Convert the captured coins amount to a formatted string
    private string GetStringForCoinAmount(int capturedCoins)
    {
        return capturedCoins.ToString() + "/" + Constants.CoinObjective;
    }

}