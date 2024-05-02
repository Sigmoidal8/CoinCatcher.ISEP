using System.Collections.Generic;

[System.Serializable]
public class FinalData
{
    public PlayerData playerData;
    public GameData metricsData;
    public TraitData personalityData;

    // Constructor for SceneCoinsData class
    public FinalData(PlayerData playerData, GameData metricsData, TraitData personalityData)
    {
        this.playerData = playerData;
        this.metricsData = metricsData;
        this.personalityData = personalityData;
    }
}