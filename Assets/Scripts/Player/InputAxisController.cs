using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputAxisController : MonoBehaviour
{
    [SerializeField] private string axisName;

    public UnityEvent<float> onAxisChanged;

    private float lastAxisValue;

    private void Update()
    {
        float newAxisValue = Input.GetAxis(axisName);
        onAxisChanged?.Invoke(newAxisValue);
    }
}
