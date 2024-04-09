using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumbleTest : MonoBehaviour
{
    public float timer;

    private void Update()
    {
        // Assuming InputManager.Instance.movement.Rumble.RumbleAction is a valid way to trigger rumble
        if (InputManager.Instance.movement.Rumble.RumbleAction.WasPressedThisFrame())
        {
            RumbleManager.Instance.Rumble(0.25f, 1f); // Rumble for 0.5 seconds (default)
        }

    }
}
