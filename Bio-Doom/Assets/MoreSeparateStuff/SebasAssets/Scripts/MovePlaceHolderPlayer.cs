using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MovePlaceHolderPlayer : MonoBehaviour
{/*
    private Vector2 nextInput;
    private Vector2 currentInput;
    private PlayerInput playerInput;
    private Rigidbody playerRigid;

    private Vector2 movementDirection;

    [SerializeField]
    private GameObject thisOne;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 motionValue = context.ReadValue<Vector2>();
        float speed = 5f;
        playerRigid.AddForce(new Vector3(motionValue.x, motionValue.y) * speed, ForceMode.Force);
    }*/

    [SerializeField] private float speed = 1f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float acceleration = 4f;

    private float speedPHolder;

    private void Update()
    {
        var direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        rb.velocity = direction * speed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedPHolder = speed;
            rb.velocity = direction * speed * acceleration;
            speed = speedPHolder;
        }
    }
}
    