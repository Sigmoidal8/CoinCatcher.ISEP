using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    // Singleton instance
    public static LanguageManager Instance { get; private set; }

    public Language currentLanguage;
    public TextAsset englishLocalizationFile;
    public TextAsset portugueseLocalizationFile;

    private Dictionary<string, string> localizedText;

    // Start is called before the first frame update
    void Start()
    {
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

    public void SetLanguage(Language language)
    {
        currentLanguage = language;
        LoadLocalizedText();
    }

    public string GetLocalizedText(string key)
    {
        if (localizedText.ContainsKey(key))
            return localizedText[key];
        else
            return "KEY_NOT_FOUND";
    }

    private void LoadLocalizedText()
    {
        localizedText = new Dictionary<string, string>();

        string json;

        switch (currentLanguage)
        {
            case Language.English:
                json = englishLocalizationFile.text;
                break;
            case Language.Portuguese:
                json = portugueseLocalizationFile.text;
                break;
            default:
                json = englishLocalizationFile.text;
                break;
        }

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

        foreach (LocalizationItem item in loadedData.items)
        {
            localizedText.Add(item.key, item.value);
        }

        // After the loop, check if items were loaded successfully
        if (localizedText.Count == 0)
        {
            Debug.LogWarning("No localization items loaded.");
        }
    }
}