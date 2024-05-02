using TMPro;
using UnityEngine;

// This class handles the animation of the coin counter text
public class CoinCounterAnimation : MonoBehaviour
{
    // Reference to the GameObject containing the coin counter text
    public GameObject CoinCounterText;

    // Duration of the animation in seconds
    public float AnimationDuration = 1.5f;

    // Scale factor for the animation
    public float ScaleFactor = 1.5f;

    public AudioClip CoinCollectedAudioClip;

    public AudioSource CoinCollectedAudioSource;

    // Timer for the animation
    private float Timer;

    // Reference to the TextMeshProUGUI component of the coin counter text
    private TextMeshProUGUI TextMeshProUGUI;

    // Initial color of the text
    private Color InitialColor;

    // Transparent color with zero alpha
    private Color TransparentColor = new Color(1f, 1f, 1f, 0f);

    // Start is called before the first frame update
    private void Start()
    {
        // Get the material of the CoinCounterText GameObject
        TextMeshProUGUI textMeshProUGUI_obj = CoinCounterText.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI = textMeshProUGUI_obj;
        InitialColor = TextMeshProUGUI.color;
    }

    // Update is called once per frame
    private void Update()
    {
        // If the Timer is greater than zero, continue the animation
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
            float progress = 1f - (Timer / AnimationDuration);
            float scale = Mathf.Lerp(ScaleFactor, 1f, progress);
            TextMeshProUGUI.transform.localScale = new Vector3(scale, scale, 1f);

            // Interpolate the alpha value
            TextMeshProUGUI.color = Color.Lerp(InitialColor, TransparentColor, progress);
        }
        else
        {
            // If the animation is finished, deactivate the coin counter text GameObject
            CoinCounterText.SetActive(false);
        }
    }

    // Method to play the animation
    public void PlayAnimation(int coinsAdded, bool isToAdd)
    {
        if (isToAdd)
        {
            TextMeshProUGUI.text = "+" + coinsAdded.ToString();
        }else{
            TextMeshProUGUI.text = "-" + coinsAdded.ToString();
        }
        CoinCollectedAudioSource.PlayOneShot(CoinCollectedAudioClip);
        // Activate the coin counter text GameObject
        CoinCounterText.SetActive(true);
        // Reset the Timer and scale of the animation
        Timer = AnimationDuration;
        TextMeshProUGUI.transform.localScale = new Vector3(ScaleFactor, ScaleFactor, 1f);
        // Reset the color of the text
        TextMeshProUGUI.color = InitialColor;
    }
}
