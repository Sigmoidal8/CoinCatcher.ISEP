/// <summary>
/// Represents data for a moral dilemma.
/// </summary>
[System.Serializable]
public class MoralDilemmaData
{
    // Indicates whether the dilemma is Completed
    public bool Completed;

    // Index of the chosen answer
    public int MoralDilemmaChosenOption;

    public string MoralDilemmaChosenOptionDescription;

    public double DecisionValue;

    public string SceneName;

    public int CoinsCollected;

    public TimeMeasurement Timestamps;

    // Constructor for MoralDilemmaData class
    public MoralDilemmaData(bool Completed, int MoralDilemmaChosenOption, string SceneName, int CoinsCollected, string MoralDilemmaChosenOptionDescription, double DecisionValue, TimeMeasurement Timestamps)
    {
        this.Completed = Completed;
        this.MoralDilemmaChosenOption = MoralDilemmaChosenOption;
        this.SceneName = SceneName;
        this.CoinsCollected = CoinsCollected;
        this.MoralDilemmaChosenOptionDescription = MoralDilemmaChosenOptionDescription;
        this.DecisionValue = DecisionValue;
        this.Timestamps = Timestamps;
    }
}