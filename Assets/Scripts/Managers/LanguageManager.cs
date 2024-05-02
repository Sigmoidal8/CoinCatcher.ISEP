using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the Languages
/// </summary>
public class LanguageManager : MonoBehaviour
{
    // Singleton instance
    public static LanguageManager Instance { get; private set; }

    // Current language selected
    public Language CurrentLanguage;

    // Localization files for the languages
    [Header("Languages")]
    public TextAsset EnglishLocalizationFile;
    public TextAsset PortugueseLocalizationFile;

    // Dictionary to store localized text
    private Dictionary<string, string> LocalizedText;

    // Start is called before the first frame update
    void Start()
    {
        // Load localized text on start
        LoadLocalizedText();
    }

    private void Awake()
    {
        // Ensure only one instance of LanguageManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to set the current language
    public void SetLanguage(Language language)
    {
        CurrentLanguage = language;
        LoadLocalizedText();
    }

    // Method to get localized text by key
    public string GetLocalizedText(string key)
    {
        if (LocalizedText.ContainsKey(key))
            return LocalizedText[key];
        else
            return "KEY_NOT_FOUND";
    }

    // Method to load localized text from JSON files
    private void LoadLocalizedText()
    {
        // Initialize the dictionary
        LocalizedText = new Dictionary<string, string>();

        string json;

        // Load JSON based on current language
        switch (CurrentLanguage)
        {
            case Language.English:
                json = EnglishLocalizationFile.text;
                break;
            case Language.Portuguese:
                json = PortugueseLocalizationFile.text;
                break;
            default:
                json = EnglishLocalizationFile.text;
                break;
        }

        // Deserialize JSON into LocalizationData object
        LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(json);

        // Check if loadedData is null
        if (loadedData == null)
        {
            Debug.LogError("Failed to load localization data.");
            return;
        }

        // Debug statement to inspect the items array
        if (loadedData.items == null)
        {
            Debug.LogError("Localization items array is null.");
            return;
        }

        // Add every item found
        foreach (LocalizationItem item in loadedData.items)
        {
            LocalizedText.Add(item.key, item.value);
        }

        // After the loop, check if items were loaded successfully
        if (LocalizedText.Count == 0)
        {
            Debug.LogWarning("No localization items loaded.");
        }
    }
}