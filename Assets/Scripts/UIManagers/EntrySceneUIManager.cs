using TMPro;

/// <summary>
/// Manages the UI elements in the tutorial scene
/// </summary>
public class EntrySceneUIManager : GeneralCanvasUIManager
{
    public TextMeshProUGUI TutorialMessage1;
    public TextMeshProUGUI TutorialMessage2;
    public TextMeshProUGUI TutorialMessage3;
    public TextMeshProUGUI TutorialMessage4;
    public TextMeshProUGUI TutorialMessage5;
    public TextMeshProUGUI TutorialMessageNext;

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
        TutorialMessage1.text = languageManager.GetLocalizedText(LanguageFields.tutorial_message1.ToString()) + Constants.CoinObjective + languageManager.GetLocalizedText(LanguageFields.tutorial_message1_1.ToString());
        TutorialMessage2.text = languageManager.GetLocalizedText(LanguageFields.tutorial_message2.ToString());
        TutorialMessage3.text = languageManager.GetLocalizedText(LanguageFields.tutorial_message3.ToString());
        TutorialMessage4.text = languageManager.GetLocalizedText(LanguageFields.tutorial_message4.ToString());
        TutorialMessage5.text = languageManager.GetLocalizedText(LanguageFields.tutorial_message5.ToString());
        TutorialMessageNext.text = languageManager.GetLocalizedText(LanguageFields.next_button.ToString());
    }
}

