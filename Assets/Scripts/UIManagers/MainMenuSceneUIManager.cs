using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSceneUIManager : MonoBehaviour
{
    public TextMeshProUGUI welcomeText;
    public TextMeshProUGUI languageText;
    public TextMeshProUGUI playButton;
    public TextMeshProUGUI settingsButton;
    public TextMeshProUGUI quitButton;
    public TextMeshProUGUI closeButton;
    public TextMeshProUGUI startButton;
    public TextMeshProUGUI alertMessage;
    public TMP_Dropdown languagePicker;

    private LanguageManager languageManager;

    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        // Find the LanguageManager in the scene or create one if not found
        languageManager = FindObjectOfType<LanguageManager>();
        if (languageManager == null)
        {
            GameObject languageManagerObject = new GameObject(Constants.LanguageManagerComponent);
            languageManager = languageManagerObject.AddComponent<LanguageManager>();
        }

        string[] enumNames = Enum.GetNames(typeof(Language));
        List<string> names = new List<string>(enumNames);
        languagePicker.AddOptions(names);

        languagePicker.onValueChanged.AddListener(OnLanguagePickerValueChanged);

        // Update UI texts based on the current language
        UpdateUITexts();
    }

    // Called when the language dropdown value changes
    private void OnLanguagePickerValueChanged(int value)
    {
        // Determine the selected language based on the dropdown value
        Language selectedLanguage = (Language)value;

        // Call OnLanguageChanged method to change the language
        OnLanguageChanged(selectedLanguage);
    }

    // Update UI text elements with localized text
    private void UpdateUITexts()
    {
        welcomeText.text = languageManager.GetLocalizedText(LanguageFields.welcome_message.ToString());
        languageText.text = languageManager.GetLocalizedText(LanguageFields.language.ToString());
        playButton.text = languageManager.GetLocalizedText(LanguageFields.play_button.ToString());
        settingsButton.text = languageManager.GetLocalizedText(LanguageFields.settings_button.ToString());
        quitButton.text = languageManager.GetLocalizedText(LanguageFields.quit_button.ToString());
        closeButton.text = languageManager.GetLocalizedText(LanguageFields.close_button.ToString());
        startButton.text = languageManager.GetLocalizedText(LanguageFields.play_button.ToString());
        alertMessage.text = languageManager.GetLocalizedText(LanguageFields.alert.ToString());
    }

    // Called when the language is changed
    public void OnLanguageChanged(Language newLanguage)
    {
        // Update the LanguageManager with the new language
        languageManager.SetLanguage(newLanguage);

        // Update UI texts with the new language
        UpdateUITexts();
    }
}
