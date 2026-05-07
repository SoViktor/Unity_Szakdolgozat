using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField]private float cameraMoveSpeed;
    [SerializeField]private float cameraMoveSpeedFast; 
    [SerializeField]private float cameraRotationSpeed;

    [SerializeField] private CinemachineCamera cinemachineCamera;

    private CinemachinePositionComposer positionComposer;

    [SerializeField] private float zoomSpeed;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;

    private float snapAngle = 90f;
    private float snapThreshold = 5f;
    private bool isUnitChanged = false;

    private void Start()
    {
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;

        if (cinemachineCamera != null)
        {
            positionComposer = cinemachineCamera.GetComponent<CinemachinePositionComposer>();    
        }
        else
        {
            Debug.LogError("cinemachineCamera Not Found");
        }

    }
    private void Update()
    {
        if (isUnitChanged)
        {
            HandleCameraMovementOnUnitChange();
        }
        else
        {
        HandleCameraMovement();            
        }
        HandleCameraRotation();
        HandleCameraZoom();

    }

    private void HandleCameraMovement()
    {
        Vector3 inputMoveDirection = Vector3.zero;

        if (Keyboard.current.wKey.isPressed) inputMoveDirection.z = +1f;
        if (Keyboard.current.sKey.isPressed) inputMoveDirection.z = -1f;
        if (Keyboard.current.dKey.isPressed) inputMoveDirection.x = +1f;
        if (Keyboard.current.aKey.isPressed) inputMoveDirection.x = -1f;

        Vector3 cameraMoveVector =
            transform.forward * inputMoveDirection.z +
            transform.right * inputMoveDirection.x;

        cameraMoveVector.y = 0f;
        cameraMoveVector.Normalize();

        transform.position += cameraMoveVector * cameraMoveSpeed * Time.deltaTime;
    }

    private void HandleCameraRotation()
    {
        Vector3 cameraRotationVector = new Vector3(0,0,0);

        if (Keyboard.current.qKey.isPressed)
        {
            cameraRotationVector.y = +1f;
        }
        if (Keyboard.current.eKey.isPressed)
        {
            cameraRotationVector.y = -1;
        }

        transform.eulerAngles += cameraRotationVector * cameraRotationSpeed * Time.deltaTime;

        if (cameraRotationVector.y == 0f)
        {
            

            Vector3 currentRotation = transform.eulerAngles;

            float nearestSnapAngle = Mathf.Round(currentRotation.y / snapAngle) * snapAngle;
            float difference = Mathf.Abs(Mathf.DeltaAngle(currentRotation.y, nearestSnapAngle));

            if (difference < snapThreshold)
            {
                currentRotation.y = nearestSnapAngle;
                transform.eulerAngles = currentRotation;
            }
        }
    }

    private void HandleCameraZoom()
    {
        float scrollValue = Mouse.current.scroll.ReadValue().y;

        if (positionComposer == null)
        {
            return;
        }

        positionComposer.CameraDistance -= scrollValue * zoomSpeed * Time.deltaTime;

        positionComposer.CameraDistance = Mathf.Clamp(positionComposer.CameraDistance, minZoom, maxZoom);
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        isUnitChanged = true;

    }
    private void HandleCameraMovementOnUnitChange()
    {
        Unit activeUnit = TurnSystem.Instance.GetActiveUnit();
        Vector3 activeUnitPosition = activeUnit.GetWorldPosition();

        float stoppingDistance = 0.5f;
        Vector3 moveDirection = (activeUnitPosition - transform.position).normalized;
        if (Vector3.Distance(transform.position, activeUnitPosition) > stoppingDistance)
        {
        transform.position += moveDirection * Time.deltaTime * cameraMoveSpeedFast;    
            
        } else
        {
            isUnitChanged = false;
        }
    }

    private void OnDestroy() 
    {
        if (TurnSystem.Instance != null)
        {
        TurnSystem.Instance.OnTurnChanged -= TurnSystem_OnTurnChanged;
        }
    }

}
