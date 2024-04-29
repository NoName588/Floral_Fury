using Cinemachine;
using System.Collections;
using System.Xml.Linq;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerC : MonoBehaviour
{

    Movement input;

    Vector2 currentmovement;
    Vector2 currentrotate;

    bool MovePress;
    bool RunPress;
    bool Change1;
    bool Change2;
    bool R_Press;
    bool L_Press;
    public float jumpForce = 8f;
    bool isGrounded = false;

   
    public bool IsAttack = true;
    public float Cooldown = 1.0f;
    public bool SwordA = false;


    private Animator Si;

    private Rigidbody rb;

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private Vector2 currentCameraRotation;

    public bool RotPress;

    private Gamepad controller = null;
    private Transform m_transform;
    
    private Vector3 lastAcceleration;
    private float strongAccelerationThreshold = 0.2f;
    private float RotationSmoothTime;

    public float RunSpeed = 5.0f;
    public float WalkSpeed = 2.0f;
    public float AccelerationRate { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        input = new Movement();

        input.CharacterControl.Movement.performed += ctx =>
        {

            
            currentmovement = ctx.ReadValue<Vector2>();
            MovePress = currentmovement.x != 0 || currentmovement.y != 0;
            
        };

        input.CharacterControl.Rotation.performed += ctx =>
        {
            currentCameraRotation = ctx.ReadValue<Vector2>();
            RotPress = currentrotate.x != 0 || currentrotate.y != 0;
        };

        input.CharacterControl.Run.performed += ctx => RunPress = ctx.ReadValueAsButton();
        input.CharacterControl.L_Attack.performed += ctx => L_Press = ctx.ReadValueAsButton();
        input.CharacterControl.R_Attack.performed += ctx => R_Press = ctx.ReadValueAsButton();
        input.CharacterControl.C1.performed += ctx => Change1 = ctx.ReadValueAsButton();
        input.CharacterControl.C2.performed += ctx => Change2 = ctx.ReadValueAsButton();

        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    void Start()
    {

        Si = GetComponent<Animator>();
        Si.SetTrigger("Idle");

        rb = GetComponent<Rigidbody>();
       
        controller = DS4.getConroller();

    }
    // Update is called once per frame
    void Update()
    {
        HandleMovement();

        /*
        if (L_Press && !R_Press)
        {
            Attacking_L();
            Si.ResetTrigger("Idle");
            Si.SetTrigger("L");
        }
        else if (!L_Press && R_Press)
        {
            Attacking_R();
            Si.ResetTrigger("Idle");
            Si.SetTrigger("R");

        }
        else if (L_Press && R_Press)
        {
            Debug.Log("ataque fuerte");
            Si.ResetTrigger("Idle");
            Si.SetTrigger("Smash");
        }*/


        if (!L_Press && !R_Press && !RunPress && !MovePress)
        {
            Si.SetTrigger("Idle");
            Si.ResetTrigger("Run");
            Si.ResetTrigger("Walk");
            Si.ResetTrigger("R");
            Si.ResetTrigger("L");
        }

        if (currentCameraRotation.sqrMagnitude > 0.1f)
        {
            // Convertir la entrada del joystick a una rotación
            Vector3 cameraRotation = new Vector3(currentCameraRotation.y, currentCameraRotation.x, 0);

            // Aplicar la rotación a la cámara Cinemachine
            cinemachineVirtualCamera.transform.Rotate(cameraRotation * Time.deltaTime * 5f);
        }

    }

    private void FixedUpdate()
    {
        if (R_Press)
        {
            float accelerationY = rb.velocity.y;  // Access Y-axis velocity

            if (accelerationY > 0)
            {
                Debug.Log("ataque fuerte");
                Si.ResetTrigger("Idle");
                Si.SetTrigger("Smash");
                Debug.Log("SMASH");
            }
            else if (Mathf.Abs(rb.velocity.x) > 0) // Check for horizontal acceleration separately
            {
                Si.ResetTrigger("Idle");
                Si.SetTrigger("R");
                Attacking_R();
                Debug.Log("RIGHT");
            }
            else if (0 > Mathf.Abs(rb.velocity.x)) // Check for horizontal acceleration separately
            {
                Si.ResetTrigger("Idle");
                Si.SetTrigger("L");
                Attacking_L();
                Debug.Log("LEFT");
            }

        }
        else
        {
            Si.SetTrigger("Idle");
            Si.ResetTrigger("Run");
            Si.ResetTrigger("Walk");
            Si.ResetTrigger("R");
            Si.ResetTrigger("L");
        }

    }
    void HandleMovement()
    {
        // Determine movement direction
        Vector3 movement = transform.right * currentmovement.x + transform.forward * currentmovement.y;

        // Check if joystick points down (considering a threshold for accuracy)
        if (currentmovement.y < -0.5f)
        {
            // Invert the forward direction if joystick points down
            movement = -transform.forward * Mathf.Abs(movement.y);
        }

        // Set animation trigger based on movement and RunPress
        if (movement.magnitude > 0.1f)
        {
            Si.SetTrigger(RunPress ? "Run" : "Walk");
        }
        else
        {
            Si.ResetTrigger("Walk");
            Si.ResetTrigger("Run");
        }

        // Calculate speed based on RunPress
        float moveSpeed = RunPress ? 20.0f : 5.0f;

        // Apply movement with smoother rotation
        rb.MovePosition(rb.position + movement * Time.deltaTime * moveSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), Time.deltaTime * 5f);
    }





    public void Attacking_L()
    {
        IsAttack = false;


        

        StartCoroutine(ResetAttack());
    }

    public void Attacking_R()
    {
        IsAttack = false;


    

        StartCoroutine(ResetAttack());
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(Cooldown);
        IsAttack = true;
        SwordA = false;

        Si.SetTrigger("Idle");
        Si.ResetTrigger("Run");
        Si.ResetTrigger("Walk");
        Si.ResetTrigger("R");
        Si.ResetTrigger("L");

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

