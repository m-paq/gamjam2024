using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Scripting.APIUpdating;

public class ThirdPersonMovement : MonoBehaviour
{
    private Transform _mainCamera;
    private Rigidbody _rigidbody;
    private Animator _animator;

    [Header("GroundDetection")]
    public float detectionLenght;
    public float playerHeight;
    public LayerMask whatIsGround;
    public Transform orientation;

    public bool Grounded = true;

    [Header("Movement")]
    public float moveSpeed = 6f;
    public float TurnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;
    public float groundDrag;

    Vector3 moveDirection;

    [Header("JumpStats")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;

    private bool _readyToJump = true;

    private float _horizontalInput;
    private float _verticalInput;


    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        if(_animator == null)
        {
            Debug.LogError("Can't find animator component");
        }  

        _mainCamera = GameObject.FindWithTag("MainCamera").transform;
        if(_mainCamera == null)
        {
            Debug.Log("Mason, you forgot the MainCamera prefab, dumbDumb");
        }
        if(!TryGetComponent<Rigidbody>(out _rigidbody))
        {
            Debug.Log("Dylan, you forgot the rigidbody, dumbDumb");
        }
        _rigidbody.freezeRotation = true;
    }

    private void Update() 
    {
        MyInput();  
        Debug.Log(_readyToJump);
        SpeedControl();
        _rigidbody.drag = Grounded ? groundDrag : 0f;
    }

    void FixedUpdate()
    {
        CheckGrounded();
        MovePlayer();
    }

    private void CheckGrounded()
    {
        Grounded = Physics.Raycast(transform.position, -transform.up, detectionLenght, whatIsGround);
    }

    private void MyInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Space) && _readyToJump && Grounded)
        {
            _readyToJump = false;
            _animator.SetBool("Jumping", !_readyToJump);

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        Vector3 camForward = _mainCamera.forward;
        Vector3 camRight = _mainCamera.right;

        // Ignore the vertical component of the camera's forward direction
        camForward.y = 0f;
        camForward.Normalize();

        // Ignore the vertical component of the camera's right direction
        camRight.y = 0f;
        camRight.Normalize();

        moveDirection = camForward * _verticalInput + camRight * _horizontalInput;

        if (Grounded)
        {
            _rigidbody.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            if(moveDirection.normalized != Vector3.zero)
            {
                _animator.SetBool("Walking", true);
            }
            else
            {
                _animator.SetBool("Walking", false);
            }
        }
        else
        {
            _rigidbody.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }


    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);

        // limit velocity if needed
        if(flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            _rigidbody.velocity = new Vector3(limitedVelocity.x, _rigidbody.velocity.y, limitedVelocity.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);

        _rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        _readyToJump = true;
        _animator.SetBool("Jumping", !_readyToJump);

    }
}
