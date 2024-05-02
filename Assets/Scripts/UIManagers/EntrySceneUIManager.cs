using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntrySceneUIManager : GeneralCanvasUIManager
{
    public TextMeshProUGUI tutorialMessage1;
    public TextMeshProUGUI tutorialMessage2;
    public TextMeshProUGUI tutorialMessage3;
    public TextMeshProUGUI tutorialMessage4;
    public TextMeshProUGUI tutorialMessage5;
    public TextMeshProUGUI tutorialMessageNext;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // Update UI texts based on the current language
        UpdateAdditionalText();
    }

    // Override the UpdateUITexts method to update additional components
    protected override void UpdateUITexts()
    {
        // Call the UpdateUITexts method of the base class
        base.UpdateUITexts();

        // Update additional text
        UpdateAdditionalText();
    }

    // Update additional UI text element with localized text
    private void UpdateAdditionalText()
    {
        tutorialMessage1.text = languageManager.GetLocalizedText(LanguageFields.tutorial_message1.ToString()) + Constants.CoinObjective + languageManager.GetLocalizedText(LanguageFields.tutorial_message1_1.ToString());
        tutorialMessage2.text = languageManager.GetLocalizedText(LanguageFields.tutorial_message2.ToString());
        tutorialMessage3.text = languageManager.GetLocalizedText(LanguageFields.tutorial_message3.ToString());
        tutorialMessage4.text = languageManager.GetLocalizedText(LanguageFields.tutorial_message4.ToString());
        tutorialMessage5.text = languageManager.GetLocalizedText(LanguageFields.tutorial_message5.ToString());
        tutorialMessageNext.text = languageManager.GetLocalizedText(LanguageFields.next_button.ToString());
    }
}

