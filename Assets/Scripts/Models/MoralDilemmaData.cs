[System.Serializable]
public class MoralDilemmaData
{
    // Indicates whether the dilemma is completed
    public bool completed;

    // Index of the chosen answer
    public int moralDilemaChosenOption;

    public string moralDilemaChosenOptionDescription;

    public double decisionValue;

    public string sceneName;

    public int coinsCollected;

    public TimeMeasurement timestamps;

    // Constructor for MoralDilemmaData class
    public MoralDilemmaData(bool completed, int moralDilemaChosenOption, string sceneName, int coinsCollected, string moralDilemaChosenOptionDescription, double decisionValue, TimeMeasurement timestamps)
    {
        this.completed = completed;
        this.moralDilemaChosenOption = moralDilemaChosenOption;
        this.sceneName = sceneName;
        this.coinsCollected = coinsCollected;
        this.moralDilemaChosenOptionDescription = moralDilemaChosenOptionDescription;
        this.decisionValue = decisionValue;
        this.timestamps = timestamps;
    }
}