using System.Collections.Generic;

[System.Serializable]
public class LocalizationData
{
    public List<LocalizationItem> items = new List<LocalizationItem>(); // Initialize list to avoid null reference
}