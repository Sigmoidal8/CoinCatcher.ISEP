/// <summary>
/// Represents a trait with its name and value.
/// </summary>
[System.Serializable]
public class Trait
{
    // The name of the trait.
    public string Name;

    // The value of the trait.
    public double Value;

    // Constructor for Trait class
    public Trait(string Name, double Value)
    {
        this.Name = Name;
        this.Value = Value;
    }
}