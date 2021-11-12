using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes2D;
using NaughtyAttributes;
using DG.Tweening;

public enum PivotMode
{
    CENTER,
    BEGINING
}

public class Line : MonoBehaviour, IAnimatable<float>
{
    private void Start() {
        if(!Animate) return;
        RegisterFunctions();
    }
    public bool Animate = true;
    public Shape shapeController;

    public Color lineColor;
    public Vector2 initial;
    public Vector2 final;

    [Range(.05f, 1f)]
    public float lineWidth = .1f;
    public PivotMode PivotMode;

    public event System.Action<IAnimatable<float>, bool, int> SaveState;


    private List<System.Tuple<System.Func<float>, System.Action<float>>> _getterSetterPairs;
    public List<System.Tuple<System.Func<float>, System.Action<float>>> GetterSetterPairs => _getterSetterPairs;

    public List<Tween> tween { get ; set ; }

    public float duration => .5f;

    public Ease AnimationEase => Ease.InSine;

    private Vector2 finalVec;

    [Button]
    public void GenerateLine()
    {
        // InitShape();
        SaveState?.Invoke(this, true, 0);
        SaveState?.Invoke(this, true, 1);
        SaveState?.Invoke(this, true, 2);
        SaveState?.Invoke(this, true, 3);

        shapeController.settings.fillColor = lineColor;

        finalVec = final - initial;
        var angle = this.GetAngle(finalVec);

        this.transform.localScale = new Vector3(finalVec.magnitude, lineWidth, 1f);
        this.transform.rotation = Quaternion.Euler(0, 0, angle);


        if (PivotMode == PivotMode.BEGINING)
        {
            transform.position = initial + finalVec / 2;
        }
        else if (PivotMode == PivotMode.CENTER)
        {
            transform.position = initial;
        }

        SaveState?.Invoke(this, false, 0);
        SaveState?.Invoke(this, false, 1);
        SaveState?.Invoke(this, false, 2);
        SaveState?.Invoke(this, false, 3);

    }

    [Button]
    public void InitShape()
    {
        TryGetComponent<Shape>(out shapeController);
        if (shapeController == null)
        {
            shapeController = gameObject.AddComponent<Shape>();
        }
    }

    public float GetAngle(Vector2 finalVec) => Vector2.Angle(Vector2.right, finalVec);
    

    [Button]
    public void RegisterFunctions(){

        _getterSetterPairs = new List<System.Tuple<System.Func<float>, System.Action<float>>>();

        _getterSetterPairs.Add(
            new System.Tuple<System.Func<float>, System.Action<float>>(
                //Getter
                ()=>this.transform.localScale.x,

                //Setter
                (float s) => {this.transform.localScale = new Vector3(s,
                            this.transform.localScale.y, this.transform.localScale.z);
                            
                            }
            )

        );

        _getterSetterPairs.Add(
            new System.Tuple<System.Func<float>, System.Action<float>>(
                //Getter
                ()=>this.transform.eulerAngles.z,

                //Setter
                (float r) => this.transform.rotation = Quaternion.Euler(0, 0, r)
            )

        );

        _getterSetterPairs.Add(
            new System.Tuple<System.Func<float>, System.Action<float>>(
                //Getter
                ()=>this.transform.position.x,

                //Setter
                (float x) => this.transform.position = new Vector3(x, transform.position.y, transform.position.z)
            )

        );


        _getterSetterPairs.Add(
            new System.Tuple<System.Func<float>, System.Action<float>>(
                //Getter
                ()=>this.transform.position.y,

                //Setter
                (float y) => this.transform.position = new Vector3(transform.position.x, y, transform.position.z)
            )

        );

        // _getterSetterPairs.Add(
        //     new System.Tuple<System.Func<float>, System.Action<float>>(
        //         //Getter
        //         ()=>this.transform.eulerAngles.z,

        //         //Setter
        //         (float z) => this.transform.position = new Vector3(transform.position.x, transform.position.y, z+finalVec.z/2)
        //     )

        // );


       
    }

    public Vector2 ProjectVector2OnLine(float target){
        var direction = transform.right;
        var initial = transform.position - transform.localScale.x/2 * direction;

        
        var final =transform.position + transform.localScale.x/2 * direction; 
        return Vector2.Lerp(initial, final, target);
    }


}
