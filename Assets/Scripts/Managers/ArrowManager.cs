using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Manages the visibility and functionality of navigation arrows in the UI.
/// </summary>
public class ArrowManager : MonoBehaviour
{
    // UI elements for left and right arrows
    [Header("Arrows")]
    public GameObject LeftArrow;
    public TextMeshProUGUI LeftArrowText;
    public GameObject RightArrow;
    public TextMeshProUGUI RightArrowText;

    [Header("Scene Names")]
    // Names of previous and next scenes
    public LanguageFields PreviousSceneNameString;
    public LanguageFields NextSceneNameString;

    private LanguageManager LanguageManager;

    private readonly  int TutorialScene = 1;
    private readonly  int SceneAfterTutorial = 2;

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
        MoralDilemmaData[] moralDilemmaStates = sceneController.MoralDilemmaStates;

        // Check if it's the first scene
        if (currentSceneIndex == TutorialScene)
        {
            // Hide left arrow and show right arrow if the next moral dilemma is completed
            LeftArrow.SetActive(false);
            if (moralDilemmaStates[TutorialScene].Completed)
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
        // Check if it's the first scene after the tutorial
        else if (currentSceneIndex == SceneAfterTutorial)
        {
            // Hide left arrow and check if the scene is completed to show right arrow
            LeftArrow.SetActive(false);
            if (moralDilemmaStates[currentSceneIndex].Completed)
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
            if (moralDilemmaStates[currentSceneIndex - 1].Completed)
            {
                LeftArrow.SetActive(true);
                LeftArrowText.text = LanguageManager.GetLocalizedText(LanguageFields.arrow_message.ToString()) + LanguageManager.GetLocalizedText(PreviousSceneNameString.ToString());
            }
            else
            {
                LeftArrow.SetActive(false);
            }

            // Check if the current moral dilemma is completed
            if (moralDilemmaStates[currentSceneIndex].Completed)
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
        PlayerPrefs.DeleteAll();
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