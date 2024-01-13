using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlayerInput : MonoBehaviour
{
    int inputDisable = 0;

    protected virtual void Start()
    {
        EventManager.onDisableInputEvent += AddInputDisable;
        EventManager.onEnableInputEvent += DecInputDisable;
    }

    protected virtual void OnDestroy()
    {
        EventManager.onDisableInputEvent -= AddInputDisable;
        EventManager.onEnableInputEvent -= DecInputDisable;
    }

    protected bool IsInputEnable()
    {
        return inputDisable <= 0;
    }

    void AddInputDisable()
    {
        inputDisable++;
    }

    void DecInputDisable()
    {
        inputDisable--;
    }
}