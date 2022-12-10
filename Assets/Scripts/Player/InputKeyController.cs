using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputKeyController : MonoBehaviour
{
    [SerializeField] private KeyCode keycode;

    public UnityEvent onKeyPressed;
    public UnityEvent onKeyReleased;
    public UnityEvent onKeyHold;

    private void Update()
    {
        if (Input.GetKeyDown(keycode))
        {
            onKeyPressed?.Invoke();
        }

        if (Input.GetKeyUp(keycode))
        {
            onKeyReleased?.Invoke();
        }

        if (Input.GetKey(keycode))
        {
            onKeyHold?.Invoke();
        }
    }
}
