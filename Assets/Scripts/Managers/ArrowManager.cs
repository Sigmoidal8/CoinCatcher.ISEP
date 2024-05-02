using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// This class manages the visibility and functionality of navigation arrows in the UI
public class ArrowManager : MonoBehaviour
{
    // UI elements for left and right arrows
    public GameObject LeftArrow;
    public TextMeshProUGUI LeftArrowText;
    public GameObject RightArrow;
    public TextMeshProUGUI RightArrowText;

    // Names of previous and next scenes
    public LanguageFields PreviousSceneNameString;
    public LanguageFields NextSceneNameString;

    private LanguageManager LanguageManager;

    // Start is called before the first frame update
    void Start()
    {
        // Update the visibility of arrows when the scene starts
        LanguageManager = FindObjectOfType<LanguageManager>();
        if (LanguageManager == null)
        {
            GameObject languageManagerObject = new GameObject(Constants.LanguageManagerComponent);
            LanguageManager = languageManagerObject.AddComponent<LanguageManager>();
        }
        UpdateArrowVisibility();
    }

    // Method to update the visibility of navigation arrows based on the current scene
    public void UpdateArrowVisibility()
    {
        // Get the index of the current scene and total number of scenes
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Minus one to ignore end scene
        int totalScenes = SceneManager.sceneCountInBuildSettings - 1;

        // Find the SceneController in the scene
        SceneController sceneController = FindObjectOfType<SceneController>();

        // Get the array of moral dilemma states from SceneController
        MoralDilemmaData[] moralDilemmaStates = sceneController.moralDilemmaStates;

        // Check if it's the first scene
        if (currentSceneIndex == 1)
        {
            // Hide left arrow and show right arrow if the next moral dilemma is completed
            LeftArrow.SetActive(false);
            if (moralDilemmaStates[1].completed)
            {
                RightArrow.SetActive(true);
                RightArrowText.text = LanguageManager.GetLocalizedText(LanguageFields.arrow_message.ToString()) + LanguageManager.GetLocalizedText(NextSceneNameString.ToString());
            }
            else
            {
                RightArrow.SetActive(false);
            }
        }
        // Check if it's the last scene
        else if (currentSceneIndex == totalScenes - 1)
        {
            // Show left arrow and hide right arrow
            LeftArrow.SetActive(true);
            LeftArrowText.text = LanguageManager.GetLocalizedText(LanguageFields.arrow_message.ToString()) + LanguageManager.GetLocalizedText(PreviousSceneNameString.ToString());
            RightArrow.SetActive(false);
        }
        else if (currentSceneIndex == 2)
        {
            LeftArrow.SetActive(false);
            if (moralDilemmaStates[currentSceneIndex].completed)
            {
                RightArrow.SetActive(true);
                RightArrowText.text = LanguageManager.GetLocalizedText(LanguageFields.arrow_message.ToString()) + LanguageManager.GetLocalizedText(NextSceneNameString.ToString());
            }
            else
            {
                RightArrow.SetActive(false);
            }
        }
        // For middle scenes
        else
        {
            // Check if the previous moral dilemma is completed
            if (moralDilemmaStates[currentSceneIndex - 1].completed)
            {
                LeftArrow.SetActive(true);
                LeftArrowText.text = LanguageManager.GetLocalizedText(LanguageFields.arrow_message.ToString()) + LanguageManager.GetLocalizedText(PreviousSceneNameString.ToString());
            }
            else
            {
                LeftArrow.SetActive(false);
            }

            // Check if the current moral dilemma is completed
            if (moralDilemmaStates[currentSceneIndex].completed)
            {
                RightArrow.SetActive(true);
                RightArrowText.text = LanguageManager.GetLocalizedText(LanguageFields.arrow_message.ToString()) + LanguageManager.GetLocalizedText(NextSceneNameString.ToString());
            }
            else
            {
                RightArrow.SetActive(false);
            }
        }
    }

    // Method to load the main menu scene
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(Constants.MainMenu);
    }

    // Method to load the next scene
    public void GoToNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    // Method to load the previous scene
    public void GoToPreviousScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex - 1);
    }
}