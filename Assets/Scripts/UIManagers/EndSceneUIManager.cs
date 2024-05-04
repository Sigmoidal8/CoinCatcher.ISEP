using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages the UI elements in the end scene, such as displaying end text, morality result, and quitting button.
/// </summary>
public class EndSceneUIManager : MonoBehaviour
{
    public TextMeshProUGUI EndText;
    public TextMeshProUGUI QuitButton;
    public TextMeshProUGUI MoralityResult;
    private LanguageManager LanguageManager;
    private double MoralityValueNormalized;
    private TimeMeasurement Gametime;

    // Start is called before the first frame update
    void Start()
    {
        // Find the LanguageManager in the scene or create one if not found
        LanguageManager = FindObjectOfType<LanguageManager>();
        if (LanguageManager == null)
        {
            GameObject languageManagerObject = new GameObject(Constants.LanguageManagerComponent);
            LanguageManager = languageManagerObject.AddComponent<LanguageManager>();
        }

        SceneController sceneController = GameObject.Find(Constants.SceneControllerComponent).GetComponent<SceneController>();
        sceneController.GameTime.StopTimer();
        sceneController.GameTime.MeasureFinalTime();
        Gametime = sceneController.GameTime;

        string playerName = PlayerPrefs.GetString(Constants.PlayerFabsPlayerName);
        PlayerData playerData = new PlayerData(playerName);

        // Read the existing JSON file
        string existingJsonFilePathPlayer = Path.Combine(Application.persistentDataPath, $"userfiles.json");
        string existingJsonContent = File.ReadAllText(existingJsonFilePathPlayer);

        // Deserialize JSON content into a UserData object
        FinalDataList existingUserDataList = FinalJsonSerializer.DeserializeFinalDataList(existingJsonContent);

        GameData gameData = new GameData(sceneController.MoralDilemmaStates, Gametime);
        double moralityValue = sceneController.MoralityValue / (sceneController.MoralDilemmaStates.Where(states => states.Completed == true).Count() - 1);
        MoralityValueNormalized = (moralityValue - 1.66) / 3.09;
        List<Trait> traits = new List<Trait>
            {
                new Trait("Morality", MoralityValueNormalized, moralityValue)
            };
        TraitData traitData = new TraitData(traits);
        FinalData dataToBeStored = new FinalData(playerData, gameData, traitData);
        existingUserDataList.userData.Add(dataToBeStored);
        string dataInString = FinalJsonSerializer.SerializeDataList(existingUserDataList);
        Debug.Log(dataInString);

        File.WriteAllText(existingJsonFilePathPlayer, dataInString);

        string newJsonFileNamePlayer = $"{playerData.Username}_userfile.json";
        string newJsonFilePathPlayer = Path.Combine(Application.persistentDataPath, newJsonFileNamePlayer);
        string moralityDescription = "";
        switch (MoralityValueNormalized)
        {
            case <= 0.35f:
                moralityDescription = LanguageManager.GetLocalizedText(LanguageFields.moral_result_1.ToString());
                break;
            case < 0.70f:
                moralityDescription = LanguageManager.GetLocalizedText(LanguageFields.moral_result_2.ToString());
                break;
            case >= 0.70f:
                moralityDescription = LanguageManager.GetLocalizedText(LanguageFields.moral_result_3.ToString());
                break;
        }
        TimeSpan timeSpan = TimeSpan.FromSeconds(Gametime.TimeTaken);
        UserData userData = new UserData(string.Format("{0:00}:{1:00}", (int)timeSpan.TotalMinutes, timeSpan.Seconds), moralityDescription, traitData);
        dataInString = JsonUtility.ToJson(userData);
        Debug.Log(dataInString);

        File.WriteAllText(newJsonFilePathPlayer, dataInString);

        // Update UI texts based on the current language
        UpdateUITexts(timeSpan);
    }

    // Update UI text elements with localized text
    private void UpdateUITexts(TimeSpan timeSpan)
    {
        EndText.text = LanguageManager.GetLocalizedText(LanguageFields.end_text.ToString());
        QuitButton.text = LanguageManager.GetLocalizedText(LanguageFields.quit_button.ToString());
        MoralityResult.text = LanguageManager.GetLocalizedText(LanguageFields.morality_result.ToString()) + string.Format("{0:00}:{1:00}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
    }
}
