using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


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
    public GameObject Sword;
    public GameObject Vain;

    private Animator Si;

    private Rigidbody rb;

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private Vector2 currentCameraRotation;

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
            MovePress = currentrotate.x != 0 || currentrotate.y != 0;
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
        Vain.SetActive(false);
        Sword.SetActive(false);
        rb = GetComponent<Rigidbody>();

        Si = GetComponent<Animator>();
        Si.SetTrigger("Idle");

    }
    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        

        if (L_Press && !R_Press)
        {
            Attacking_L();
            Si.SetTrigger("L");
        }
        else if (!L_Press && R_Press)
        {
            Attacking_R();
            Si.SetTrigger("R");
        }
        else if (L_Press && R_Press)
        {
            Debug.Log("ataque fuerte");
            Si.SetTrigger("Smash");
        }

        else if (!L_Press && !R_Press && !RunPress && !MovePress)
        {
            Si.SetTrigger("Idle");
            Si.ResetTrigger("Run");
            Si.ResetTrigger("Walk");
        }

        if (currentCameraRotation.sqrMagnitude > 0.1f)
        {
            // Convertir la entrada del joystick a una rotación
            Vector3 cameraRotation = new Vector3(currentCameraRotation.y, currentCameraRotation.x, 0);

            // Aplicar la rotación a la cámara Cinemachine
            cinemachineVirtualCamera.transform.Rotate(cameraRotation * Time.deltaTime * 5f);
        }

    }

    void HandleMovement()
    {
        // Determine movement direction
        Vector3 movement = CalculateMovement(currentmovement);

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
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), Time.deltaTime * 10f);
    }

    Vector3 CalculateMovement(Vector2 currentMovement)
    {
        return transform.right * currentMovement.x + transform.forward * currentMovement.y;
    }


    public void Attacking_L()
    {
        IsAttack = false;

            Sword.SetActive(true);
        

        StartCoroutine(ResetAttack());
    }

    public void Attacking_R()
    {
        IsAttack = false;

        Vain.SetActive(true);
    

        StartCoroutine(ResetAttack());
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(Cooldown);
        IsAttack = true;
        SwordA = false;
        Sword.SetActive(false);
        Vain.SetActive(false);
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

