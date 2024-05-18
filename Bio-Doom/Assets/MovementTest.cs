using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementTest : MonoBehaviour
{
    public float movementForce = 10;
    private Vector3 moveDirection;
    private Rigidbody rgbd;
    private PlayerInput playerInput;

    void Start()
    {
        rgbd = GetComponentInChildren<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        moveDirection = playerInput.actions["Move"].ReadValue<Vector2>();
        moveDirection.z = moveDirection.y;
        moveDirection.y = 0f;
        rgbd.AddForce(moveDirection * movementForce);
    }

    public void Jump()
    {

    }
}
