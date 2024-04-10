using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    private Gamepad controller = null;
    private Transform thisTransform;
    private Rigidbody rb;
    private Vector3 lastAcceleration;
    private float strongAccelerationThreshold = 0.2f; // Adjustable threshold for strong acceleration

    public GameObject leftObject; // Reference to the left GameObject
    public GameObject rightObject; // Reference to the right GameObject
    public GameObject upObject; // Reference to the upward GameObject

    private bool spaceButtonPressed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        thisTransform = this.transform;
        controller = DS4.getConroller(); // Assuming DS4 is your gamepad class
    }

    void FixedUpdate()
    {
        spaceButtonPressed = Keyboard.current.spaceKey.isPressed; // Check for space button press

        if (controller == null)
        {
            try
            {
                controller = DS4.getConroller();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        else if (spaceButtonPressed) // Only check acceleration if space is pressed
        {
            Vector3 acceleration = DS4.getAccelerometer();

            if (Mathf.Abs(acceleration.x) > strongAccelerationThreshold)
            {
                // Activate appropriate object based on X-axis acceleration direction
                leftObject.SetActive(acceleration.x < 0); // Activate left if negative, right otherwise
                rightObject.SetActive(acceleration.x > 0);

                upObject.SetActive(false);

                // Optional: Visual feedback or sound effects (consider separate methods)
                // Provide visual or audio cues to indicate object activation
            }
            else if (Mathf.Abs(acceleration.y) > strongAccelerationThreshold)
            {
                // Deactivate left and right objects
                leftObject.SetActive(false);
                rightObject.SetActive(false);

                // Activate upward object
                upObject.SetActive(true);

                // Optional: Visual or audio feedback for upward object activation
            }
         

            lastAcceleration = acceleration;
        }
    }
}




