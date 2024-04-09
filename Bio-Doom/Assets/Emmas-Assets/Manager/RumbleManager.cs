using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RumbleManager : MonoBehaviour
{
    public static RumbleManager instance;

    private Gamepad pad;

    private Coroutine coroutine;

    private void Awake()
    {
       if (instance == null)
        {
            instance = this;
        }
    }

    public void RumblePulse(float LowFrecuency, float HighFrecuency, float duration)
    {
        pad = Gamepad.current;

        if (pad!= null)
        {
            pad.SetMotorSpeeds(LowFrecuency, HighFrecuency);

            coroutine = StartCoroutine(StopRumble(duration, pad));
        }
    }

    private IEnumerator StopRumble(float duration, Gamepad pad) 
    {
        float elapsetime = 0f;
        while (elapsetime > duration) 
        { elapsetime += Time.deltaTime; 
            yield return null;
        }

        pad.SetMotorSpeeds(0f, 0f);
    }
}
