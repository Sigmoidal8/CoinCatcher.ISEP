/// <summary>
/// Constants class containing various constant values used throughout the game.
/// </summary>
public static class Constants
{
    #region PlayerPrefs

    // Player name key for PlayerPrefs.
    public static string PlayerFabsPlayerName = "PlayerName";

    // Key for storing coin data in PlayerPrefs.
    public static string CoinDataPlayerPrefs = "_CoinsData";

    // Key for storing general game data in PlayerPrefs.
    public static string GameDataPlayerPrefs = "GameData";

    #endregion

    #region Tags

    // Tag for identifying coins in the game.
    public static string CoinTag = "Coin";

    // Tag for identifying the amount of coins.
    public static string CoinAmountTag = "CoinAmount";

    #endregion

    #region Scenes

    // Scene name for the main menu.
    public static string MainMenu = "MainMenu";

    // Scene name for the entry scene.
    public static string EntryScene = "EntryScene";

    // Scene name for the house scene.
    public static string HouseScene = "HouseScene";

    #endregion

    #region Components

    // Name of the coin animation component.
    public static string CoinAnimationComponent = "CoinAmount";

    // Name of the language manager component.
    public static string LanguageManagerComponent = "LanguageManager";

    // Name of the scene controller component.
    public static string SceneControllerComponent = "SceneController";

    // Name of the arrow manager component.
    public static string ArrowManagerComponent = "ArrowManager";

    #endregion
    
    // Coin objective value.
    public static int CoinObjective = 25;
}