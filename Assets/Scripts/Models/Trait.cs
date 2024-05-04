/// <summary>
/// Represents a trait with its name and value.
/// </summary>
[System.Serializable]
public class Trait
{
    // The name of the trait.
    public string Name;

    // The value normalized of the trait.
    public double ValueNormalized;

    // The value of the trait.
    public double? Value;

    // Constructor for Trait class
    public Trait(string Name, double ValueNormalized, double? Value)
    {
        this.Name = Name;
        this.ValueNormalized = ValueNormalized;
        this.Value = Value;
    }
}