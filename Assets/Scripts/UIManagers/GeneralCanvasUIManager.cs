using TMPro;
using UnityEngine;

public class GeneralCanvasUIManager : MonoBehaviour
{
    public TextMeshProUGUI QuitButton;
    public TextMeshProUGUI HelpDilemmaMessage;
    public TextMeshProUGUI HelpDilemmaMessageClose;
    public AudioSource AudioSource;
    public AudioClip AudioClip;

    protected LanguageManager languageManager;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Find the LanguageManager in the scene or create one if not found
        languageManager = FindObjectOfType<LanguageManager>();
        if (languageManager == null)
        {
            GameObject languageManagerObject = new GameObject(Constants.LanguageManagerComponent);
            languageManager = languageManagerObject.AddComponent<LanguageManager>();
        }

        // Update UI texts based on the current language
        UpdateUITexts();
    }

    void Update()
    {
        //Plays the background audio of the scene
        if (!AudioSource.isPlaying && AudioClip != null)
        {
            AudioSource.PlayOneShot(AudioClip);
        }
    }

    // Update UI text elements with localized text
    protected virtual void UpdateUITexts()
    {
        QuitButton.text = languageManager.GetLocalizedText(LanguageFields.quit_button.ToString());
    }

    //Fills the help message according to the coins left on the scene
    public void FillHelpMessage(int coinsLeftOnScene)
    {
        if (coinsLeftOnScene == 0)
        {
            HelpDilemmaMessage.text = languageManager.GetLocalizedText(LanguageFields.no_coins_missing_on_scene.ToString());
        }
        else
        {
            HelpDilemmaMessage.text = languageManager.GetLocalizedText(LanguageFields.coins_missing_on_scene.ToString()) + coinsLeftOnScene;
        }
        HelpDilemmaMessageClose.text = languageManager.GetLocalizedText(LanguageFields.close_button.ToString());
    }
}
