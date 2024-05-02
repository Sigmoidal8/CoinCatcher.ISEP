using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    // Indicates whether the dilemma is completed
    public MoralDilemmaData[] moralDilemmaData;

    public TimeMeasurement gameTime;

    // Constructor for MoralDilemmaData class
    public GameData(MoralDilemmaData[] moralDilemmaData, TimeMeasurement gameTime)
    {
        this.moralDilemmaData = moralDilemmaData;
        this.gameTime = gameTime;
    }
}