using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatToDo : Todo
{

    [Range(0f, 100f)]
    [SerializeField] private float _target;
    public float target { get => _target; set {_target = value ;}}
    public Func<float> Getter;
    [SerializeField] private float epsilon = 0.001f;
    [SerializeField] private List<float> points;
    [SerializeField] private int currentPoint = 0;
    public override bool CheckForCompletion()
    {
        return Mathf.Abs(target - Getter()) < epsilon;
    }
    protected override void FinishStep()
    {
        currentPoint ++ ;
        if(currentPoint < points.Count){
            _target = points[currentPoint];
        }
        else{
            base.FinishStep();
        }
    }
}
