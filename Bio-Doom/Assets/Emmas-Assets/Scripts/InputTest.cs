//InputTest.cs
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    private Gamepad controller = null;
    private Transform m_transform;
    Rigidbody rb;
    private Vector3 lastAcceleration;
    private float accelerationThreshold = 0.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        this.controller = DS4.getConroller();
        m_transform = this.transform;
    }

    void Update()
    {
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
        else
        {
            // Press circle button to reset rotation
            if (controller.buttonEast.isPressed)
            {
                m_transform.rotation = Quaternion.identity;
            }
            m_transform.rotation *= DS4.getRotation(4000 * Time.deltaTime);
            Vector3 acceleration = DS4.getAccelerometer();
            Vector3 movement = (acceleration - lastAcceleration) * 5f;

            // Limitar movimiento
            movement.x = Mathf.Clamp(movement.x, -5, 5);
            movement.y = Mathf.Clamp(movement.y, -5, 5);
            movement.z = Mathf.Clamp(movement.z, -5, 5);

            rb.AddForce(movement);
            lastAcceleration = acceleration;

            // Mostrar datos por consola
            float xAcceleration = acceleration.x;
            if (Mathf.Abs(xAcceleration) > accelerationThreshold)
            {
                string message = xAcceleration > 0 ? "Brusque acceleration to the right!" : "Brusque acceleration to the left!";
                Debug.Log(message);
            }
        }



    }
}
