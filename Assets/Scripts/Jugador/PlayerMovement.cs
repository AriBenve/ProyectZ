using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float sprintSpeed;
    public float walkSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool _readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool _grounded;

    [Header("Slope Handling")]
    public float MaxSlopeAngle;
    private RaycastHit _slopeHit;
    bool _exitingSlope;

    public Transform orientation;

    float _horizontalInput;
    float _verticalInput;

    Vector3 _moveDirection;

    Rigidbody _rb;

    public MovementState state;

    public enum MovementState {sprinting, walking, air}

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _readyToJump = true;
    }

    private void Update()
    {
        _grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        _MyInput();
        _SpeedControl();
        _StateHandler();

        if (_grounded)
            _rb.drag = groundDrag;
        else
            _rb.drag = 0;

        Debug.Log(_OnSlope());
    }

    private void FixedUpdate()
    {
        _MovePlayer();
    }

    private void _MyInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && _readyToJump && _grounded)
        {
            _readyToJump = false;

            _Jump();

            Invoke(nameof(_ResetJump), jumpCooldown);
        }
    }

    private void _StateHandler()
    {
        if(_grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        else if(_grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else
        {
            state= MovementState.air;
        }
    }

    private void _MovePlayer()
    {
        _moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;

        //Slope movement
        if (_OnSlope() && !_exitingSlope)
        {
            _rb.AddForce(_GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);
            
            if(_rb.velocity.y > 0)
            {
                _rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }
        //Ground Drag
        if(_grounded)
            _rb.AddForce(_moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        //In Air
        else if(!_grounded)
            _rb.AddForce(_moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        
        //Turn gravity off while on a slope
        _rb.useGravity = !_OnSlope();
    }

    private void _SpeedControl()
    {
        if(_OnSlope() && !_exitingSlope)
        {
            if (_rb.velocity.magnitude > moveSpeed)
                _rb.velocity = _rb.velocity.normalized * moveSpeed;
        }
        else
        {
            Vector3 _flatVelocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

            if (_flatVelocity.magnitude > moveSpeed)
            {
                Vector3 _limitedVelocity = _flatVelocity.normalized * moveSpeed;
                _rb.velocity = new Vector3(_limitedVelocity.x, _rb.velocity.y, _limitedVelocity.z);
            }
        }
        
    }

    private void _Jump()
    {
        _exitingSlope = true;

        _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

        _rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void _ResetJump()
    {
        _readyToJump = true;

        _exitingSlope = false;
    }

    private bool _OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out _slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            return angle < MaxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 _GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(_moveDirection, _slopeHit.normal).normalized;
    }
}
