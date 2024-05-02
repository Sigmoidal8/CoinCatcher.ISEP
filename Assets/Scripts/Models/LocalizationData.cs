using System.Collections.Generic;

/// <summary>
/// Serializable class to store localization data
/// </summary>
[System.Serializable]
public class LocalizationData
{
    // List of items
    public List<LocalizationItem> items = new List<LocalizationItem>(); // Initialize list to avoid null reference
}