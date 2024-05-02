using TMPro;
using UnityEngine;

public class RobberySceneUIManager : GeneralCanvasUIManager
{
    public TextMeshProUGUI RobberyDilemaMessage;
    public TextMeshProUGUI RobberyDilemaMessageButton1;
    public TextMeshProUGUI RobberyDilemaMessageButton2;
    public TextMeshProUGUI RobberyDilemaMessageButton3;

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
        RobberyDilemaMessage.text = languageManager.GetLocalizedText(LanguageFields.robbery_dilema_message.ToString());
        RobberyDilemaMessageButton1.text = languageManager.GetLocalizedText(LanguageFields.robbery_dilema_message_button1.ToString());
        RobberyDilemaMessageButton2.text = languageManager.GetLocalizedText(LanguageFields.robbery_dilema_message_button2.ToString());
        RobberyDilemaMessageButton3.text = languageManager.GetLocalizedText(LanguageFields.robbery_dilema_message_button3.ToString());
    }
}
