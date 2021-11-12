using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EVariable<T> : MonoBehaviour
{
    private T _value;
    public event System.Action<T> OnChangeEV;
    public T Value {get => _value; 
    set{
        OnChangeEV?.Invoke(value);
        _value = value;
    }}

}
