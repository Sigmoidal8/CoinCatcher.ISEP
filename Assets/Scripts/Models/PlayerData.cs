/// <summary>
/// Represents data related to a player.
/// </summary>
[System.Serializable]
public class PlayerData
{
    //The username of the player.
    public string Username;

    // Constructor for the PlayerData class.
    public PlayerData(string Username)
    {
        this.Username = Username;
    }
}