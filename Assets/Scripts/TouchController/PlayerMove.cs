using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls the movement of the player using a joystick input
public class PlayerMove : MonoBehaviour
{
    // Reference to the joystick for movement input
    public FixedJoystick joystick;

    // Movement speed of the player
    public float SpeedMove = 5f;

    // Reference to the CharacterController component
    public CharacterController controller;

    // Gravity value affecting the player
    public float gravity = 9.81f;

    public AudioSource footstepsAudio;

    // Boolean indicating if the player is grounded
    private bool isGrounded;

    // Vertical velocity of the player
    private float verticalVelocity;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the CharacterController component
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is grounded
        isGrounded = controller.isGrounded;

        // If grounded, reset the vertical velocity
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        // Calculate movement direction based on joystick input
        Vector3 moveDirection = transform.right * joystick.Horizontal + transform.forward * joystick.Vertical;
        moveDirection *= SpeedMove;

        // Apply gravity
        if (!isGrounded)
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        else // If grounded, ensure vertical velocity is reset
        {
            // A small negative value to ensure proper grounding
            verticalVelocity = -0.1f;
        }

        // Apply vertical velocity to move direction
        moveDirection.y = verticalVelocity;

        if (moveDirection.magnitude > 0.2)
        {
            footstepsAudio.enabled = true;
        }else{
            footstepsAudio.enabled = false;
        }

        // Move the player
        controller.Move(moveDirection * Time.deltaTime);
    }
}
