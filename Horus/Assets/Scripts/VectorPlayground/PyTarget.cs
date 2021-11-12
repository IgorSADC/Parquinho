using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyTarget : Vector2do
{
    public Transform targetTransform;

    // Start is called before the first frame update
    void Awake()
    {
        Getter = () => targetTransform.localPosition;
        transform.localPosition = targets[_currentTarget];

    }

    protected override void FinishStep()
    {
        base.FinishStep();
        if(_currentTarget < targets.Count)
            transform.localPosition = targets[_currentTarget];

    }
}
