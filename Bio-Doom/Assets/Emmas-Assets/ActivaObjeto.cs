using System;
using UnityEngine;

public class ActivaObjeto : MonoBehaviour
{

    public GameObject objectToActivate; // Reference to the object to activate
    private float timeInterval = 10.0f; // Time interval between activations
    private float activationTime = 1.0f; // Duration of activation
    private float lastActivationTime = 0.0f; // Time of the last activation
    private void Start()
    {
        objectToActivate.SetActive(false);
    }
    void Update()
    {
        if (Time.time - lastActivationTime >= timeInterval)
        {
            objectToActivate.SetActive(true);
            Invoke("DeactivateObject", activationTime);
            lastActivationTime = Time.time;
        }
    }

    void DeactivateObject()
    {
        objectToActivate.SetActive(false);
    }
}

