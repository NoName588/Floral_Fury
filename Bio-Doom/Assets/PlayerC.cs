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

    }
    void Start()
    {
        Vain.SetActive(false);
        Sword.SetActive(false);
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        handleRotation();

        if (L_Press)
        {
            Attacking_L ();
        }
        if (R_Press)
        {
            Attacking_R();
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

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.5f);

        if (RunPress && isGrounded) // Jump only if grounded and Run is pressed
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (MovePress)
        {
            float moveSpeed = 10.0f; // Adjust this value to control movement speed
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
