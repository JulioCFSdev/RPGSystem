using System;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private const float MinFollowYOffset = 2f;
    private const float MaxFollowYOffset = 12f;
    
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float zoomAmount = 1f;
    
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineTransposer _cinemachineTransposer;
    private Vector3 _targetFollowOffSet;

    private void Start()
    {
        _cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        _targetFollowOffSet = _cinemachineTransposer.m_FollowOffset;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    private void HandleMovement()
    {
        // Movement Camera WASD
        Vector3 inputMoveDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDir.z = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDir.z = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x = +1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x = -1f;
        }

        Vector3 moveVector3 = (transform.forward * inputMoveDir.z) + (transform.right * inputMoveDir.x);
        transform.position += moveVector3 * (moveSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        // Rotation Camera QE
        Vector3 rotationVector = new Vector3(0, 0, 0);
        
        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = +1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -1f;
        }

        transform.eulerAngles += rotationVector * (rotationSpeed * Time.deltaTime);
    }

    private void HandleZoom()
    {
        // Zoom Camera Scroll Mouse
        if (Input.mouseScrollDelta.y > 0)
        {
            _targetFollowOffSet.y -= zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            _targetFollowOffSet.y += zoomAmount;
        }

        _targetFollowOffSet.y = Mathf.Clamp(_targetFollowOffSet.y, MinFollowYOffset, MaxFollowYOffset);
        // Smoothing of zoom
        _cinemachineTransposer.m_FollowOffset = Vector3.Lerp(_cinemachineTransposer.m_FollowOffset, _targetFollowOffSet, Time.deltaTime * zoomSpeed);
    }
}
