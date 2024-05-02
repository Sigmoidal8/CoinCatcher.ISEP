using System.Collections.Generic;

/// <summary>
/// Represents morality values associated with each option in a dilemma for a scene.
/// </summary>
[System.Serializable]
public class MoralityValuesPerScene
{
    // List of morality values for each option in the dilemma
    public List<double> MoralityValues;
    //List of descriptions for each option's morality in the dilemma.
    public List<string> MoralityDescriptions;

    // Constructor for MoralityValuesPerScene class
    public MoralityValuesPerScene()
    {
        MoralityValues = new List<double>();
        MoralityDescriptions = new List<string>();
    }
}