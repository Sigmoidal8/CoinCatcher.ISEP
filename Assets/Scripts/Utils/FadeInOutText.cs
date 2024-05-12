using System.Collections;
using TMPro;
using UnityEngine;

public class FadeInOutText : MonoBehaviour
{
    public TextMeshProUGUI TextMeshPro;
    public float FadeDuration = 1.0f;

    private void Start()
    {
        StartCoroutine(FadeInOut());
    }

    IEnumerator FadeInOut()
    {
        while (true)
        {
            yield return FadeText(true);
            yield return new WaitForSeconds(0.5f); // Adjust delay between fade in and out
            yield return FadeText(false);
            yield return new WaitForSeconds(0.5f); // Adjust delay between fade out and in
        }
    }

    IEnumerator FadeText(bool fadeIn)
    {
        float startAlpha = fadeIn ? 0f : 1f;
        float endAlpha = fadeIn ? 1f : 0f;
        float timer = 0f;

        while (timer < FadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timer / FadeDuration);
            TextMeshPro.alpha = alpha;
            timer += Time.deltaTime;
            yield return null;
        }

        TextMeshPro.alpha = endAlpha;
    }
}
