using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumbleTest : MonoBehaviour
{
    public float R1 = 0.25f;
    public float r2 = 10f;

    private void Update()
    {
        // Assuming InputManager.Instance.movement.Rumble.RumbleAction is a valid way to trigger rumble
        if (InputManager.Instance.movement.Rumble.RumbleAction.WasPressedThisFrame())
        {
            RumbleManager.Instance.Rumble(R1, r2); // Rumble for 0.5 seconds (default)
        }

    }
}
