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
    public MoralDilemmaData[] moralDilemmaStates;

    // Current morality value
    public double moralityValue;

    // Total coins collected
    public int totalCoins;

    // List of MoralityValuesPerScene objects
    [SerializeField]
    public List<MoralityValuesPerScene> moralityValuesPerSceneList;

    public TimeMeasurement gameTime;

    public bool isGameOver;

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
        moralDilemmaStates = new MoralDilemmaData[SceneManager.sceneCountInBuildSettings];
        for (int i = 0; i < moralDilemmaStates.Length; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);
            moralDilemmaStates[i] = new MoralDilemmaData(false, -1, sceneName, 0, "", 0, new TimeMeasurement()); // Not completed, no answer chosen
        }
        // Find the text displaying the amount of coins
        GameObject CoinAmountObject = GameObject.FindGameObjectsWithTag(Constants.CoinAmountTag).FirstOrDefault();
        if (CoinAmountObject)
        {
            CoinAmount = CoinAmountObject.GetComponent<TextMeshProUGUI>();
            if(CoinAmount.text == "0"){
                CoinAmount.text = GetStringForCoinAmount(0);
            }
        }
        if (gameTime == null)
        {
            gameTime = new TimeMeasurement();
            gameTime.StartTimer();
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
                CoinAmount.text = GetStringForCoinAmount(totalCoins);
            }
        }
    }

    // Resolve a dilemma by updating its state and the morality value
    public void ResolveDillema(int chosenValue)
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Update the chosen answer index in moral dilemma data for the current scene
        moralDilemmaStates[currentSceneIndex].moralDilemaChosenOption = chosenValue;
        moralDilemmaStates[currentSceneIndex].completed = true;
        moralDilemmaStates[currentSceneIndex].decisionValue = moralityValuesPerSceneList[currentSceneIndex - 1].moralityValues[chosenValue - 1];
        moralDilemmaStates[currentSceneIndex].moralDilemaChosenOptionDescription = moralityValuesPerSceneList[currentSceneIndex - 1].moralityDescriptions[chosenValue - 1];
        // Update morality value based on chosen dilemma option
        moralityValue += moralityValuesPerSceneList[currentSceneIndex - 1].moralityValues[chosenValue - 1]; // Assuming moralityValues array holds values for each option
        // Save game data
        SaveGameData();
    }

    // Update the total coins collected
    public void CoinCollected(int coinAmountToAdd)
    {
        // Increment total coins collected
        totalCoins += coinAmountToAdd;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        moralDilemmaStates[currentSceneIndex].coinsCollected = moralDilemmaStates[currentSceneIndex].coinsCollected + 1;
        // Update the displayed coin amount
        CoinAmount.text = GetStringForCoinAmount(totalCoins);
        // Trigger coin counter animation
        CoinCounterAnimation coinCounterAnimation = GameObject.Find(Constants.CoinAnimationComponent).GetComponent<CoinCounterAnimation>();
        coinCounterAnimation.PlayAnimation(coinAmountToAdd, true);
        // Save game data
        SaveGameData();
        if (totalCoins >= Constants.CoinObjective)
        {
            SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
        }
    }

    // Update the total coins collected
    public void CoinGiven(int coinAmountToSubtract)
    {
        // Increment total coins collected
        totalCoins -= coinAmountToSubtract;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        moralDilemmaStates[currentSceneIndex].coinsCollected = moralDilemmaStates[currentSceneIndex].coinsCollected + 1;
        // Update the displayed coin amount
        CoinAmount.text = GetStringForCoinAmount(totalCoins);
        // Trigger coin counter animation
        CoinCounterAnimation coinCounterAnimation = GameObject.Find(Constants.CoinAnimationComponent).GetComponent<CoinCounterAnimation>();
        coinCounterAnimation.PlayAnimation(coinAmountToSubtract, false);
        // Save game data
        SaveGameData();
        if (totalCoins >= Constants.CoinObjective)
        {
            SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
        }
    }

    public bool IsSceneDilemaCompleted()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        return moralDilemmaStates[currentSceneIndex].completed;
    }

    public bool IsSceneResolvedWithValue(int optionSelected)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int chosenOption = moralDilemmaStates[currentSceneIndex].moralDilemaChosenOption;
        return optionSelected == chosenOption;
    }

    public bool IsNextSceneDilemaCompleted()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        return moralDilemmaStates[currentSceneIndex + 1].completed;
    }

    public int NextSceneResolvedWithValue()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int chosenOption = moralDilemmaStates[currentSceneIndex + 1].moralDilemaChosenOption;
        return chosenOption;
    }

    public int PreviousSceneResolvedWithValue()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int chosenOption = moralDilemmaStates[currentSceneIndex - 1].moralDilemaChosenOption;
        return chosenOption;
    }

    public MoralDilemmaData GetCurrentMoralDilemmaData()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        return moralDilemmaStates[currentSceneIndex];
    }

    private string GetStringForCoinAmount(int capturedCoins)
    {
        return capturedCoins.ToString() + "/" + Constants.CoinObjective;
    }

}