using TMPro;
using UnityEngine;

// This class handles the animation of the coin counter text
public class CoinCounterAnimation : MonoBehaviour
{
    // Reference to the GameObject containing the coin counter text
    public GameObject coinCounterText;

    // Duration of the animation in seconds
    public float animationDuration = 1.5f;

    // Scale factor for the animation
    public float scaleFactor = 1.5f;

    public AudioClip coinCollectedAudioClip;

    public AudioSource coinCollectedAudioSource;

    // Timer for the animation
    private float timer;

    // Reference to the TextMeshProUGUI component of the coin counter text
    private TextMeshProUGUI textMeshProUGUI;

    // Initial color of the text
    private Color initialColor;

    // Transparent color with zero alpha
    private Color transparentColor = new Color(1f, 1f, 1f, 0f);

    // Start is called before the first frame update
    private void Start()
    {
        // Get the material of the coinCounterText GameObject
        TextMeshProUGUI textMeshProUGUI_obj = coinCounterText.GetComponent<TextMeshProUGUI>();
        textMeshProUGUI = textMeshProUGUI_obj;
        initialColor = textMeshProUGUI.color;
    }

    // Update is called once per frame
    private void Update()
    {
        // If the timer is greater than zero, continue the animation
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            float progress = 1f - (timer / animationDuration);
            float scale = Mathf.Lerp(scaleFactor, 1f, progress);
            textMeshProUGUI.transform.localScale = new Vector3(scale, scale, 1f);

            // Interpolate the alpha value
            textMeshProUGUI.color = Color.Lerp(initialColor, transparentColor, progress);
        }
        else
        {
            // If the animation is finished, deactivate the coin counter text GameObject
            coinCounterText.SetActive(false);
        }
    }

    // Method to play the animation
    public void PlayAnimation(int coinsAdded, bool isToAdd)
    {
        if (isToAdd)
        {
            textMeshProUGUI.text = "+" + coinsAdded.ToString();
        }else{
            textMeshProUGUI.text = "-" + coinsAdded.ToString();
        }
        coinCollectedAudioSource.PlayOneShot(coinCollectedAudioClip);
        // Activate the coin counter text GameObject
        coinCounterText.SetActive(true);
        // Reset the timer and scale of the animation
        timer = animationDuration;
        textMeshProUGUI.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
        // Reset the color of the text
        textMeshProUGUI.color = initialColor;
    }
}
