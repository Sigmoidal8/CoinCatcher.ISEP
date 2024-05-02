using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class EndSceneUIManager : MonoBehaviour
{
    public TextMeshProUGUI endText;
    public TextMeshProUGUI quitButton;
    public TextMeshProUGUI moralityResult;
    private LanguageManager languageManager;
    private double moralityValue;
    private TimeMeasurement gametime;

    // Start is called before the first frame update
    void Start()
    {
        // Find the LanguageManager in the scene or create one if not found
        languageManager = FindObjectOfType<LanguageManager>();
        if (languageManager == null)
        {
            GameObject languageManagerObject = new GameObject(Constants.LanguageManagerComponent);
            languageManager = languageManagerObject.AddComponent<LanguageManager>();
        }

        SceneController sceneController = GameObject.Find(Constants.SceneControllerComponent).GetComponent<SceneController>();
        sceneController.gameTime.StopTimer();
        gametime = sceneController.gameTime;


        string playerName = PlayerPrefs.GetString(Constants.PlayerFabsPlayerName);
        PlayerData playerData = new PlayerData(playerName);

        // Create new JSON file name
        string newJsonFileName = $"{playerData.username}.json";
        string newJsonFilePath = Path.Combine(Application.persistentDataPath, newJsonFileName);
        GameData gameData = new GameData(sceneController.moralDilemmaStates, gametime);
        moralityValue = ((sceneController.moralityValue / (sceneController.moralDilemmaStates.Where(states => states.completed == true).Count() - 1)) - 1) / 4;
        List<Trait> traits = new List<Trait>
            {
                new Trait("Morality", moralityValue)
            };
        TraitData traitData = new TraitData(traits);
        FinalData dataToBeStored = new FinalData(playerData, gameData, traitData);
        string dataInString = FinalJsonSerializer.SerializeData(dataToBeStored);
        Debug.Log(dataInString);

        File.WriteAllText(newJsonFilePath, dataInString);

        string newJsonFileNamePlayer = $"{playerData.username}_userfile.json";
        string newJsonFilePathPlayer = Path.Combine(Application.persistentDataPath, newJsonFileNamePlayer);
        string moralityDescription = "";
        switch (moralityValue)
        {
            case <= 0.33f:
                moralityDescription = languageManager.GetLocalizedText(LanguageFields.moral_result_1.ToString());
                break;
            case <= 0.66f:
                moralityDescription = languageManager.GetLocalizedText(LanguageFields.moral_result_2.ToString());
                break;
            case > 0.66:
                moralityDescription = languageManager.GetLocalizedText(LanguageFields.moral_result_3.ToString());
                break;
        }
        TimeSpan timeSpan = TimeSpan.FromSeconds(gametime.timeTaken);
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
        endText.text = languageManager.GetLocalizedText(LanguageFields.end_text.ToString());
        quitButton.text = languageManager.GetLocalizedText(LanguageFields.quit_button.ToString());
        moralityResult.text = languageManager.GetLocalizedText(LanguageFields.morality_result.ToString()) + string.Format("{0:00}:{1:00}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
    }
}
