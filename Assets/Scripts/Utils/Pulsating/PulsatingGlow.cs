using UnityEngine;

public class PulsatingGlow : MonoBehaviour
{
    public Material glowMaterial;
    public float minIntensity = 0f;
    public float maxIntensity = 1f;
    public float glowSpeed = 1f;

    private float currentIntensity;
    private bool increasing = true;

    void Update()
    {
        currentIntensity += (increasing ? glowSpeed : -glowSpeed) * Time.deltaTime;
        currentIntensity = Mathf.Clamp01(currentIntensity);
        glowMaterial.SetFloat("_EmissionIntensity", Mathf.Lerp(minIntensity, maxIntensity, currentIntensity));

        if (currentIntensity >= 1f || currentIntensity <= 0f)
        {
            increasing = !increasing;
        }
    }
}
