
using UnityEngine;

/// <summary>
/// Represents a coin object
/// </summary>
[System.Serializable]
public class Coin
{
    // Name of the coin object
    public string CoinObjectId;

    // Indicates whether the coin is Collected
    public bool Collected;

    public Coin(GameObject pos, bool Collected)
    {
        // Store the name of the GameObject as the coin object ID
        CoinObjectId = pos.name;
        this.Collected = Collected;
    }

}