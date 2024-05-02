/// <summary>
/// Represents user data including game time, description, and trait data.
/// </summary>
[System.Serializable]
public class UserData
{
    public string GameTime;
    public string Description;
    public TraitData TraitData;

    public UserData(string GameTime, string Description, TraitData TraitData)
    {
        this.GameTime = GameTime;
        this.Description = Description;
        this.TraitData = TraitData;
    }
}