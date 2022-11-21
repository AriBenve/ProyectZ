using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float sprintSpeed;
    public float walkSpeed;

    public float dashSpeed;
    public float dashSpeedChangeFactor;

    public float maxYspeed;

    public float groundDrag;

    [Header("Jump")]
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
    public bool grounded;

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

    public enum MovementState {sprinting, walking, dashing, air}

    public bool dashing;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _readyToJump = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        _MyInput();
        _SpeedControl();
        _StateHandler();

        if (state == MovementState.walking || state == MovementState.sprinting)
            _rb.drag = groundDrag;
        else
            _rb.drag = 1f;
    }

    private void FixedUpdate()
    {
        _MovePlayer();
    }

    private void _MyInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && _readyToJump && grounded)
        {
            _readyToJump = false;

            _Jump();

            Invoke(nameof(_ResetJump), jumpCooldown);
        }
    }

    private float _desiredMoveSpeed;
    private float _lastDesiredMoveSpeed;
    private MovementState _lastState;
    private bool _keepMomentum;

    private void _StateHandler()
    {
        if(dashing)
        {
            _speedChangeFactor = dashSpeedChangeFactor;
            state = MovementState.dashing;
            _desiredMoveSpeed = dashSpeed;
        }
        else if(grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            _desiredMoveSpeed = sprintSpeed;
        }
        else if(grounded)
        {
            state = MovementState.walking;
            _desiredMoveSpeed = walkSpeed;
        }
        else
        {
            state= MovementState.air;

            if (_desiredMoveSpeed < sprintSpeed)
                _desiredMoveSpeed = walkSpeed;
            else
                _desiredMoveSpeed = sprintSpeed;
        }

        bool _desiredMoveSpeedHasChange = _desiredMoveSpeed != _lastDesiredMoveSpeed;

        if(_lastState == MovementState.dashing) _keepMomentum = true;

        if(_desiredMoveSpeedHasChange)
        {
            if(_keepMomentum)
            {
                StopAllCoroutines();
                StartCoroutine(_SmoothlyLerpMoveSpeed());
            }
            else
            {
                StopAllCoroutines();
                moveSpeed = _desiredMoveSpeed;
            }
        }


        _lastDesiredMoveSpeed = _desiredMoveSpeed;
        _lastState = state;
    }

    private float _speedChangeFactor;

    private IEnumerator _SmoothlyLerpMoveSpeed()
    {
        float time = 0;
        float diference = Mathf.Abs(_desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        float boostFactor = _speedChangeFactor;

        while (time < diference)
        {
            moveSpeed = Mathf.Lerp(startValue, _desiredMoveSpeed, time / diference);

            time += Time.deltaTime * boostFactor;

            yield return null;
        }

        moveSpeed = _desiredMoveSpeed;
        _speedChangeFactor = 1f;
        _keepMomentum = false;
    }


    private void _MovePlayer()
    {
        if (state == MovementState.dashing) return;

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
        if(grounded)
            _rb.AddForce(_moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        //In Air
        else if(!grounded)
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

        if(maxYspeed != 0 && _rb.velocity.y > maxYspeed)
            _rb.velocity = new Vector3(_rb.velocity.x, maxYspeed, _rb.velocity.z);
        
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
