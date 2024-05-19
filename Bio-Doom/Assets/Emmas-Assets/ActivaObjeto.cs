using System;
using System.Collections;
using UnityEngine;

public class ActivaObjeto : MonoBehaviour
{
    public Animator animator;
    public GameObject objectToActivate; // Reference to the object to activate
    private float timeInterval = 10.0f; // Time interval between activations
    private float activationTime = 1.0f; // Duration of activation
    private float lastActivationTime = 0.0f; // Time of the last activation

    public float timer;
    void Start()
    {
        animator.SetTrigger("Stay"); // Set initial state to "Stay"
        objectToActivate.SetActive(false); // Ensure object is initially inactive
    }

    void Update()
    {
        if (Time.time - lastActivationTime >= timeInterval)
        {
            StartCoroutine(ActivateWithDelay());
            lastActivationTime = Time.time;
        }
    }

    IEnumerator ActivateWithDelay()
    {
        animator.SetTrigger("Pump"); // Trigger animation first
        yield return new WaitForSeconds(timer); // Wait for 1 second
        objectToActivate.SetActive(true); // Activate object after delay

        // Optionally deactivate after activationTime (consider animation duration):
        yield return new WaitForSeconds(activationTime);
        objectToActivate.SetActive(false);
        animator.SetTrigger("Stay"); // Reset animation state
    }
}


