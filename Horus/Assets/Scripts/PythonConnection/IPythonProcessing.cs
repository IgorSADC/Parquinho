using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ProcessingType{
    PRE,
    POST
}

public delegate string ProcessingDelegate(string v);
public interface IPythonProcessing 
{
    public ProcessingDelegate ProcessingFunction { get; }
    public ProcessingType Type { get; }
}
