using System.Collections.Generic;

[System.Serializable]
public class MoralityValuesPerScene
{
    // List of morality values for each option in the dilemma
    public List<double> moralityValues;
    public List<string> moralityDescriptions;

    // Constructor for MoralityValuesPerScene class
    public MoralityValuesPerScene()
    {
        moralityValues = new List<double>();
        moralityDescriptions = new List<string>();
    }
}