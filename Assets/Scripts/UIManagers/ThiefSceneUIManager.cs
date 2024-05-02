using TMPro;

public class ThiefSceneUIManager : GeneralCanvasUIManager
{
    public TextMeshProUGUI ThiefDilemaMessage;
    public TextMeshProUGUI ThiefDilemaMessage2;
    public TextMeshProUGUI ThiefDilemaMessageButton1;
    public TextMeshProUGUI ThiefDilemaMessageButton2;
    public TextMeshProUGUI ThiefDilemaMessageButton3;
    public TextMeshProUGUI ThiefDilemaMessageButton4;
    public TextMeshProUGUI ThiefDilemaMessageNextButton;
    public TextMeshProUGUI InfoPanelMessage;
    public TextMeshProUGUI InfoPanelMessageCloseButton;

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
        ThiefDilemaMessage.text = languageManager.GetLocalizedText(LanguageFields.thief_dilemma_message.ToString());
        ThiefDilemaMessage2.text = languageManager.GetLocalizedText(LanguageFields.thief_dilemma_message2.ToString());
        ThiefDilemaMessageButton1.text = languageManager.GetLocalizedText(LanguageFields.thief_dilemma_message_button1.ToString());
        ThiefDilemaMessageButton2.text = languageManager.GetLocalizedText(LanguageFields.thief_dilemma_message_button2.ToString());
        ThiefDilemaMessageButton3.text = languageManager.GetLocalizedText(LanguageFields.thief_dilemma_message_button3.ToString());
        ThiefDilemaMessageButton4.text = languageManager.GetLocalizedText(LanguageFields.thief_dilemma_message_button4.ToString());
        ThiefDilemaMessageNextButton.text = languageManager.GetLocalizedText(LanguageFields.next_button.ToString());
        InfoPanelMessage.text = languageManager.GetLocalizedText(LanguageFields.info_panel_message1.ToString()) + Constants.CoinObjective +languageManager.GetLocalizedText(LanguageFields.info_panel_message2.ToString());
        InfoPanelMessageCloseButton.text = languageManager.GetLocalizedText(LanguageFields.close_button.ToString());
    }
}
