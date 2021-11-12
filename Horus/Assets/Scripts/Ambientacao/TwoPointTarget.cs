using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RenderMode
{
    TwoPointsInLine,
    Single2DPoint
    
}

public class TwoPointTarget : FloatToDo
{
    public Line l1;
    public Line l2;
    public Transform p1;
    public PointEV Point1;
    public Transform p2;
    public PointEV Point2;

    [Range(0f,1f)]
    public float Factor1;
    [Range(0f,1f)]
    public float Factor2;



    public FloatToDo TodoOne;
    public FloatToDo TodoTwo;



    private void Start() {

        TodoOne =  this.gameObject.AddComponent<FloatToDo>();
        TodoTwo =  this.gameObject.AddComponent<FloatToDo>();
        

        // var direction = Ax.initial - Ax.final;
        // transform.position = Vector2.Lerp(Ax.initial, Ax.final, target);
    }

    private void Setup(){
        TodoOne.target = Factor1;
        TodoTwo.target = Factor2;
        TodoOne.Getter = () => Point1.controller.Value;
        TodoTwo.Getter = () => Point2.controller.Value;
        TodoOne.OnCompleteGoal += WaitForIt;
        
    }

    private void WaitForIt()
    {
        throw new NotImplementedException();
    }

    private void FixedUpdate() {
        p1.position = l1.ProjectVector2OnLine(TodoOne.target);
        p2.position = l2.ProjectVector2OnLine(TodoTwo.target);
    }




}
