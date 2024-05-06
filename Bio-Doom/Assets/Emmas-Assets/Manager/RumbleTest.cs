using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumbleTest : MonoBehaviour
{
    public float rumbleIntensity1 = 0.25f; // Intensity for left motor (0 to 1)
    public float rumbleIntensity2 = 1.0f;
    public float rumbleIntensity3 = 0.0f;
    // Intensity for right motor (0 to 1)

    private float rumbleStartTime; // Timestamp when rumble started

    private void OnTriggerEnter(Collider other)
    {
        // Check if colliding object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Start rumble and record start time
            RumbleManager.Instance.Rumble(rumbleIntensity1, rumbleIntensity2);
            rumbleStartTime = Time.time;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if exiting object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Calculate rumble duration and stop rumble
            float rumbleDuration = Time.time - rumbleStartTime;
            RumbleManager.Instance.StopRumble(rumbleIntensity3, rumbleIntensity3);
        }
    }
}




