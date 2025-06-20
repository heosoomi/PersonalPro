using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TycoonInputSystem;

public class InputControl : MonoBehaviour
{
    [HideInInspector] public TycoonInput tycoonInput;

    void Awake()
    {
        tycoonInput = new TycoonInput();

    }

    void OnEnable()
    {
        tycoonInput.Enable();
    }
    void OnDisable()
    {
        tycoonInput.Disable();
    }
    void OnDestroy()
    {
        tycoonInput.Dispose();
    }
}
