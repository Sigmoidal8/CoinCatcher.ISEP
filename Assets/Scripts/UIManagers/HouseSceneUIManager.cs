using TMPro;
using UnityEngine;

public class HouseSceneUIManager : GeneralCanvasUIManager
{
    [Header("Introduction Messages")]
    public TextMeshProUGUI IntroductionMessage;
    public TextMeshProUGUI IntroductionMessageButton1;
    public TextMeshProUGUI IntroductionMessageButton2;

    [Header("Watch Dilema")]
    public TextMeshProUGUI WatchDilemaMessage;
    public TextMeshProUGUI WatchDilemaMessageButton1;
    public TextMeshProUGUI WatchDilemaMessageButton2;


    [Header("Others")]
    public TextMeshProUGUI WatchDilemaClosingMessageButton;

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
        IntroductionMessage.text = languageManager.GetLocalizedText(LanguageFields.introduction_message.ToString());
        IntroductionMessageButton1.text = languageManager.GetLocalizedText(LanguageFields.introduction_message_button1.ToString());
        IntroductionMessageButton2.text = languageManager.GetLocalizedText(LanguageFields.introduction_message_button2.ToString());
        WatchDilemaMessage.text = languageManager.GetLocalizedText(LanguageFields.watch_dilemma_message.ToString());
        WatchDilemaMessageButton1.text = languageManager.GetLocalizedText(LanguageFields.watch_dilemma_message_button1.ToString());
        WatchDilemaMessageButton2.text = languageManager.GetLocalizedText(LanguageFields.watch_dilemma_message_button2.ToString());
        WatchDilemaClosingMessageButton.text = languageManager.GetLocalizedText(LanguageFields.close_button.ToString());
    }

    public void UpdateClosingMessage()
    {
        WatchDilemaMessage.text = languageManager.GetLocalizedText(LanguageFields.watch_dilemma_closing_message.ToString());
    }
}
