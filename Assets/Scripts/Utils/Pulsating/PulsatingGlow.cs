using UnityEngine;

// This script creates a pulsating glow effect by modifying the emission intensity of a material.
public class PulsatingGlow : MonoBehaviour
{
    public Material GlowMaterial; // The material to modify for the glow effect
    public float MinIntensity = 0f; // The minimum intensity of the glow
    public float MaxIntensity = 1f; // The maximum intensity of the glow
    public float GlowSpeed = 1f; // The speed at which the intensity changes

    private float CurrentIntensity; // The current intensity of the glow
    private bool Increasing = true; // Flag indicating whether the intensity is increasing or decreasing

    void Update()
    {
        // Adjust the current intensity based on whether it is increasing or decreasing
        CurrentIntensity += (Increasing ? GlowSpeed : -GlowSpeed) * Time.deltaTime;
        // Clamp the intensity between 0 and 1
        CurrentIntensity = Mathf.Clamp01(CurrentIntensity);

        // Set the emission intensity of the material using a linear interpolation between the min and max intensities
        GlowMaterial.SetFloat("_EmissionIntensity", Mathf.Lerp(MinIntensity, MaxIntensity, CurrentIntensity));

        // If the intensity reaches either extreme, reverse the direction of change
        if (CurrentIntensity >= 1f || CurrentIntensity <= 0f)
        {
            Increasing = !Increasing;
        }
    }
}
