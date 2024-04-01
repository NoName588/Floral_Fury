using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanvaCtl : MonoBehaviour
{
    //public GameObject objectToDeactivate;
    Movement input;
    bool InteractionX;

    private void Awake()
    {
        input = new Movement();

        input.CharacterControl.Interact.performed += ctx => InteractionX = ctx.ReadValueAsButton();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InteractionX)
        {
            Activator();
        }

    }

    void Activator()
    {
        //objectToDeactivate.SetActive(false);
    }

    private void OnEnable()
    {
        input.CharacterControl.Enable();
    }

    private void OnDisable()
    {
        input.CharacterControl.Disable();
    }
}
