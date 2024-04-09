using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [HideInInspector] public Movement movement;
    public void Awake()
    {
       if( Instance == null)
        {
            Instance = this;
        }
        
        movement = new Movement();
     }

    private void OnEnable()
    {
       movement.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
    }
}
