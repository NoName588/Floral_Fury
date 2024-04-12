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
    public Animator animator;

    void Start()
    {
        animator.SetTrigger("Idle");

        rb = GetComponent<Rigidbody>();
        thisTransform = this.transform;
        controller = DS4.getConroller(); // Assuming DS4 is your gamepad class

        // Obtener la referencia al Animator
        animator = GetComponent<Animator>();
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

                // Llamar a métodos del Animator según sea necesario
                // Por ejemplo, activar una animación llamada "Left" si la aceleración es hacia la izquierda
                if (acceleration.x < 0)
                {
                    animator.SetTrigger("L");
                    animator.ResetTrigger("R");
                }
                else
                {
                    animator.SetTrigger("R");
                    animator.ResetTrigger("L");
                }

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

                // Llamar a métodos del Animator según sea necesario
                // Por ejemplo, activar una animación llamada "Up" si la aceleración es hacia arriba
                animator.SetTrigger("Smash");
                animator.ResetTrigger("R");
                animator.ResetTrigger("L");

                // Optional: Visual or audio feedback for upward object activation
            }
            else
            {
                animator.SetTrigger("Idle");
                animator.ResetTrigger("R");
                animator.ResetTrigger("L");
                animator.ResetTrigger("Smash");

            }

            lastAcceleration = acceleration;
        }
    }
}





