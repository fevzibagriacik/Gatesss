using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    //public InputAction action;
    public PlayerInput action;

    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float crouch_jumpTime;
    
    public float sprintSpeed;

    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;
    [SerializeField] Transform camera;
    private bool CrouchToJumpBool;
    float timer = 0f;
    public float dashTime;
    public float dashLength;
    public float DashPower;




    Vector3 moveDirection= Vector2.zero;

    Rigidbody rb;

    public MovementState state;
    private bool dashing=false;

    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        startYScale = transform.localScale.y;
        //Debug.Log("scriptIsWorking");

        state = MovementState.crouching;
        moveSpeed = crouchSpeed;
    }

    private void Update()
    {
        // ground check
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit ,playerHeight * 0.5f + 0.2f, whatIsGround))
        {
            grounded=true;
        }
        //Debug.Log(hit + "");
        MyInput();
        SpeedControl();
        StateHandler();
        CrouchToJump();

        // handle drag
        if (grounded) { 
            //Debug.Log("GroundDetected");
        rb.drag = groundDrag; }
        else
            rb.drag = 0;

        if (Input.GetKeyDown(KeyCode.F))
        {
            dashing = true;

        }
        Dash();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        
        

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            CrouchToJumpBool = true; 

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // start crouch
        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        // stop crouch
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }
    void OnMove(InputValue value)
    {
        /*Vector2 inputVector = value.Get<Vector2>();
        
        moveDirection = new Vector3(inputVector.y+camera.rotation.x  ,  0, -inputVector.x+camera.rotation.y);*/
        Vector2 inputVector = value.Get<Vector2>();

        // Kameranýn ileri (forward) ve saða (right) vektörlerini alarak hareket yönünü hesapla
        float x_axis =camera.GetComponent<CinemachineFreeLook>().m_XAxis.Value;
        float y_Axis = camera.GetComponent<CinemachineFreeLook>().m_YAxis.Value;

        // Y ekseni (yukarý-aþaðý) hareketinde kamerayý iptal et
        

        // Hareket yönünü normalize edip input deðerleri ile çarp
        

        moveDirection =new Vector3(x_axis+inputVector.x,0f,x_axis+inputVector.y);
    }

    private void StateHandler()
    {
        // Mode - Crouching
        if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }

        // Mode - Sprinting
        else if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        // Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        // Mode - Air
        else
        {
            state = MovementState.air;
            //Debug.Log("on Air");
        }
    }
    /*
    private void MovePlayer()
    {
        // calculate movement direction
        //Debug.Log("MovePlayer()");

        // on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        // on ground
        else if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        // turn gravity off while on slope
        rb.useGravity = !OnSlope();
    }*/
    /*
    private void MovePlayer()
    {
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }
        else if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        rb.useGravity = !OnSlope();
    }*/
    private void MovePlayer()
    {
        if (moveDirection != Vector3.zero) // Sadece hareket giriþi varsa kuvvet uygula
        {
            if (OnSlope() && !exitingSlope)
            {
                rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

                if (rb.velocity.y > 0)
                    rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
            else if (grounded)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            }
            else if (!grounded)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            }
        }

        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        // limiting speed on slope
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        // limiting speed on ground or in air
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }
    private void CrouchToJump()
    {
        
        if (CrouchToJumpBool)
        {
            Debug.Log("CrouchtoJumpBool");
            state = MovementState.crouching;
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            if (timer > crouch_jumpTime)
            {
                CrouchToJumpBool = false;
                timer = 0f;
                transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
                Jump();
                
            }
            else
            {
                timer += Time.deltaTime;
            }
            
        }
    }

    private void Jump()
    {

        Debug.Log("jump");
        state = MovementState.air;
        exitingSlope = true;

        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }
    private void Dash()
    {
        if (dashing == true)
        {
            if (timer < dashTime)
            {
                rb.useGravity = false;
                timer += Time.deltaTime;
                transform.position = new Vector3(transform.position.x, 0f,transform.position.z);
                
                rb.AddForce(transform.forward*DashPower,ForceMode.Force);
            }
            else
            {
                timer = 0;
                dashing = false;
                rb.useGravity = true;
            }
        }
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}
