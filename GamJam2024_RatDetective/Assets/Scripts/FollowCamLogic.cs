using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FollowCamLogic : MonoBehaviour
{
    private CinemachineFreeLook _camera;
    private Transform _orientation;
    private Transform _playerTransform;
    private Transform _playerObj;
    private Rigidbody _playerRigidBody;

    public float rotationSpeed;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _camera = GetComponent<CinemachineFreeLook>();
        // _playerRigidBody = _playerTransform.gameObject.GetComponent<Rigidbody>();
        _playerTransform = GameObject.FindWithTag("Player").transform;

        if(_playerTransform == null)
        {
            Debug.LogError("No player in current scene.");
        }
        else
        {
            _camera.Follow = _playerTransform;
            _camera.LookAt = _playerTransform;

            _orientation = GameObject.FindWithTag("Orientation").transform;
            _playerObj = GameObject.FindWithTag("PlayerObj").transform;
        }
    }

    private void Update() 
    {
        Vector3 viewDir = _playerTransform.position - new Vector3(transform.position.x, _playerTransform.position.y, transform.position.z);
        _orientation.forward = viewDir.normalized;

        // Rotate player object
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = _orientation.forward * verticalInput + _orientation.right * horizontalInput;

        // Apply initial rotation to fix the rotation issue
        Quaternion initialRotation = Quaternion.Euler(0f, 90f, 0f);

        if(inputDir != Vector3.zero)
        {
            // Apply initial rotation to the input direction
            inputDir = initialRotation * inputDir;

            // Use Quaternion.LookRotation instead of Vector3.Slerp to directly set rotation
            Quaternion targetRotation = Quaternion.LookRotation(inputDir.normalized, _orientation.up);
            _playerObj.rotation = Quaternion.Slerp(_playerObj.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
