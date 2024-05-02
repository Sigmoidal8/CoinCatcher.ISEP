[System.Serializable]
public class UserData
{
    public string gameTime;
    public string description;
    public TraitData traitData;

    public UserData(string gameTime, string description, TraitData traitData)
    {
       this.gameTime = gameTime;
       this.description = description;
       this.traitData = traitData; 
    }
}