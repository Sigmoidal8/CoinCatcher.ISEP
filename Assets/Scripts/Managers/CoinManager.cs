using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

// Manages coins in the game
[System.Serializable]
public class CoinManager : MonoBehaviour
{
    // Static instance of CoinManager
    public static CoinManager instance;

    // Dictionary to store scene name and corresponding coin data
    private Dictionary<string, SceneCoinsData> sceneCoins = new Dictionary<string, SceneCoinsData>();

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Ensure only one instance of CoinManager exists across scenes
        if (instance == null)
        {
            instance = this;
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
        if (!instance.sceneCoins.ContainsKey(scene.name))
        {
            // If not, add the scene to the dictionary with an empty list of coins
            instance.sceneCoins.Add(scene.name, new SceneCoinsData());
        }

        // Get all coin objects in the scene
        GameObject[] coinObjects = GameObject.FindGameObjectsWithTag(Constants.CoinTag);

        // Add each coin object to the list for the current scene
        foreach (GameObject coinObject in coinObjects)
        {
            if (instance.sceneCoins[scene.name].coins.Where(x => x.coinObjectId == coinObject.name).Count() == 0)
            {
                AddCoin(coinObject, scene.name);
            }
        }
    }

    // Add a coin to the list for the specified scene
    private void AddCoin(GameObject coinObject, string sceneName)
    {
        instance.sceneCoins[sceneName].coins.Add(new Coin(coinObject, false));
    }

    // Method to handle collecting a coin
    public void CollectCoin(GameObject coinObject)
    {
        // Get the scene name of the coin object
        string sceneName = coinObject.scene.name;

        // Get the list of coins for the current scene

        foreach (Coin coin in instance.sceneCoins[sceneName].coins)
        {
            if (coin.coinObjectId == coinObject.name)
            {
                // Update coin state
                coinObject.SetActive(false);
                coin.collected = true;
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

        // Get the list of coins for the current scene
        List<Coin> coins = instance.sceneCoins[sceneName].coins;

        foreach (Coin coin in coins)
        {
            if (coin.coinObjectId == coinObject.name)
            {
                coinObject.SetActive(false);
                return coin.collected;
            }
        }
        return false;
    }

    // Method to save coins data for a scene
    private void SaveCoins(string sceneName)
    {
        SceneCoinsData sceneCoinsData = new SceneCoinsData(instance.sceneCoins[sceneName].coins);
        string json = JsonUtility.ToJson(sceneCoinsData);
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
            foreach (Coin coinData in coinDataList.coins)
            {
                loadedCoins.coins.Add(coinData);
                GameObject coinObject = GameObject.Find(coinData.coinObjectId);
                if (coinObject != null)
                {
                    if (coinData.collected)
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
            if (instance.sceneCoins.ContainsKey(sceneName))
            {
                instance.sceneCoins[sceneName].coins = loadedCoins.coins;
            }
            else
            {
                instance.sceneCoins.Add(sceneName, loadedCoins);
                Debug.LogWarning("Scene coins data not found for scene: " + sceneName);
            }
        }
    }

    public bool IsAnyCoinFromSceneCollected()
    {
        IEnumerable<Coin> coinsNotCollected = instance.sceneCoins[SceneManager.GetActiveScene().name].coins.Where(coin => coin.collected == true);
        return coinsNotCollected.Count() != 0;
    }

    public bool AreAllCoinsFromSceneCollected()
    {
        IEnumerable<Coin> coinsNotCollected = instance.sceneCoins[SceneManager.GetActiveScene().name].coins.Where(coin => coin.collected == false);
        return coinsNotCollected.Count() == 0;
    }

    public int CountMissingCoinsFromScene()
    {
        List<Coin> coinsNotCollected = instance.sceneCoins[SceneManager.GetActiveScene().name].coins.Where(coin => coin.collected == false).ToList();
        return coinsNotCollected.Count();
    }

}
