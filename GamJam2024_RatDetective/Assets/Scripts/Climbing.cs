using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Rigidbody _rigidBody;
    public LayerMask whatIsClimbable;
    public ThirdPersonMovement tpm;

    private Animator _animator;

    [Header("Climbing")]
    public float climbSpeed;
    public float maxClimbTime;
    private float _climbTimer;

    private bool _climbing;

    [Header("Detection")]
    public float detectionLenght;
    public float sphereCastRadius;
    public float maxWallLookAngle;
    private float _wallLookAngle;

    private RaycastHit frontWallHit;
    private bool _wallHit;

    private void Start() 
    {
        _animator = GetComponentInChildren<Animator>();  
        if(_animator == null)
        {
            Debug.LogError("Can't find animator component");
        }  
    }

    private void Update() 
    {
        Debug.Log(_climbTimer);
        WallCheck();
        StateMachine();

        if(_climbing) ClimbingMovement();
    }

    private void StateMachine()
    {
        //State 1 - Climbing
        if(_wallHit && Input.GetKey(KeyCode.W) && _wallLookAngle < maxWallLookAngle)
        {
            if(!_climbing && _climbTimer > 0) StartClimbing();

            if(_climbTimer > 0) _climbTimer -= Time.deltaTime;
            if(_climbTimer < 0) StopClimbing();
        }

        // State 3 - none
        else
        {
            if(_climbing) StopClimbing();
        }
    }

    private void WallCheck()
    {
        _wallHit = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLenght, whatIsClimbable);
        _wallLookAngle = Vector3.Angle(orientation.forward, -frontWallHit.normal);

        if(!_climbing && tpm.Grounded)
        {
            _climbTimer = maxClimbTime;
        }
    }

    private void StartClimbing()
    {
        _climbing = true;
        _animator.SetBool("Climbing", _climbing);
        tpm.Grounded = false;
    }

    private void StopClimbing()
    {
        if(_climbing)
        {
            _climbing = false;
            _animator.SetBool("Climbing", _climbing);

        }
    }

    private void ClimbingMovement()
    {
        _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, climbSpeed, _rigidBody.velocity.z);
    }
}
