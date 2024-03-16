using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FollowCamLogic : MonoBehaviour
{

    private CinemachineFreeLook _camera;
    private Transform _playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<CinemachineFreeLook>();
        _playerTransform = GameObject.FindWithTag("Player").transform;

        if(_playerTransform == null)
        {
            Debug.LogError("No player in current scene.");
        }
        else
        {
            _camera.Follow = _playerTransform;
            _camera.LookAt = _playerTransform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
