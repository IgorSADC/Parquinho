using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTarget : FloatToDo
{
    public Line Ax;
    public PointEV Point;

    public List<Vector2> Positions;
    private int currentPos = 0;
    private void Start() {

        Getter = () => Point.controller.Value;
        // var direction = Ax.initial - Ax.final;
        // transform.position = Vector2.Lerp(Ax.initial, Ax.final, target);
    }

    protected override void FinishStep(){
        base.FinishStep();

        if(currentPos >= Positions.Count){
            return;

        }

        Ax.final = Positions[currentPos];
        Ax.GenerateLine();

        currentPos++;

        // var direction = Ax.initial - Ax.final;
        // transform.position = Vector2.Lerp(Ax.initial, Ax.final, target);
        // currentPos++;
    }

    protected  void FixedUpdate() {
        // var direction = Ax.transform.right;
        // var initial = Ax.transform.position - Ax.transform.localScale.x/2 * direction;

        
        // var final =Ax.transform.position + Ax.transform.localScale.x/2 * direction; 
        transform.position = Ax.ProjectVector2OnLine(target);
        
        
    }
}
