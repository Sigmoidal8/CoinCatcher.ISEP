using System.Collections.Generic;

[System.Serializable]
public class TraitData
{
    // Indicates whether the dilemma is completed
    public List<Trait> traits;

    // Constructor for MoralDilemmaData class
    public TraitData(List<Trait> traits)
    {
        this.traits = traits;
    }
}