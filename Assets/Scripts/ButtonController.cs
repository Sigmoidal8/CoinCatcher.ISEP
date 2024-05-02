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

    public GameObject usernamePanel;

    public GameObject alert;

    public TMP_InputField inputField;

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
        usernamePanel.SetActive(true);
    }

    // Method to handle the start game button click event
    public void StartGameButton()
    {
        if (ValidateUsername())
        {
            // Extract name and age
            string playerName = inputField.text;

            // Create new JSON file name
            string newJsonFileName = $"{playerName}.json";
            string newJsonFilePath = Path.Combine(Application.persistentDataPath, newJsonFileName);

            PlayerData playerData = new PlayerData(playerName);

            // Create and save new JSON file
            File.WriteAllText(newJsonFilePath, JsonUtility.ToJson(playerData));

            // Store the path to the new JSON file for later use
            PlayerPrefs.SetString(Constants.PlayerFabsPlayerName, playerName);
            PlayerPrefs.Save();

            // Load the entry scene
            SceneManager.LoadScene(Constants.EntryScene);
        }
        else
        {
            alert.SetActive(true);
        }
    }

    private bool ValidateUsername()
    {
        string playerName = inputField.text;
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
