using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuSceneUIManager : MonoBehaviour
{
    public TextMeshProUGUI WelcomeText;
    public TextMeshProUGUI LanguageText;
    public TextMeshProUGUI PlayButton;
    public TextMeshProUGUI SettingsButton;
    public TextMeshProUGUI QuitButton;
    public TextMeshProUGUI CloseButton;
    public TextMeshProUGUI StartButton;
    public TextMeshProUGUI CloseUsernameButton;
    public TextMeshProUGUI UsernameInput;
    public TextMeshProUGUI AlertMessage;
    public TMP_Dropdown LanguagePicker;
    private LanguageManager LanguageManager;

    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        // Find the LanguageManager in the scene or create one if not found
        LanguageManager = FindObjectOfType<LanguageManager>();
        if (LanguageManager == null)
        {
            GameObject languageManagerObject = new GameObject(Constants.LanguageManagerComponent);
            LanguageManager = languageManagerObject.AddComponent<LanguageManager>();
        }

        string[] enumNames = Enum.GetNames(typeof(Language));
        List<string> names = new List<string>(enumNames);
        LanguagePicker.AddOptions(names);

        LanguagePicker.onValueChanged.AddListener(OnLanguagePickerValueChanged);

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
        WelcomeText.text = LanguageManager.GetLocalizedText(LanguageFields.welcome_message.ToString());
        LanguageText.text = LanguageManager.GetLocalizedText(LanguageFields.language.ToString());
        PlayButton.text = LanguageManager.GetLocalizedText(LanguageFields.play_button.ToString());
        SettingsButton.text = LanguageManager.GetLocalizedText(LanguageFields.settings_button.ToString());
        QuitButton.text = LanguageManager.GetLocalizedText(LanguageFields.quit_button.ToString());
        CloseButton.text = LanguageManager.GetLocalizedText(LanguageFields.close_button.ToString());
        StartButton.text = LanguageManager.GetLocalizedText(LanguageFields.play_button.ToString());
        AlertMessage.text = LanguageManager.GetLocalizedText(LanguageFields.alert.ToString());
        CloseUsernameButton.text = LanguageManager.GetLocalizedText(LanguageFields.close_button.ToString());
        UsernameInput.text = LanguageManager.GetLocalizedText(LanguageFields.username_input.ToString());
    }

    // Called when the language is changed
    public void OnLanguageChanged(Language newLanguage)
    {
        // Update the LanguageManager with the new language
        LanguageManager.SetLanguage(newLanguage);

        // Update UI texts with the new language
        UpdateUITexts();
    }
}
