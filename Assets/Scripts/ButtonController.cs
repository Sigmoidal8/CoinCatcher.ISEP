using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// This class controls the functionality of buttons in the UI
[System.Serializable]
public class ButtonController : MonoBehaviour
{
    // Reference to the settings panel in the UI
    public GameObject SettingsPanel;

    public GameObject UsernamePanel;

    public GameObject Alert;

    public TMP_InputField InputField;

    // Method to toggle the visibility of the settings panel
    public void ToggleSettingsPanel()
    {
        // Check if the settings panel is currently active
        if (SettingsPanel.activeInHierarchy == false)
        {
            // Activate the settings panel if it's currently inactive
            SettingsPanel.SetActive(true);
        }
        else
        {
            // Deactivate the settings panel if it's currently active
            SettingsPanel.SetActive(false);
        }
    }

    public void ShowUsernamePanel()
    {
        UsernamePanel.SetActive(true);
    }

        public void HideUsernamePanel()
    {
        UsernamePanel.SetActive(false);
    }

    // Method to handle the start game button click event
    public void StartGameButton()
    {
        if (ValidateUsername())
        {
            // Extract name and age
            string playerName = InputField.text;

            string existingJsonFilePathPlayer = Path.Combine(Application.persistentDataPath, $"userfiles.json");
            if(!File.Exists(existingJsonFilePathPlayer)){
                File.WriteAllText(existingJsonFilePathPlayer, JsonUtility.ToJson("{userData:[]}"));
            }
            /*
             string newJsonFilePath = Path.Combine(Application.persistentDataPath, newJsonFileName
             PlayerData playerData = new PlayerData(playerName
             // Create and save new JSON file
             File.WriteAllText(newJsonFilePath, JsonUtility.ToJson(playerData));
             */

            // Store the path to the new JSON file for later use
            PlayerPrefs.SetString(Constants.PlayerFabsPlayerName, playerName);
            PlayerPrefs.Save();

            // Load the entry scene
            SceneManager.LoadScene(Constants.EntryScene);
        }
        else
        {
            Alert.SetActive(true);
        }
    }

    private bool ValidateUsername()
    {
        string playerName = InputField.text;
        if (string.IsNullOrEmpty(playerName))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // Method to handle the exit game button click event
    public void ExitGameButton()
    {
        // Delete player preferences and quit the application
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
