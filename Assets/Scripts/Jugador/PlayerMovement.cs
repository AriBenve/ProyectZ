using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool _readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool _grounded;

    public Transform orientation;

    float _horizontalInput;
    float _verticalInput;

    Vector3 _moveDirection;

    Rigidbody _rb;

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

        if (_grounded)
            _rb.drag = groundDrag;
        else
            _rb.drag = 0;
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

    private void _MovePlayer()
    {
        _moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;

        if(_grounded)
            _rb.AddForce(_moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if(!_grounded)
            _rb.AddForce(_moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void _SpeedControl()
    {
        Vector3 _flatVelocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

        if(_flatVelocity.magnitude > moveSpeed)
        {
            Vector3 _limitedVelocity = _flatVelocity.normalized * moveSpeed;
            _rb.velocity = new Vector3(_limitedVelocity.x, _rb.velocity.y, _limitedVelocity.z);
        }
    }

    private void _Jump()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

        _rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void _ResetJump()
    {
        _readyToJump = true;
    }
}
