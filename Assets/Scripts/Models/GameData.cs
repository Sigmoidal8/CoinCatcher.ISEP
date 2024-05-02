/// <summary>
/// Serializable class to store game data
/// </summary>
[System.Serializable]
public class GameData
{
    // Array to store moral dilemma data
    public MoralDilemmaData[] MoralDilemmaData;

    // Game time measurement
    public TimeMeasurement GameTime;

    // Constructor for MoralDilemmaData class
    public GameData(MoralDilemmaData[] MoralDilemmaData, TimeMeasurement GameTime)
    {
        this.MoralDilemmaData = MoralDilemmaData;
        this.GameTime = GameTime;
    }
}