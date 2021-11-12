using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector2do : Todo
{
    public List<Vector2> targets;
    protected int _currentTarget = 0;
    public System.Func<Vector2> Getter;
    public float epsilon = 0.001f;


    public override bool CheckForCompletion()
    {
        var finalV = targets[_currentTarget] - Getter();
        return Mathf.Abs(finalV.x) < epsilon && Mathf.Abs(finalV.y) < epsilon;
    }


    protected override void FinishStep()
    {
        _currentTarget ++ ;
        if(_currentTarget < targets.Count){
           return;
        }
        else{
            base.FinishStep();
        }
    }

    protected override void Reactivate()
    {
        base.Reactivate();
        _currentTarget--;
        this.GetComponent<SpriteRenderer>().enabled = true;
    }


    protected override void Disable()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
    }
}
