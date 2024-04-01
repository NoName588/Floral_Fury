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
    bool RotatePress;
    bool R_Press;
    bool L_Press;
    public float jumpForce = 8f;
    bool isGrounded = false;

    public Animator swordAnimator;
    public Animator VainCheck;
    public bool IsAttack = true;
    public float Cooldown = 1.0f;
    public bool SwordA = false;
    public GameObject Sword;
    public GameObject Vain;

    private Animator Si;

    private Rigidbody rb;

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
            currentrotate = ctx.ReadValue<Vector2>();
            MovePress = currentrotate.x != 0 || currentrotate.y != 0;
        };

        input.CharacterControl.Run.performed += ctx => RunPress = ctx.ReadValueAsButton();
        input.CharacterControl.L_Attack.performed += ctx => L_Press = ctx.ReadValueAsButton();
        input.CharacterControl.R_Attack.performed += ctx => R_Press = ctx.ReadValueAsButton();
        input.CharacterControl.C1.performed += ctx => Change1 = ctx.ReadValueAsButton();
        input.CharacterControl.C2.performed += ctx => Change2 = ctx.ReadValueAsButton();

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
        handleRotation();

        if (L_Press && !R_Press)
        {
            Attacking_L();
        }
        else if (!L_Press && R_Press)
        {
            Attacking_R();
        }
        else if (L_Press && R_Press)
        {
            Debug.Log("ataque fuerte");
        }

    }

    void handleRotation()
    {
        Vector3 currentPosition = transform.position;

        Vector3 newPosition = new Vector3(currentrotate.x, 0, currentrotate.y);

        Vector3 positionLookAt = currentPosition + newPosition;

        transform.LookAt(positionLookAt);
    }

    void HandleMovement()
    {

        if (MovePress)
        {
            Si.SetTrigger("Walk");

            float moveSpeed = RunPress ? 20.0f : 5.0f; // Adjust movement speed based on RunPress

            Vector3 movement = transform.right * currentmovement.x * moveSpeed + transform.forward * currentmovement.y * moveSpeed;
            rb.MovePosition(rb.position + movement * Time.deltaTime);
        }
    }

    public void Attacking_L()
    {
        IsAttack = false;

        // Verifica si el animator está asignado
        if (swordAnimator != null)
        {
            Sword.SetActive(true);
            SwordA = true;

        }

        StartCoroutine(ResetAttack());
    }

    public void Attacking_R()
    {
        IsAttack = false;

        // Verifica si el animator está asignado
        if (swordAnimator != null)
        {
            Vain.SetActive(true);
            SwordA = true;

        }

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

