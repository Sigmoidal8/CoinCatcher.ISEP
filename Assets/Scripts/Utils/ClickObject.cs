using UnityEngine;

// This class detects touch input on coins in the scene
public class ClickObject : MonoBehaviour
{

    // Reference to the CoinManager script
    public CoinManager CoinsManager;

    // Reference to the SceneController script
    public SceneController SceneController;

    // Update is called once per frame
    void FixedUpdate()
    {
        // Iterate over all touches
        for (int i = 0; i < Input.touchCount; i++)
        {
            // Check if the touch phase is began
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                // Get the touch position and cast a ray from it
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit hit;

                // Check if the ray hits any collider
                if (Physics.Raycast(ray, out hit, Mathf.Infinity)) //layerMask
                {
                    // Get the game object that was hit
                    GameObject hitObject = hit.collider.gameObject;

                    // Check if the hit object has the "Coin" tag
                    if (hitObject.tag == Constants.CoinTag)
                    {
                        // If it's a coin, collect it and update scene
                        CoinsManager.CollectCoin(hitObject);
                        SceneController.CoinCollected(1);
                    }
                }
            }
        }
    }
}
