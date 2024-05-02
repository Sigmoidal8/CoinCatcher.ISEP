[System.Serializable]
public class Trait
{
    // Indicates whether the dilemma is completed
    public string trait;

    public double value;

    // Constructor for MoralDilemmaData class
    public Trait(string trait, double value)
    {
        this.trait = trait;
        this.value = value;
    }
}