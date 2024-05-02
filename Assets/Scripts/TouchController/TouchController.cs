using UnityEngine;

// Controls touch input for camera movement
public class TouchController : MonoBehaviour
{
    // Reference to the fixed touch field for touch input
    public FixedTouchField _FixedTouchField;

    // Reference to the camera look script for controlling camera movement
    public CameraLook _CameraLook;

    // Update is called once per frame
    void Update()
    {
        // Set camera look lock axis based on touch distance
        _CameraLook.LockAxis = _FixedTouchField.TouchDist;
    }
}
