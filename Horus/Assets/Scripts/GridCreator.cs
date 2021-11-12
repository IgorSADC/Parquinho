using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes2D;
using NaughtyAttributes;

public class GridCreator : MonoBehaviour
{
    public float planeSize = 8;
    public Color planeColor;
    public Color lineColor;

    public int lineQtd;
    public float lineWidth = .02f;
    public GameObject line;

    public List<Shape> Lines;
    public Shape board;

    // Start is called before the first frame update
    void Start()
    {
        line = new GameObject();
        transform.localScale = new Vector3(planeSize, planeSize, 1);
        this.gameObject.AddComponent<SpriteRenderer>();
        var shape = this.gameObject.AddComponent<Shape>();
        board = shape;
        shape.settings.fillColor = planeColor;

        var lineSize = (1f/(float)lineQtd); //* lineWidth;
        var initialPoint = -0.5f;

        //Horizontal
        spawnLine(initialPoint , y:0f, sx:lineWidth, sy:1, true);

        //Vertical
        spawnLine(0f, y:initialPoint , sx:1, sy:lineWidth, true);

        for (int i = 1; i < lineQtd + 1; i++)
        {
            //Horizontal
            spawnLine(initialPoint + i*lineSize, y:0f, sx:lineWidth, sy:1);

            //Vertical
            spawnLine(0f, y:initialPoint + i*lineSize, sx:1, sy:lineWidth);
        }
        



 
        
        // shape.shapeType = ShapeType.Rectangle;
    }

    private void spawnLine(float x, float y, float sx, float sy, bool isFirst = false){
         

        var l = Instantiate(line, parent:this.transform);
        l.transform.localPosition = new Vector3(x, y, this.transform.position.z);
        l.transform.localScale = new Vector3(sx, sy, 1);

        var sp = l.AddComponent<SpriteRenderer>();
        sp.sortingOrder = 1;

        var s = l.AddComponent<Shape>();

        var finalColor = isFirst? new Color (lineColor.r,lineColor.g, lineColor.b, 1f ): new Color (lineColor.r,lineColor.g, lineColor.b, .5f ) ;


        s.settings.fillColor = finalColor;

        //s.settings.fillColor = lineColor;

        Lines.Add(s);


    }
    [Button]
    public void UpdateColor(){
        int i= 0;
        foreach (var s in Lines)
        {
            var isFirst = i <2;
            var finalColor = isFirst? new Color (lineColor.r, lineColor.g, lineColor.b, 1f ): new Color (lineColor.r,lineColor.g, lineColor.b, .5f ) ;            
            i++;
            s.settings.fillColor = finalColor;


        }
        board.settings.fillColor = planeColor;


    }
}
