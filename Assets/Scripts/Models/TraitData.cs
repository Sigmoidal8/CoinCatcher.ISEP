using System.Collections.Generic;

[System.Serializable]
public class TraitData
{
    // Indicates whether the dilemma is completed
    public List<Trait> Traits;

    // Constructor for MoralDilemmaData class
    public TraitData(List<Trait> Traits)
    {
        this.Traits = Traits;
    }
}