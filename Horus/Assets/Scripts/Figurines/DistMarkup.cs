using System.Collections;
using System.Collections.Generic;
using Shapes2D;
using UnityEngine;

public class DistMarkup : MonoBehaviour
{
    public Color lineColor;
    public Vector2 Dist = Vector2.right;
    public Vector2 StartPoint = Vector2.zero;
    public Vector2 FinalPoint = Vector2.one * 10f;
    public int PointQtd { 
        get { 
            return  Mathf.RoundToInt((FinalPoint - StartPoint).magnitude / Dist.magnitude);

        }

    }
    public float LineWidth = 0.1f;
    public float lineHeight =  0.5f;

    private Vector2 _markupAxis;
    public bool invertText;
    public float textPush = .5f;
    // Start is called before the first frame update
    void Start()
    {
        _markupAxis = Quaternion.Euler(0f, 0f, -90f) * Dist;
        for (int i = 0; i < PointQtd; i++)
        {
//            var newObj = ;
            var newLine = new GameObject($"mark_line_{i}");
            

            newLine.transform.parent = this.transform;
            
            newLine.AddComponent<SpriteRenderer>();
            var shape = newLine.AddComponent<Shape>();
            shape.settings.fillColor = lineColor;
            
            var lineCompo = newLine.AddComponent<Line>();
            lineCompo.lineColor = lineColor;
            lineCompo.PivotMode = PivotMode.BEGINING;
            lineCompo.InitShape();
            

            
            var dislocatedV2 = StartPoint + Dist * i;
            


            lineCompo.Animate = false;
            lineCompo.lineWidth = LineWidth;
            lineCompo.initial = this.transform.TransformPoint(dislocatedV2 - _markupAxis   * this.lineHeight);
            lineCompo.final = this.transform.TransformPoint(dislocatedV2 + _markupAxis * this.lineHeight);

            lineCompo.GenerateLine();


            var newText = new GameObject($"text_{i}");
            var text = newText.AddComponent<TextMesh>();
            text.text = i.ToString();
            text.transform.parent = this.transform;

            text.transform.position = lineCompo.initial + Dist* lineHeight *2;
            if(invertText){
                 text.transform.position = lineCompo.initial - Dist* lineHeight + _markupAxis * textPush;
             }else{
                 text.transform.position = lineCompo.initial + Dist* lineHeight *2 - _markupAxis * textPush;
             }

            // }
            text.color = this.lineColor;
            text.characterSize = .3f;
        }
    }

    
}
