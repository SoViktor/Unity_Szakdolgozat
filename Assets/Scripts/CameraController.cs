using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField]private float cameraMoveSpeed;
    [SerializeField]private float cameraRotationSpeed;

    float snapAngle = 90f;
    float snapThreshold = 5f;
    void Update()
    {
        HandelCameraMovement();
        HandelCameraRotation();

    }

    private void HandelCameraMovement ()
    {
                Vector3 inputMoveDirection= new Vector3(0,0,0);
        if (Keyboard.current.wKey.isPressed)
        {
            inputMoveDirection.z = +1f;
        }
                if (Keyboard.current.sKey.isPressed)
        {
            inputMoveDirection.z = -1f;
        }
                if (Keyboard.current.dKey.isPressed)
        {
            inputMoveDirection.x = +1f;
        }
                if (Keyboard.current.aKey.isPressed)
        {
            inputMoveDirection.x = -1f;
        }

        Vector3 cameraMoveVector = transform.forward*inputMoveDirection.z + transform.right * inputMoveDirection.x;
        transform.position += cameraMoveVector *cameraMoveSpeed*Time.deltaTime;
        

    }

    private void HandelCameraRotation()
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

            float nearestSnapAngel = Mathf.Round(currentRotation.y / snapAngle) * snapAngle;
            float differenc = Mathf.Abs(Mathf.DeltaAngle(currentRotation.y, nearestSnapAngel));

            if (differenc < snapThreshold)
            {
                currentRotation.y = nearestSnapAngel;
                transform.eulerAngles = currentRotation;
            }
        }
    }

}
