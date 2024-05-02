// Represents a coin object
using UnityEngine;

[System.Serializable]
public class Coin
{
    // Name of the coin object
    public string coinObjectId;

    // Indicates whether the coin is collected
    public bool collected;

    public Coin(GameObject pos, bool collected)
    {
        coinObjectId = pos.name; // Store the name of the GameObject
        this.collected = collected;
    }

}