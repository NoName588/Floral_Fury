using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float speed = 50f; 
    public float rotationSpeed = 100f; 
    public float jumpForce = 8f; 

    private Rigidbody rb;

    private bool isGrounded;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            Debug.Log(Gamepad.all[i].name);
        }

  
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        movement = movement.normalized * speed * Time.deltaTime;

        rb.MovePosition(transform.position + transform.TransformDirection(movement));

        float rotation = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, rotation, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }
}
