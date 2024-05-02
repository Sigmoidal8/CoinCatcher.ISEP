using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages coins in the game
/// </summary>
[System.Serializable]
public class CoinManager : MonoBehaviour
{
    // Static Instance of CoinManager
    public static CoinManager Instance;

    // Dictionary to store scene name and corresponding coin data
    private Dictionary<string, SceneCoinsData> SceneCoins = new Dictionary<string, SceneCoinsData>();

    // Awake is called when the script Instance is being loaded
    void Awake()
    {
        // Ensure only one Instance of CoinManager exists across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Register the OnSceneLoaded method to be called when a scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Called when a scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Load coins data for the scene
        LoadCoins(scene.name);

        // Check if the scene name is already in the dictionary
        if (!Instance.SceneCoins.ContainsKey(scene.name))
        {
            // If not, add the scene to the dictionary with an empty list of coins
            Instance.SceneCoins.Add(scene.name, new SceneCoinsData());
        }

        // Get all coin objects in the scene
        GameObject[] coinObjects = GameObject.FindGameObjectsWithTag(Constants.CoinTag);

        // Add each coin object to the list for the current scene
        foreach (GameObject coinObject in coinObjects)
        {
            if (Instance.SceneCoins[scene.name].Coins.Where(x => x.CoinObjectId == coinObject.name).Count() == 0)
            {
                AddCoin(coinObject, scene.name);
            }
        }
    }

    // Add a coin to the list for the specified scene
    private void AddCoin(GameObject coinObject, string sceneName)
    {
        Instance.SceneCoins[sceneName].Coins.Add(new Coin(coinObject, false));
    }

    // Method to handle collecting a coin
    public void CollectCoin(GameObject coinObject)
    {
        // Get the scene name of the coin object
        string sceneName = coinObject.scene.name;

        // Iterate through coins to see which coin was collected
        foreach (Coin coin in Instance.SceneCoins[sceneName].Coins)
        {
            if (coin.CoinObjectId == coinObject.name)
            {
                // Update coin state
                coinObject.SetActive(false);
                coin.Collected = true;
                break;
            }
        }
        // Save updated coins data
        SaveCoins(sceneName);
    }

    // Method to check if a coin is already collected
    public bool IsCoinCollected(GameObject coinObject)
    {
        // Get the scene name of the coin object
        string sceneName = coinObject.scene.name;

        // Iterate through coins to see if the coin was collected
        foreach (Coin coin in Instance.SceneCoins[sceneName].Coins)
        {
            if (coin.CoinObjectId == coinObject.name)
            {
                coinObject.SetActive(false);
                return coin.Collected;
            }
        }
        return false;
    }

    // Method to save coins data for a scene
    private void SaveCoins(string sceneName)
    {
        SceneCoinsData SceneCoinsData = new SceneCoinsData(Instance.SceneCoins[sceneName].Coins);
        string json = JsonUtility.ToJson(SceneCoinsData);
        PlayerPrefs.SetString(sceneName + Constants.CoinDataPlayerPrefs, json);
        PlayerPrefs.Save();
    }

    // Method to load coins data for a scene
    private void LoadCoins(string sceneName)
    {
        // Load coins data from player prefs
        if (PlayerPrefs.HasKey(sceneName + Constants.CoinDataPlayerPrefs))
        {
            string json = PlayerPrefs.GetString(sceneName + Constants.CoinDataPlayerPrefs);
            SceneCoinsData coinDataList = JsonUtility.FromJson<SceneCoinsData>(json);

            // Iterate through the loaded coins
            SceneCoinsData loadedCoins = new SceneCoinsData();
            foreach (Coin coinData in coinDataList.Coins)
            {
                loadedCoins.Coins.Add(coinData);
                GameObject coinObject = GameObject.Find(coinData.CoinObjectId);
                if (coinObject != null)
                {
                    // If coin collected disable it from the scene
                    if (coinData.Collected)
                    {
                        coinObject.SetActive(false);
                    }
                }
                else
                {
                    Debug.LogWarning("Coin object not found for loaded coin in scene: " + sceneName);
                }
            }
            // Replace the existing list of coins with the loaded list
            if (Instance.SceneCoins.ContainsKey(sceneName))
            {
                Instance.SceneCoins[sceneName].Coins = loadedCoins.Coins;
            }
            else
            {
                Instance.SceneCoins.Add(sceneName, loadedCoins);
                Debug.LogWarning("Scene coins data not found for scene: " + sceneName);
            }
        }
    }

    // Method to check if there is any coin in the scene
    public bool IsAnyCoinFromSceneCollected()
    {
        IEnumerable<Coin> coinsNotCollected = Instance.SceneCoins[SceneManager.GetActiveScene().name].Coins.Where(coin => coin.Collected == true);
        return coinsNotCollected.Count() != 0;
    }

    // Method to check if all coins have been collected in the scene
    public bool AreAllCoinsFromSceneCollected()
    {
        IEnumerable<Coin> coinsNotCollected = Instance.SceneCoins[SceneManager.GetActiveScene().name].Coins.Where(coin => coin.Collected == false);
        return coinsNotCollected.Count() == 0;
    }

    // Method to check how many coins missing from the scene
    public int CountMissingCoinsFromScene()
    {
        List<Coin> coinsNotCollected = Instance.SceneCoins[SceneManager.GetActiveScene().name].Coins.Where(coin => coin.Collected == false).ToList();
        return coinsNotCollected.Count();
    }

}
