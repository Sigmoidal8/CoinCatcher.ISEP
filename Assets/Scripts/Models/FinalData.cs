using System.Collections.Generic;

[System.Serializable]
public class FinalDataList
{
    public List<FinalData> userData;

    public FinalDataList()
    {
        userData = new List<FinalData>();
    }
}

/// <summary>
/// Serializable class to store final game data
/// </summary>
[System.Serializable]
public class FinalData
{
    public PlayerData PlayerData;
    public GameData MetricsData;
    public TraitData PersonalityData;

    // Constructor for SceneCoinsData class
    public FinalData(PlayerData PlayerData, GameData MetricsData, TraitData PersonalityData)
    {
        this.PlayerData = PlayerData;
        this.MetricsData = MetricsData;
        this.PersonalityData = PersonalityData;
    }
}