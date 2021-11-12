using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public abstract class SObjects : MonoBehaviour
{
    
    public abstract Tween Tick(float duration, int currentFrame);
    
}
