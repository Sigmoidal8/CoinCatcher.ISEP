using UnityEngine;

// Controls the camera movement for looking around
public class CameraLook : MonoBehaviour
{
    // Rotation amount around the X-axis
    private float XMove;

    // Rotation amount around the Y-axis
    private float YMove;

    // Current rotation around the X-axis
    private float XRotation;

    // Reference to the player's body transform
    [SerializeField] private Transform PlayerBody;

    // Locking axis for camera movement
    public Vector2 LockAxis;

    // Sensitivity of camera movement
    public float Sensivity = 10f;

    // Update is called once per frame
    void Update()
    {
        // Calculate rotation amounts based on mouse/touch input and sensitivity
        XMove = LockAxis.x * Sensivity * Time.deltaTime;
        YMove = LockAxis.y * Sensivity * Time.deltaTime;

        // Adjust X rotation based on Y movement and clamp it to prevent over-rotation
        XRotation -= YMove;
        XRotation = Mathf.Clamp(XRotation, -90f, 90f);

        // Apply rotation to the camera
        transform.localRotation = Quaternion.Euler(XRotation, 0, 0);
        // Rotate the player's body around the Y-axis based on X movement
        PlayerBody.Rotate(Vector3.up * XMove);
    }

     // Method to set camera rotation to a specific direction
    public void SetCameraRotation(Vector3 direction)
    {
        // Calculate rotation amounts based on mouse/touch input and sensitivity
        XMove = direction.x;
        YMove = direction.y;

        // Adjust X rotation based on Y movement and clamp it to prevent over-rotation
        XRotation -= YMove;
        XRotation = Mathf.Clamp(XRotation, -90f, 90f);

        // Apply rotation to the camera
        transform.localRotation = Quaternion.Euler(XRotation, 0, 0);
        // Rotate the player's body around the Y-axis based on X movement
        PlayerBody.Rotate(Vector3.up * XMove);
    }
}
