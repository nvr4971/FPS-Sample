using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputMouseController : MonoBehaviour
{
    [SerializeField] UnityEvent<Vector3> mousePositionChanged;
    [SerializeField] UnityEvent<Vector3> mousePositionUpdate;

    [SerializeField] UnityEvent leftMouseClicked;
    [SerializeField] UnityEvent rightMouseClicked;
    [SerializeField] UnityEvent leftMouseHold;

    private Vector3 lastMousePosition;

    private void Update()
    {
        mousePositionUpdate?.Invoke(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));

        if (lastMousePosition != Input.mousePosition)
        {
            lastMousePosition = Input.mousePosition;
            mousePositionChanged?.Invoke(lastMousePosition);
        }

        if (Input.GetMouseButtonDown(0))
        {
            leftMouseClicked?.Invoke();
        }

        if (Input.GetMouseButtonDown(1))
        {
            rightMouseClicked?.Invoke();
        }

        if (Input.GetMouseButton(0))
        {
            leftMouseHold?.Invoke();
        }
    }
}
