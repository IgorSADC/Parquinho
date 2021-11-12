using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointEV : MonoBehaviour
{
    [SerializeField] private Line Ax;
    [SerializeField]public  EVariable<float> controller;

    private Vector2 Direction;


    private void Start() {
        
    }
    private void FixedUpdate() {
        var direction =  Ax.transform.right;
        var initial = Ax.transform.position - Ax.transform.localScale.x/2 * direction;

        
        var final =Ax.transform.position + Ax.transform.localScale.x/2 * direction; 
        transform.position = Vector2.Lerp(initial, final, controller.Value);
        
    }
}
