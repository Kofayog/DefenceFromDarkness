using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericVariable<T> : ScriptableObject, ISerializationCallbackReceiver
{
    public event Action<T> OnRuntimeValueChanged;
    [SerializeField] private T initialValue;
    [NonSerialized] private T runtimeValue;

    public T InitialValue
    {
        get
        {
            return initialValue;
        }
        set
        {
            initialValue = value;
        }
    }
    public T RuntimeValue
    {
        get
        {
            return runtimeValue;
        }
        set
        {
            runtimeValue = value;
            OnRuntimeValueChanged?.Invoke(value);
        }
    }

    public void OnAfterDeserialize()
    {
        RuntimeValue = InitialValue;
    }

    public void OnBeforeSerialize()
    {

    }
}
