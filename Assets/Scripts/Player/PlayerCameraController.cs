using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Transform cam;
    [SerializeField] private bool lockCursor;
    [SerializeField] [Range(0.01f, 10)] private float lookSensitivity;
    [SerializeField] private float maxUpRotation;
    [SerializeField] private float maxDownRotation;
    [SerializeField] private Transform targetPosition;
    [SerializeField] private Transform lookatTarget;

    [SerializeField] Vector3 positionOffset;

    private void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        //cam.LookAt(lookatTarget);
        //cam.position = targetPosition.position + positionOffset;
        //cam.rotation = targetPosition.rotation;
    }

    // Camera
    public void Look(Vector3 mousePosition)
    {
        transform.Rotate(0, mousePosition.x * lookSensitivity, 0);
        //xRotation -= mousePosition.y * lookSensitivity;
        //xRotation = Mathf.Clamp(xRotation, -maxUpRotation, maxDownRotation);
        //cam.localRotation = Quaternion.Euler(xRotation, 0, 0);
        Vector3 newTargetPosition = lookatTarget.localPosition + (lookSensitivity * mousePosition.y * Vector3.up);
        //Debug.Log("(Vector3.up * xRotation): " + (Vector3.up * xRotation))
        lookatTarget.localPosition = newTargetPosition;
    }
}
