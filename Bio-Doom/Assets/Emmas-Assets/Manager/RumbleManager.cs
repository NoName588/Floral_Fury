using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RumbleManager : MonoBehaviour
{
    public static RumbleManager Instance { get; private set; } // Singleton with proper access

    private Gamepad pad;
    private Coroutine rumbleCoroutine; // More descriptive name

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensure only one RumbleManager exists
        }
    }

    public void Rumble(float lowFrequency, float highFrequency, float duration = 0.3f) // Combined function with optional duration
    {
        pad = Gamepad.current;

        if (pad != null)
        {
            pad.SetMotorSpeeds(lowFrequency, highFrequency);

            // Stop any previous rumble coroutine before starting a new one
            if (rumbleCoroutine != null)
            {
                StopCoroutine(rumbleCoroutine);
            }

            rumbleCoroutine = StartCoroutine(StopRumbleAfter(duration, pad));
        }
    }

    private IEnumerator StopRumbleAfter(float duration, Gamepad pad)
    {
        yield return new WaitForSeconds(duration); // Wait for specified duration
        pad.SetMotorSpeeds(0f, 0f);
    }
}
