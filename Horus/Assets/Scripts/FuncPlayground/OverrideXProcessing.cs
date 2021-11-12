using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverrideXProcessing : MonoBehaviour, IPythonProcessing
{
    public ProcessingDelegate ProcessingFunction => OverrideX;

    public ProcessingType Type => ProcessingType.POST;
    public float xValue = 1f;



    private string OverrideX(string v){
        v += $"\nx={xValue}";
        return v;
    }
}
