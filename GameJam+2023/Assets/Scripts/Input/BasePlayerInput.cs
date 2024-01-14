using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlayerInput : MonoBehaviour
{
    int inputDisable = 0;

    protected virtual void Start()
    {
        EventManager.onDisableInputEvent += InputDisable;
    }

    protected virtual void OnDestroy()
    {
        EventManager.onDisableInputEvent -= InputDisable;
    }

    protected bool IsInputEnable()
    {
        return inputDisable <= 0;
    }

    void InputDisable(bool disableInput)
    {
        if (disableInput)
            inputDisable++;
        else
            inputDisable--;
    }
}