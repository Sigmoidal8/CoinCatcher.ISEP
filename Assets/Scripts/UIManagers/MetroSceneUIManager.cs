using TMPro;
using UnityEngine;

public class MetroSceneUIManager : GeneralCanvasUIManager
{
    public TextMeshProUGUI HomelessDilemaMessage;
    public TextMeshProUGUI HomelessDilemaMessageButton1;
    public TextMeshProUGUI HomelessDilemaMessageButton2;
    public TextMeshProUGUI HomelessDilemaMessageButton3;

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
        HomelessDilemaMessage.text = languageManager.GetLocalizedText(LanguageFields.homeless_dilema_message.ToString());
        HomelessDilemaMessageButton1.text = languageManager.GetLocalizedText(LanguageFields.homeless_dilema_message_button1.ToString());
        HomelessDilemaMessageButton2.text = languageManager.GetLocalizedText(LanguageFields.homeless_dilema_message_button2.ToString());
        HomelessDilemaMessageButton3.text = languageManager.GetLocalizedText(LanguageFields.homeless_dilema_message_button3.ToString());
    }
}
