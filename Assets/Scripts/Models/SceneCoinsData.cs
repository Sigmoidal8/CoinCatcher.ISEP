using System.Collections.Generic;

/// <summary>
/// Represents data related to coins in a scene.
/// </summary>
[System.Serializable]
public class SceneCoinsData
{
    // List of Coins in the scene
    public List<Coin> Coins;

    // Constructor for SceneCoinsData class
    public SceneCoinsData(List<Coin> Coins)
    {
        this.Coins = Coins;
    }

    // Default constructor for SceneCoinsData class
    public SceneCoinsData()
    {
        this.Coins = new List<Coin>();
    }
}