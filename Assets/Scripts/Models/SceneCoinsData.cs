using System.Collections.Generic;

[System.Serializable]
public class SceneCoinsData
{
    // List of coins in the scene
    public List<Coin> coins;

    // Constructor for SceneCoinsData class
    public SceneCoinsData(List<Coin> coins)
    {
        this.coins = coins;
    }

    // Default constructor for SceneCoinsData class
    public SceneCoinsData()
    {
        this.coins = new List<Coin>();
    }
}